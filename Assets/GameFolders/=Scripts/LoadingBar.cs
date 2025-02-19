using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingBar : MonoBehaviour
{
    public Image fillBar;
    private float fillTime = 2.2f;

    void Start()
    {
        StartCoroutine(FillBarAndLoadScene());
    }

    IEnumerator FillBarAndLoadScene()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime;
            fillBar.fillAmount = Mathf.Clamp01(elapsedTime / fillTime);
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }
}