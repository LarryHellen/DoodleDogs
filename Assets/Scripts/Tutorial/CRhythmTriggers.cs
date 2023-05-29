using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRhythmTriggers : TutorialSystem
{
    public ContinousNoteSpawning cns;
    public bool noteTapped;
    public GameObject tutorialNote;
    public static bool pauseGame;



    void Awake()
    {
        pauseGame = true;
    }

    public void Next(){
        if(currentIndex == 0){
            NextTutorial();
            tutorialNote.SetActive(true);
        } else if (currentIndex == 1 && noteTapped == true){
            NextTutorial();
        } else if (currentIndex == 2){
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
        pauseGame = false;
    }
}
