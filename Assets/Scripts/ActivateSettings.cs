using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSettings : MonoBehaviour
{
    public GameObject settingsMenu;
    public void OpenSettings()
    {
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsMenu.SetActive(true);
        }
    }
}
