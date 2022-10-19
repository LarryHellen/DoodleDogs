using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeRunner : MonoBehaviour
{
    public GameObject tile;

    public GameObject VictoryScreen, DefeatScreen;
    


    private List<List<int>> rP = new List<List<int>>()
    {
        new List<int>(){0,0},
        new List<int>(){0,1},
        new List<int>(){0,2},
        new List<int>(){1,2},
        new List<int>(){2,2},
        new List<int>(){2,1},
        new List<int>(){2,0},
        new List<int>(){1,0},
        new List<int>(){0,0}

    };


    private List<List<GameObject>> board = new List<List<GameObject>>();
    public static int turnCounter = 0;

    public static bool runGame = true;




    void OnWin()
    {
        VictoryScreen.SetActive(true);
    }

    void OnLose()
    {
        DefeatScreen.SetActive(true);
    }



    void DebugLogBoard()
    {
        string aLine = "";

        foreach (List<GameObject> tmp in board)
        {
            aLine = "";
            foreach (GameObject tmp1 in tmp)
            {
                aLine += tmp1.GetComponent<IfIveBeenClicked>().type.ToString() + " ";
            }
            Debug.Log(aLine);
        }
    }

    void WinDetection() // NEEDS DEBUGGING MAYBE IDK
    {
        bool XHasWon = false, OHasWon = false;

        for (int i = 0; i < 3; i++)
        {
            if (board[0][i].GetComponent<IfIveBeenClicked>().type == board[1][i].GetComponent<IfIveBeenClicked>().type && board[1][i].GetComponent<IfIveBeenClicked>().type == board[2][i].GetComponent<IfIveBeenClicked>().type && board[0][i].GetComponent<IfIveBeenClicked>().type != 0)
            {
                if (board[0][i].GetComponent<IfIveBeenClicked>().type == 1)
                {
                    XHasWon = true;
                    //Debug.Log(i);
                    //Debug.Log("1 - X");
                    //DebugLogBoard();
                }
                else
                {
                    OHasWon = true;
                    //Debug.Log(i);
                    //Debug.Log("1 - O");
                    //DebugLogBoard();
                }
            }

            if (board[i][0].GetComponent<IfIveBeenClicked>().type == board[i][1].GetComponent<IfIveBeenClicked>().type && board[i][1].GetComponent<IfIveBeenClicked>().type == board[i][2].GetComponent<IfIveBeenClicked>().type && board[i][0].GetComponent<IfIveBeenClicked>().type != 0)
            {
                if (board[i][0].GetComponent<IfIveBeenClicked>().type == 1)
                {
                    XHasWon = true;
                    //Debug.Log(i);
                    //Debug.Log("2 - X");
                    //DebugLogBoard();
                }
                else
                {
                    OHasWon = true;
                    //Debug.Log(i);
                    //Debug.Log("2 - O");
                    //DebugLogBoard();
                }
            }

        }

        if (board[0][0].GetComponent<IfIveBeenClicked>().type == board[1][1].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type == board[2][2].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type != 0 || board[0][2].GetComponent<IfIveBeenClicked>().type == board[1][1].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type == board[2][0].GetComponent<IfIveBeenClicked>().type && board[1][1].GetComponent<IfIveBeenClicked>().type != 0)
        {
            if (board[1][1].GetComponent<IfIveBeenClicked>().type == 1)
            {
                XHasWon = true;
                //Debug.Log("3 - X");
                //DebugLogBoard();
            }
            else
            {
                OHasWon = true;
                //Debug.Log("3 - O");
                //DebugLogBoard();
            }
        }


        if (XHasWon == true && OHasWon == true)
        {
            Debug.Log("Tie");
            runGame = false;
            DebugLogBoard();
            OnLose();
        } else if (XHasWon == true)
        {
            Debug.Log("X has acheived a victorious status");
            runGame = false;
            DebugLogBoard();
            OnWin();
        } else if (OHasWon == true)
        {
            Debug.Log("O has won the day");
            runGame = false;
            DebugLogBoard();
            OnLose();
        }
        else
        {
            int deadBoard = 0;

            foreach (List<GameObject> boardLine in board)
            {
                foreach (GameObject boardStuff in boardLine)
                {
                    if (boardStuff.GetComponent<IfIveBeenClicked>().type == 0)
                    {
                        deadBoard++;
                    }
                }
            }

            if (deadBoard == 0)
            {
                Debug.Log("Tie");
                runGame = false;
                DebugLogBoard();
                Retry();
            }
        }

    }

    public void Retry()
    {
        Debug.Log("yoyoyoyo");

        VictoryScreen.SetActive(false);
        DefeatScreen.SetActive(false);

        turnCounter = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i][j].GetComponent<IfIveBeenClicked>().type = 0;
            }
        }

        runGame = true;
    }

    private bool hasRotated = false;

    private GameObject tmp1, tmp2;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            List<GameObject> row = new List<GameObject>();

            for (int j = 0; j < 3; j++)
            {
                GameObject Tile = Instantiate(tile, new Vector3(j+0.2f*j, -i - 0.2f * i, 0), Quaternion.identity);
                row.Add(Tile);
            }

            board.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (runGame == true)
        {

            
            //Make tiles rotate every 2 turns only once using "hasRotated" and "rotationPattern"
            //Iterate through "rotationPattern" and set and change temp1 and temp2
            //Hard code start change of board[1][0] -> board[0][0]

            if (turnCounter % 2 == 0 && hasRotated == true)
            {

                tmp2 = board[rP[0][0]][rP[0][1]];

                 for (int i = 0; i < 8; i++)
                 {
                     //Debug.Log(i);

                     tmp1 = board[rP[i + 1][0]][rP[i + 1][1]];

                     board[rP[i + 1][0]][rP[i + 1][1]] = tmp2;

                     tmp2 = tmp1;
                 }



                 hasRotated = false;


                /*
                GameObject temp = board[0][0];
                board[0][0] = board[1][0];

                GameObject temp2 = board[0][1];
                board[0][1] = temp;


                temp = board[0][2];
                board[0][2] = temp2;

                temp2 = board[1][2];
                board[1][2] = temp;

                temp = board[2][2];
                board[2][2] = temp2;
                */

                for (int i = 0; i < 3; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {
                        board[i][j].transform.position = new Vector3(j+0.2f*j, -i - 0.2f * i, 0);
                    }

                }

                //DebugLogBoard();
            }
            if (runGame == true)
            {
                if (turnCounter % 2 == 1)
                {
                    hasRotated = true;
                    if (turnCounter >= 9)
                    {
                        //DebugLogBoard();
                        WinDetection();
                    }

                }
                else
                {
                    WinDetection();
                }
            }

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
            
        }
    }
}
