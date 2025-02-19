using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private float timerDuration = 300;
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

    void Start()
    {
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

                if (timeElapsed >= 60)
                {
                    PlayerPrefs.SetInt("Achieve_1", 1);
                    string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
                    PlayerPrefs.SetString("Achieve_1_date", currentDate);
                }
                if (timeElapsed >= 180)
                {
                    PlayerPrefs.SetInt("Achieve_3", 1);
                    string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
                    PlayerPrefs.SetString("Achieve_3_date", currentDate);
                }
                if (timeElapsed >= 300)
                {
                    PlayerPrefs.SetInt("Achieve_4", 1);
                    string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
                    PlayerPrefs.SetString("Achieve_4_date", currentDate);
                }
            }
            else
            {
                timeElapsed = timerDuration;
                isRunning = false;
                timerText.text = "0:00";
                _playerController.WinBehavior();
            }
        }
    }

    private void UpdateUI()
    {
        float timeRemaining = timerDuration - timeElapsed;
        int minutesRemaining = Mathf.FloorToInt(timeRemaining / 60);
        int secondsRemaining = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutesRemaining}:{secondsRemaining:D2}";

        int minutesElapsed = Mathf.FloorToInt(timeElapsed / 60);
        int secondsElapsed = Mathf.FloorToInt(timeElapsed % 60);
        string elapsedTimeFormatted = $"{minutesElapsed}:{secondsElapsed:D2}";

        foreach (Text elapsedText in elapsedTimeTexts)
        {
            if (elapsedText != null)
            {
                elapsedText.text = elapsedTimeFormatted;
            }
        }

        float fillAmount = timeElapsed / timerDuration;
        progressBar.fillAmount = fillAmount;

        if (progressIndicator != null)
        {
            RectTransform barRect = progressBar.rectTransform;
            RectTransform indicatorRect = progressIndicator.rectTransform;

            float barWidth = barRect.rect.width;
            float newX = Mathf.Lerp(-barWidth / 2, barWidth / 2, fillAmount);
            indicatorRect.localPosition = new Vector3(newX, indicatorRect.localPosition.y, 0);
        }
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