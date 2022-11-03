using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSettings : MonoBehaviour
{
    public GameObject settingsMenu;
    public static bool gameRunning = true;

    public void OpenSettings()
    {
        if (settingsMenu.activeSelf)
        {
            gameRunning = true;
            settingsMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            gameRunning = false;
            settingsMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
