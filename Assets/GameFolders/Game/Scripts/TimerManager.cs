using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private float timerDuration;
    private float timeElapsed = 0f;

    public Text timerText;
    public Text[] elapsedTimeTexts;

    public Image progressBar;
    public Image progressIndicator;

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _pausePanel;

    private PlayerController _playerController;
    private CoinController _coinController;

    private bool isRunning = false;
    private int _currentLevel;
    [SerializeField] private Text _currentLevelText;

    void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        _currentLevelText.text = $"LEVEL {_currentLevel} COMPLETE";
        timerDuration = _currentLevel * 10;
        _playerController = FindObjectOfType<PlayerController>();
        timeElapsed = 0f;
        isRunning = true;
        UpdateUI();
        _coinController = GetComponent<CoinController>();
    }

    void Update()
    {
        if (isRunning && !_losePanel.activeInHierarchy && !_pausePanel.activeInHierarchy)
        {
            if (timeElapsed < timerDuration)
            {
                timeElapsed += Time.deltaTime;
                UpdateUI();

                //if (timeElapsed >= 60)
                //{
                //    PlayerPrefs.SetInt("Achieve_1", 1);
                //    string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
                //    PlayerPrefs.SetString("Achieve_1_date", currentDate);
                //}
                //if (timeElapsed >= 180)
                //{
                //    PlayerPrefs.SetInt("Achieve_3", 1);
                //    string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
                //    PlayerPrefs.SetString("Achieve_3_date", currentDate);
                //}
                //if (timeElapsed >= 300)
                //{
                //    PlayerPrefs.SetInt("Achieve_4", 1);
                //    string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
                //    PlayerPrefs.SetString("Achieve_4_date", currentDate);
                //}
            }
            else
            {
                timeElapsed = timerDuration;
                isRunning = false;
                timerText.text = "100%";
                _playerController.WinBehavior();
                _currentLevel++;
                PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            }
        }
    }

    private void UpdateUI()
    {
        //float timeRemaining = timerDuration - timeElapsed;
        //int minutesRemaining = Mathf.FloorToInt(timeRemaining / 60);
        //int secondsRemaining = Mathf.FloorToInt(timeRemaining % 60);
        //timerText.text = $"{minutesRemaining}:{secondsRemaining:D2}";
        float fillAmount = timeElapsed / timerDuration;
        progressBar.fillAmount = fillAmount;
        timerText.text = $"{Mathf.CeilToInt(fillAmount * 100)}%";
    }

    public void SaveResult()
    {
        int currentCoins = _coinController.ReturnCurrentCoinsAmount();
        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

        List<ResultEntry> results = LoadResults();

        results.Add(new ResultEntry(timeElapsed, currentDate, currentCoins));

        results.Sort((a, b) => b.time.CompareTo(a.time));

        while (results.Count > 8)
        {
            results.RemoveAt(results.Count - 1);
        }

        for (int i = 0; i < results.Count; i++)
        {
            PlayerPrefs.SetFloat($"Result_Time_{i}", results[i].time);
            PlayerPrefs.SetString($"Result_Date_{i}", results[i].date);
            PlayerPrefs.SetInt($"Result_Coins_{i}", results[i].coins);
        }

        PlayerPrefs.Save();
    }

    private List<ResultEntry> LoadResults()
    {
        List<ResultEntry> results = new List<ResultEntry>();

        for (int i = 0; i < 8; i++)
        {
            if (PlayerPrefs.HasKey($"Result_Time_{i}"))
            {
                float time = PlayerPrefs.GetFloat($"Result_Time_{i}");
                string date = PlayerPrefs.GetString($"Result_Date_{i}");
                int coins = PlayerPrefs.GetInt($"Result_Coins_{i}");
                results.Add(new ResultEntry(time, date, coins));
            }
        }

        return results;
    }

    private class ResultEntry
    {
        public float time;
        public string date;
        public int coins;

        public ResultEntry(float time, string date, int coins)
        {
            this.time = time;
            this.date = date;
            this.coins = coins;
        }
    }
}