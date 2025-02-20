using UnityEngine;
using UnityEngine.UI;

public class ChickenGameSkin : MonoBehaviour
{
    [SerializeField] private GameObject[] _chickens;

    [SerializeField] private Image _chickenPauseImage;
    [SerializeField] private Sprite[] _chickenSprites;

    private void Awake()
    {
        int chickenIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        _chickens[chickenIndex].SetActive(true);
        _chickenPauseImage.sprite = _chickenSprites[chickenIndex];
    }
}