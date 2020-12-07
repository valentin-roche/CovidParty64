
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct LevelMenu {
    public string levelName;
}

public class MainMenu : MonoBehaviour
{

    public string levelToLoad;
    public List<LevelMenu> LevelsList = new List<LevelMenu>();

    public GameObject levelsWindow;
    public GameObject controlsWindow;
    
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

    public void DisplayControlsWindow()
    {
        controlsWindow.SetActive(true);
    }

    public void CloseControlsWindow()
    {
        controlsWindow.SetActive(false);
    }

    public void Level1()
    {
        SceneManager.LoadScene(LevelsList[0].levelName);
    }

    public void Level2()
    {
        SceneManager.LoadScene(LevelsList[1].levelName);
    }

    public void Level3()
    {
        SceneManager.LoadScene(LevelsList[2].levelName);
    }

    public void Level4()
    {
        SceneManager.LoadScene(LevelsList[3].levelName);
    }

    public void Level5()
    {
        SceneManager.LoadScene(LevelsList[4].levelName);
    }

    public void Level6()
    {
        SceneManager.LoadScene(LevelsList[5].levelName);
    }

    public void Level7()
    {
        SceneManager.LoadScene(LevelsList[6].levelName);
    }

    public void Level8()
    {
        SceneManager.LoadScene(LevelsList[7].levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
