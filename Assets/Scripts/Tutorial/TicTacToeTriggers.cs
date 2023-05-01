using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeTriggers : TutorialSystem
{

    public TicTacToeRunner tttr;
    public bool placed;

    public void Setup(){
        allScenes[0].SetActive(true);
        if(tttr.advanced == true){
            advanced = true;
        }
    }

    public void Next(){
        if(currentIndex == 0){
            NextTutorial();
        } else if(currentIndex == 1 && placed == true){
            NextTutorial();
            Time.timeScale = 1;
        } else if(currentIndex == 2){
            NextTutorial();
        } else if(currentIndex == 3){
            NextTutorial();
        } else if(currentIndex == 4){
            NextTutorial();
        } else if(currentIndex == 5){
            NextTutorial();
        }
    }


    public void EndTutorial(){
        tttr.tutorialEnabled = false;
    }
}
