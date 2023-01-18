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


    void Start(){
        board = FindObjectOfType<Board>();
        textField = gameObject.GetComponent<TextMeshProUGUI>();
        currentMoves = board.startingMoves;
        UpdateText();
    }

    public void SpendMove(){
        currentMoves--;
        UpdateText();
        if(currentMoves == 0){
            board.endGame();
        }
    }

    public void Reset(){
        currentMoves = board.startingMoves;
        UpdateText();
    }

    private void UpdateText(){
        textField.text = currentMoves + "/" + board.startingMoves;
    }
}
