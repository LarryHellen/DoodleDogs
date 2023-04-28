using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchingTriggers : TutorialSystem
{
    public Board board;
    public bool clearedMatch;
    public int[] setMatch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(){
        allScenes[0].SetActive(true);
    }

    public void Next(){
        if(currentIndex == 0){
            NextTutorial();
            CreateMatch();
        } else if (currentIndex == 1 && clearedMatch){
            NextTutorial();
        } else if (currentIndex == 2){
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

    public void EndTutorial(){
        board.tutorialEnabled = false;
    }
}