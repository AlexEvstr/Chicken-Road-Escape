using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    [System.Serializable]
    public class LeaderboardSlot
    {
        public Text timeText;
        public Text coinsText;
        public Text dateText;
    }

    public LeaderboardSlot[] slots;

    void Start()
    {
        LoadResults();
    }

    private void LoadResults()
    {
        List<ResultEntry> results = new List<ResultEntry>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (PlayerPrefs.HasKey($"Result_Time_{i}"))
            {
                float time = PlayerPrefs.GetFloat($"Result_Time_{i}");
                string date = PlayerPrefs.GetString($"Result_Date_{i}");
                int coins = PlayerPrefs.GetInt($"Result_Coins_{i}");

                results.Add(new ResultEntry(time, date, coins));
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < results.Count)
            {
                int minutes = Mathf.FloorToInt(results[i].time / 60);
                int seconds = Mathf.FloorToInt(results[i].time % 60);

                slots[i].timeText.text = $"{minutes}:{seconds:D2}";
                slots[i].coinsText.text = results[i].coins.ToString();
                slots[i].dateText.text = results[i].date;
            }
            else
            {
                slots[i].timeText.text = "--:--";
                slots[i].coinsText.text = "--";
                slots[i].dateText.text = "--.--.----";
            }
        }
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