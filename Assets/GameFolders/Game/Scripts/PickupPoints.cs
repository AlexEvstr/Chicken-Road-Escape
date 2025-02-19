using UnityEngine;

public class PickupPoints : MonoBehaviour
{
    public int scoreToGive;

    private ScoreManager theScoreManager;

    private AudioSource coinSound;

    private CoinController _coinController;

    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        _coinController = FindObjectOfType<CoinController>();

        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
        coinSound.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theScoreManager.addScore(scoreToGive);
            _coinController.IncreaseCoins(1);
            gameObject.SetActive(false);

            if(coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
        }
    }
}