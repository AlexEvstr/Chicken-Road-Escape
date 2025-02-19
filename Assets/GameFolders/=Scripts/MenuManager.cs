using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OpenWindow(GameObject window)
    {
        if (window.activeSelf) return;

        window.SetActive(true);
        window.transform.localScale = Vector3.zero;
        LeanTween.scale(window, Vector3.one, 0.5f).setEaseOutBack();
    }

    public void CloseWindow(GameObject window)
    {
        if (!window.activeSelf) return;

        LeanTween.scale(window, Vector3.zero, 0.3f).setEaseInBack().setOnComplete(() => window.SetActive(false));
    }

    public void StartGame()
    {
        StartCoroutine(StartGameWithDelay());
    }

    private IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("GameScene");
    }
}