using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MoveCounter : MonoBehaviour
{
    private Board board;
    public int currentMoves;
    private TextMeshProUGUI textField;
    bool tryingToEndGame;


    void Start(){
        board = FindObjectOfType<Board>();
        textField = gameObject.GetComponent<TextMeshProUGUI>();
        currentMoves = board.startingMoves;
        UpdateText();
    }

    void Update(){
        if(board.coCount == 0 && tryingToEndGame){
            board.endGame();
        }
    }

    public void SpendMove(){
        currentMoves--;
        UpdateText();
        if(currentMoves == 0){
            tryingToEndGame = true;
        }
    }

    public void Reset(){
        currentMoves = board.startingMoves;
        tryingToEndGame = false;
        UpdateText();
    }

    private void UpdateText(){
        textField.text = currentMoves + "/" + board.startingMoves;
    }
}
