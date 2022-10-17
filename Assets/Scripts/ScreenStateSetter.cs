using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenStateSetter : MonoBehaviour
{

    public string stateName;
    private Board board;

    void Start(){
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stateName == "win"){
            board.currentState = GameState.win;
        } else if(stateName == "lose"){
            board.currentState = GameState.lose;
        }
    }
}
