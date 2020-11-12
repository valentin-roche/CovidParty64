using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    private bool isPaused = false;
    public string mainMenuScene;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        
    }

    public void resume()
    {
        isPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void exit()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
