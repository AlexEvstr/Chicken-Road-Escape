using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public GameObject lostHeartEffect;
    private GameObject _chicken;
    private PlayerController _playerController;
    [SerializeField] private Text _livesText; 

    private int currentLives;

    void Start()
    {
        _chicken = GameObject.FindWithTag("Player");
        _playerController = FindObjectOfType<PlayerController>();
        currentLives = PlayerPrefs.GetInt("CurrentLives", 1);
        PlayerPrefs.SetInt("CurrentLives", 50);
        _livesText.text = $"x{currentLives}";
    }

    public void LoseLife()
    {
        currentLives--;
        ShowLostHeartEffect();

        if (currentLives <= 0) _playerController.LoseBehavior();
        _livesText.text = $"x{currentLives}";
    }

    private void ShowLostHeartEffect()
    {
        GameObject minusHeart = Instantiate(lostHeartEffect, _chicken.transform);
        minusHeart.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        minusHeart.transform.position = new Vector3(_chicken.transform.position.x, _chicken.transform.position.y + 2, _chicken.transform.position.z);
        Destroy(minusHeart, 0.5f);
    }
}