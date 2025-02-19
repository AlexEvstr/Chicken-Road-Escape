using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    public GameObject[] hearts;
    public GameObject[] lostHeartEffects;
    private PlayerController _playerController;

    private int currentLives;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        currentLives = hearts.Length;
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;

            if (hearts[currentLives] != null)
                hearts[currentLives].SetActive(true);

            if (lostHeartEffects.Length > currentLives && lostHeartEffects[currentLives] != null)
                StartCoroutine(ShowLostHeartEffect(lostHeartEffects[currentLives]));
        }

        if (currentLives <= 0)
        {
            _playerController.LoseBehavior();
        }
    }

    private IEnumerator ShowLostHeartEffect(GameObject effect)
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        effect.SetActive(false);
    }
}