using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTimer : MonoBehaviour
{
    private void Start()
    {
        Invoke("OpenMenu", 2.5f);
    }

    private void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}