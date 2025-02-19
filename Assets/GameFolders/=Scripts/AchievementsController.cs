using UnityEngine;
using UnityEngine.UI;

public class AchievementsController : MonoBehaviour
{
    [SerializeField] private GameObject[] _eggsColor;
    [SerializeField] private Text[] _dateText;

    private void Start()
    {
        for (int i = 0; i < _eggsColor.Length; i++)
        {
            CheckEggs(i);
        }
    }

    private void CheckEggs(int index)
    {
        int status = PlayerPrefs.GetInt($"Achieve_{index}", 0);
        if (status == 1)
        {
            _eggsColor[index].SetActive(true);
            string date = PlayerPrefs.GetString($"Achieve_{index}_date", "");
            _dateText[index].text = date;
        }
    }
}