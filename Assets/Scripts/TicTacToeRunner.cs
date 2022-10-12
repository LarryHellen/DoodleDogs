using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeRunner : MonoBehaviour
{
    public GameObject tile;

    private List<List<GameObject>> board = new List<List<GameObject>>();
    public static int turnCounter = 0;


    public List<List<GameObject>> Retry()
    {
        List<List<GameObject>> board = new List<List<GameObject>>();

        for (int i = 0; i < 3; i++)
        {
            List<GameObject> row = new List<GameObject>();

            for (int j = 0; j < 3; j++)
            {
                GameObject Tile = Instantiate(tile, new Vector3(i - 1, j, 0), Quaternion.identity);
                row.Add(Tile);
            }

            board.Add(row);
        }

        return board;
    }

    // Start is called before the first frame update
    void Start()
    {
        board = Retry();
    }

    // Update is called once per frame
    void Update()
    {
        /*

        ####### AI CODE WILL GO HERE ######

        if (turnCounter % 2 == 1)
        {
            board[1][2].GetComponent<IfIveBeenClicked>().type = 2;
            board[1][2].GetComponent<IfIveBeenClicked>().HasChanged = false;
            turnCounter++;
        }

        */



        //Win Detection Here

        for (int i = 0; i < 3; i++)
        {
            if (board[0][i].GetComponent<IfIveBeenClicked>().type == board[1][i].GetComponent<IfIveBeenClicked>().type && board[1][i].GetComponent<IfIveBeenClicked>().type == board[2][i].GetComponent<IfIveBeenClicked>().type && board[0][i].GetComponent<IfIveBeenClicked>().type != 0)
            {
                if (board[0][i].GetComponent<IfIveBeenClicked>().type == 1)
                {
                    Debug.Log("Xs are victorious");
                }
                else
                {
                    Debug.Log("Os have won the day");
                }
            }

            if (board[i][0].GetComponent<IfIveBeenClicked>().type == board[i][1].GetComponent<IfIveBeenClicked>().type && board[i][1].GetComponent<IfIveBeenClicked>().type == board[i][2].GetComponent<IfIveBeenClicked>().type && board[i][0].GetComponent<IfIveBeenClicked>().type != 0)
            {
                if (board[i][0].GetComponent<IfIveBeenClicked>().type == 1)
                {
                    Debug.Log("Xs are victorious");
                }
                else
                {
                    Debug.Log("Os have won the day");
                }
            }

        }

        if (board[0][0].GetComponent<IfIveBeenClicked>().type == board[1][1].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type == board[2][2].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type != 0 || board[0][2].GetComponent<IfIveBeenClicked>().type == board[1][1].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type == board[2][0].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type != 0)
        {
            if (board[1][1].GetComponent<IfIveBeenClicked>().type == 1)
            {
                Debug.Log("Xs are victorious");
            }
            else
            {
                Debug.Log("Os have won the day");
            }



        }
    }
}
