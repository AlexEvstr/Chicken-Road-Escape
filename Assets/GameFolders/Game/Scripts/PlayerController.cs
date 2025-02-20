using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    private Vector3 originalScale;
    private bool isShrinking = false;
    [SerializeField] private Text speedText;

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

        originalScale = GetComponent<Transform>().transform.localScale;

        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        speedText.text = (moveSpeed - 7.0f).ToString("F2") + "x";

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (grounded)
        {
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            moveSpeed += 0.01f;
        }
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

    public void ShrinkTemporarily()
    {
        if (!isShrinking && transform.gameObject.activeInHierarchy)
        {
            _gameSounds.PlayResizeSound();
            StartCoroutine(FastShrinkCoroutine(1.5f));
        }
    }

    private IEnumerator FastShrinkCoroutine(float duration)
    {
        isShrinking = true;
        Vector3 targetScale = new Vector3(1f, 1f, 1f);
        float shrinkTime = duration * 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < shrinkTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        yield return new WaitForSeconds(duration * 0.5f);

        elapsedTime = 0f;
        while (elapsedTime < shrinkTime)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / shrinkTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        isShrinking = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("killbox"))
        {
            GameObject boom = Instantiate(_boom);
            boom.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Destroy(boom, 2.0f);
            other.gameObject.SetActive(false);
            _lifeManager.LoseLife();
            _gameSounds.PlayBombSound();
        }
        else if (other.gameObject.CompareTag("BottomKill"))
        {
            JumpFromSpikes(15f);
            _lifeManager.LoseLife();
            _gameSounds.PlaySpikesSound();
        }
    }

    public void JumpFromSpikes(float jumpForce)
    {
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f); // Обнуляем вертикальную скорость
        myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void LoseBehavior()
    {
        StartCoroutine(WaitAndLose());
    }

    private IEnumerator WaitAndLose()
    {
        yield return new WaitForSeconds(0.25f);
        _gameSounds.StopAllMusic();
        _gameSounds.PlayLoseSound();
        theGameManager.RestartGame();
        moveSpeed = moveSpeedOriginal;
        speedUpDistanceCount = speedUpDistanceOriginal;
        speedUpDistance = speedUpDistanceOriginal;
        //deathSound.Play();
    }

    public void WinBehavior()
    {
        theGameManager.WinGame();
        moveSpeed = moveSpeedOriginal;
        speedUpDistanceCount = speedUpDistanceOriginal;
        speedUpDistance = speedUpDistanceOriginal;
        //deathSound.Play();
    }
}