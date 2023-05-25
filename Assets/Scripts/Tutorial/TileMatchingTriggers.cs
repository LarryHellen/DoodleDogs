using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchingTriggers : TutorialSystem
{
    public Board board;
    public bool clearedMatch;
    public int[] setMatch;


    public void Setup(){
        allScenes[0].SetActive(true);
        if(board.advanced == true){
            advanced = true;
        }
    }

    public void Next(){
        if(currentIndex == 0){
            NextTutorial();
            //CreateMatch();
        } else if (currentIndex == 1 && clearedMatch){
            NextTutorial();
        } else if (currentIndex == 2){
            NextTutorial();
        } else if (currentIndex == 3){
            NextTutorial();
        } else if (currentIndex == 4){
            NextTutorial();
        }
    }

    public void CreateMatch(){
        setMatch = board.FindSet();
        /*print(setMatch[0]);
        print(setMatch[1]);
        print(setMatch[2]);
        print(setMatch[3]);
        print(setMatch[4]);
        print(setMatch[5]);*/
        board.allDots[setMatch[0],setMatch[1]].GetComponent<Dot>().isTutorial = true;
        board.allDots[setMatch[2],setMatch[3]].GetComponent<Dot>().isTutorial = true;;
        board.allDots[setMatch[4],setMatch[5]].GetComponent<Dot>().isTutorial = true;;
        //create panels around set match here
    }

    public void Update()
    {
        if(shouldEnd == true)
        {
            EndTutorial();
        }
    }

    public void EndTutorial(){
        print("got here 3");
        board.tutorialEnabled = false;
        board.ResetGame();
        shouldEnd = false;
    }
}
