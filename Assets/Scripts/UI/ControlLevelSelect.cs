using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLevelSelect : MonoBehaviour
{
    public GameObject chapter2Disable;
    public GameObject chapter3Disable;
    public CutsceneManager cutsceneManager;
    // Start is called before the first frame update
    void Start()
    {
        if (cutsceneManager.cutscenes == cutsceneManager.secondChapter)
        {
            chapter2Disable.SetActive(false);
        }
        else if (cutsceneManager.cutscenes == cutsceneManager.thirdChapter)
        {
            chapter2Disable.SetActive(false);
            chapter3Disable.SetActive(false);
        }
    }

}
