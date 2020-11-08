using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Debug.Log("Disp");
            PauseMenuUI.SetActive(true);
        }
        else
        {
            Debug.Log("Hide");
            PauseMenuUI.SetActive(true);
        }
    }
}
