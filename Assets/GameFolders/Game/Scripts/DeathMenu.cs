using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public string mainMenuLevel;

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene("MenuScene");
    }
}