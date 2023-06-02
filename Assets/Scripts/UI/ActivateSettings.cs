using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateSettings : MonoBehaviour
{
    public GameObject settingsMenu;
    public bool gameRunning = true;

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
            settingsMenu.SetActive(true);
            open.SetActive(false);
            Time.timeScale = 0;
            state = true;
        }
        else
        {
            gameRunning = true;
            settingsMenu.SetActive(false);
            open.SetActive(true);
            Time.timeScale = 1;
            state = false;
        }

        //Audio Stuff (Only works specifically in Tic Tac Toe)
        try
        {
            BackgroundAudioHandler backgroundAudioHandler = FindObjectOfType<BackgroundAudioHandler>();
            if (state == true)
            {
                backgroundAudioHandler.PauseAudio();
            }
            else
            {
                backgroundAudioHandler.UnPauseAudio();
            }
        }
        catch
        {
            //Nothing lol
        }
    }   
}
