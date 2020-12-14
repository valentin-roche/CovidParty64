using Stats;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    private bool isPaused = false;
    public string mainMenuScene;
    public Text Effects;

    

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

        effectsText();

        
    }

    public void resume()
    {
        isPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void effectsText()
    {
        string effects = "";
        for(int i = 0; i < PlayerStat.ChosenCouples.Count; i++)
        {
            effects += PlayerStat.ChosenCouples[i].DisplayName + "\n";

        }
        Effects.text = effects;
    }
}
