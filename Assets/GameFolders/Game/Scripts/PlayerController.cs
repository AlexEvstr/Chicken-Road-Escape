using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedOriginal;
    public float speedMultiplier;

    public float speedUpDistance;
    private float speedUpDistanceOriginal;
    private float speedUpDistanceCount;

    public float jumpForce;
    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask WhatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    private Animator myAnimator;

    public float jumpTime;
    private bool canDoubleJump;

    public GameManager theGameManager;
    private GameSounds _gameSounds;
    private int _jumpsCount;
    private float _vibration;

    [SerializeField] private LifeManager _lifeManager;
    [SerializeField] private GameObject _boom;

    void Start()
    {
        jumpSound.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        deathSound.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        _jumpsCount = 0;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        speedUpDistanceCount = speedUpDistance;

        moveSpeedOriginal = moveSpeed;
        speedUpDistanceOriginal = speedUpDistanceCount;

        _gameSounds = theGameManager.GetComponent<GameSounds>();
        _vibration = PlayerPrefs.GetFloat("VibroStatus", 1);
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        if (transform.position.x > speedUpDistanceCount)
        {
            speedUpDistanceCount += speedUpDistance;
            speedUpDistance *= speedMultiplier;
            moveSpeed *= speedMultiplier;
        }

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (grounded)
        {
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    public void Jump()
    {
        if (grounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            
            jumpSound.Play();
            _jumpsCount++;
            if (_vibration == 1) Vibration.VibratePop();

        }
        else if (!grounded && canDoubleJump)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            
            canDoubleJump = false;
            jumpSound.Play();
            _jumpsCount++;
            if (_vibration == 1) Vibration.VibratePop();
        }
        if (_jumpsCount >= 200)
        {
            PlayerPrefs.SetInt("Achieve_2", 1);
            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
            PlayerPrefs.SetString("Achieve_2_date", currentDate);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("killbox"))
        {
            GameObject boom = Instantiate(_boom);
            boom.transform.position = other.transform.position;
            Destroy(boom, 2.0f);
            other.gameObject.SetActive(false);
            _lifeManager.LoseLife();
            _gameSounds.PlayBombSound();
        }
        else if (other.gameObject.CompareTag("BottomKill"))
        {
            LoseBehavior();
        }
    }

    public void LoseBehavior()
    {
        _gameSounds.PlayLoseSound();
        theGameManager.RestartGame();
        moveSpeed = moveSpeedOriginal;
        speedUpDistanceCount = speedUpDistanceOriginal;
        speedUpDistance = speedUpDistanceOriginal;
        deathSound.Play();
    }

    public void WinBehavior()
    {
        theGameManager.WinGame();
        moveSpeed = moveSpeedOriginal;
        speedUpDistanceCount = speedUpDistanceOriginal;
        speedUpDistance = speedUpDistanceOriginal;
        deathSound.Play();
    }
}