using UnityEngine;

public class ChickenGameSkin : MonoBehaviour
{
    [SerializeField] private GameObject[] _chickens;

    private void Awake()
    {
        _chickens[PlayerPrefs.GetInt("SelectedSkin", 0)].SetActive(true);
    }
}