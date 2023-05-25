using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRhythmTriggers : TutorialSystem
{
    public ContinousNoteSpawning cns;

    public void Setup(){
        allScenes[0].SetActive(true);
        if(cns.advanced == true){
            advanced = true;
        }
    }

    public void Next(){
        if(currentIndex == 0){
            NextTutorial();
        } else if (currentIndex == 1){
            NextTutorial();
        } else if (currentIndex == 2 /*tap note*/){
            NextTutorial();
        } else if (currentIndex == 3){
            NextTutorial();
        } else if (currentIndex == 4){
            NextTutorial();
        } else if(currentIndex == 5){
            NextTutorial();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(shouldEnd == true)
        {
            EndTutorial();
        }
    }

    public void EndTutorial(){
        Time.timeScale = 1.0f;
        shouldEnd = false;
    }
}
