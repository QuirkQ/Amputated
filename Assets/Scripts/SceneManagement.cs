using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement: MonoBehaviour
{
    public void SceneMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void SceneYouWon()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    public void SceneFirstLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void levelOne()
    {
        SceneManager.LoadScene("Level1");
    }
}