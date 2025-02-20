using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    [SerializeField] private Text _totalCoinsText;
    [SerializeField] private Text[] _currentCoinsText;
    private int _totalCoinsAmount;
    private int _currentCoins = 0;
    [SerializeField] private GameObject _plusCoins;

    private int _coinsIncreaseCount = 0;
    private float _lastCheckTime = 0f;

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

        if (Time.time - _lastCheckTime > 0.5f)
        {
            _coinsIncreaseCount = 0;
        }

        _coinsIncreaseCount++;
        _lastCheckTime = Time.time;

        UpdateCouinsText();
        StartCoroutine(ShowPlus10());
    }

    private IEnumerator ShowPlus10()
    {
        //yield return new WaitForSeconds(1.0f);
        if (_plusCoins.activeInHierarchy) _plusCoins.SetActive(false);
        if (_coinsIncreaseCount != 0)
            _plusCoins.GetComponent<Text>().text = (_coinsIncreaseCount * 10).ToString();
        else
            _plusCoins.GetComponent<Text>().text = 10.ToString();
        _plusCoins.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _plusCoins.SetActive(false);
    }

    public int ReturnCurrentCoinsAmount()
    {
        return _currentCoins;
    }
}