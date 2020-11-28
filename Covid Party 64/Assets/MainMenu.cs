
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad;
    public string level1;
    public string level2;

    public GameObject levelsWindow;
    
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void LevelsButton()
    {
        levelsWindow.SetActive(true);
    }

    public void CloseLevelsWindow()
    {
        levelsWindow.SetActive(false);
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1);
    }

    public void Level2()
    {
        SceneManager.LoadScene(level2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
