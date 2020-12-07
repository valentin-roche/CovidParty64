using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct Level
{
    public string SceneName;
}

public static class LevelsData
{

    public static Level[] levels = {
        new Level()
        {
            SceneName = "LVL_1_Hospital"
        },
        new Level()
        {
            SceneName = "LVL_2_Sewers"
        },
        new Level()
        {
            SceneName = "LVL_3_Street"
        },
        new Level()
        {
            SceneName = "LVL_4_Forest"
        },
        new Level()
        {
            SceneName = "LVL_5_Mountain"
        },
        new Level()
        {
            SceneName = "LVL_6_Fire Forest"
        },
        new Level()
        {
            SceneName = "LVL_7_Beach"
        },
        new Level()
        {
            SceneName = "LVL_8_Ship"
        }
    };
    private static int currentLevel = 0;

    public static void LoadNextLevel()
    {
        if (levels[currentLevel+1].SceneName != null)
        {
            currentLevel++;
            SceneManager.LoadScene(levels[currentLevel].SceneName);
        }
        else
        {
            SceneManager.LoadScene("Last Scene");
        }
    }
}
