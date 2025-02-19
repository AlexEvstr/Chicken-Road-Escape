using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool doublePoints;
    public bool safeMode;

    public float powerUpLength;


    public Sprite[] powerUpSprites;

    private CoinController _coinController;
    private GameManager _gameManager;
    private GameSounds _gameSounds;
    

    void Start()
    {
        _coinController = FindObjectOfType<CoinController>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameSounds = _gameManager.GetComponent<GameSounds>();
    }

    void Awake ()
    {
        int powerUpSelector = Random.Range(0, 2);

        GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerUpSelector];

        switch (powerUpSelector)
        {
            case 0: doublePoints = true;
                break;

            case 1: safeMode = true;
                break;
        }

    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Player")
        {
            _coinController.IncreaseCoins(25);
            _gameManager.IncreaseAndShow25();
            _gameSounds.PlayEggSound();
        }
        gameObject.SetActive(false);
    }
}