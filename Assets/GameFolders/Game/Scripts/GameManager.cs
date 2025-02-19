using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlatformGenerator;

    private PlayerController thePlayer;

    public ScoreManager theScoreManager;

    public DeathMenu theDeathMenu;
    [SerializeField] private GameObject _winMenu;

    public GameObject pauseButton;

    public bool powerUpReset;

    [SerializeField] private GameObject _plus25;
    private TimerManager _timerManager;
    private GameSounds _gameSounds;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        theScoreManager = FindObjectOfType<ScoreManager>();
        _timerManager = GetComponent<TimerManager>();
        _gameSounds = GetComponent<GameSounds>();
    }

    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);

        theDeathMenu.gameObject.SetActive(true);

        pauseButton.SetActive(false);
        _timerManager.SaveResult();
    }

    public void WinGame()
    {
        _gameSounds.PlayWinSound();
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);

        _winMenu.SetActive(true);

        pauseButton.SetActive(false);
        _timerManager.SaveResult();
    }

    public void IncreaseAndShow25()
    {
        StartCoroutine(ShowPlus25());
    }

    private IEnumerator ShowPlus25()
    {
        if (_plus25.activeInHierarchy) _plus25.SetActive(false);
        _plus25.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _plus25.SetActive(false);
    }
}