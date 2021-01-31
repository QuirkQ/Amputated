using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement: MonoBehaviour
{
    public void SceneMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void SceneYouWon()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("WinScreen");
    }

    public void doExitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void SceneFirstLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void levelOne()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }

    public void Reload()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }
}