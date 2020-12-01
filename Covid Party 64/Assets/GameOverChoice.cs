using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverChoice : MonoBehaviour
{


    public string levelToLoad;

    public void MainMenu()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    


    public void QuitGame()
    {
        Application.Quit();
    }
}
