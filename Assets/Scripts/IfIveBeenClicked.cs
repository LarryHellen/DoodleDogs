using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfIveBeenClicked : MonoBehaviour
{

    private static int x, y;

    public static int type = 0;

    private void OnMouseDown()
    {
        if (TicTacToeRunner.turnCounter % 2 == 0)
        {
            type = 1;
            TicTacToeRunner.turnCounter++;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        //if "type" a certain num, set texture to a certain something
    }
}
