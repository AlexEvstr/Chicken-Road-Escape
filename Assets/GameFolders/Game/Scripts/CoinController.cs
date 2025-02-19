using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    [SerializeField] private Text _totalCoinsText;
    [SerializeField] private Text[] _currentCoinsText;
    private int _totalCoinsAmount;
    private int _currentCoins = 0;

    private void Start()
    {
        _totalCoinsAmount = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCouinsText();
    }

    private void UpdateCouinsText()
    {
        foreach (var item in _currentCoinsText)
        {
            item.text = _currentCoins.ToString();
        }
        
        _totalCoinsText.text = _totalCoinsAmount.ToString();
        PlayerPrefs.SetInt("TotalCoins", _totalCoinsAmount);
    }

    public void IncreaseCoins(int coinsToIncrese)
    {
        _currentCoins += coinsToIncrese;
        _totalCoinsAmount += coinsToIncrese;
        UpdateCouinsText();
        if (_currentCoins >= 100)
        {
            PlayerPrefs.SetInt("Achieve_0", 1);
            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
            PlayerPrefs.SetString("Achieve_0_date", currentDate);
        }
    }

    public int ReturnCurrentCoinsAmount()
    {
        return _currentCoins;
    }
}