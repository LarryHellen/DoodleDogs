using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSettings : MonoBehaviour
{
    public GameObject settingsMenu;
    public bool gameRunning = true;

    public GameObject closed;
    public GameObject open;
    private bool state = false;

    /*
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
    */

    public void SettingsClicked()
    {
        if (state == false)
        {
            gameRunning = false;
            open.SetActive(true);
            Time.timeScale = 0;
            state = true;
        }
        else
        {
            gameRunning = true;
            open.SetActive(false);
            Time.timeScale = 1;
            state = false;
        }
    }
}
