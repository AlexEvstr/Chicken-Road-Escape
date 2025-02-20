using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChoose : MonoBehaviour
{
    private void Start()
    {
        int bestLevel = PlayerPrefs.GetInt("BestLevel", 1);
        if (bestLevel >= int.Parse(gameObject.name))
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void ChooseLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        StartCoroutine(StartGameWithDelay());
    }

    private IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("Gameplay");
    }
}