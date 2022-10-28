using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeRunner : MonoBehaviour
{
    public GameObject tile;

    public GameObject VictoryScreen, DefeatScreen;

    public bool toTwistOrNotToTwist;

    public float distanceBetweenTiles = 0.2f;

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

    public static bool twoPlayer = false;

    private List<List<GameObject>> board = new List<List<GameObject>>();
    public static int turnCounter = 0;

    public static bool runGame = true;

    public bool unfair;


    private bool XHasWon;
    private bool OHasWon;


    private List<int> aiCoordsToPlaceOAt;


    void OnWin()
    {
        VictoryScreen.SetActive(true);
    }

    void OnLose()
    {
        DefeatScreen.SetActive(true);
    }



    void DebugLogBoard(List<List<GameObject>> theBoard)
    {
        string aLine = "";

        foreach (List<GameObject> tmp in theBoard)
        {
            aLine = "";
            foreach (GameObject tmp1 in tmp)
            {
                aLine += tmp1.GetComponent<IfIveBeenClicked>().type.ToString() + " ";
            }
            Debug.Log(aLine);
        }
    }

    int WinDetection(List<List<GameObject>> aBoard, bool realCheck)
    {
        bool XHasWon = false;
        bool OHasWon = false;

        for (int i = 0; i < 3; i++)
        {
            if (aBoard[0][i].GetComponent<IfIveBeenClicked>().type == aBoard[1][i].GetComponent<IfIveBeenClicked>().type && aBoard[1][i].GetComponent<IfIveBeenClicked>().type == aBoard[2][i].GetComponent<IfIveBeenClicked>().type && aBoard[0][i].GetComponent<IfIveBeenClicked>().type != 0)
            {
                if (aBoard[0][i].GetComponent<IfIveBeenClicked>().type == 1)
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

            if (aBoard[i][0].GetComponent<IfIveBeenClicked>().type == aBoard[i][1].GetComponent<IfIveBeenClicked>().type && aBoard[i][1].GetComponent<IfIveBeenClicked>().type == aBoard[i][2].GetComponent<IfIveBeenClicked>().type && aBoard[i][0].GetComponent<IfIveBeenClicked>().type != 0)
            {
                if (aBoard[i][0].GetComponent<IfIveBeenClicked>().type == 1)
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

        if (aBoard[0][0].GetComponent<IfIveBeenClicked>().type == aBoard[1][1].GetComponent<IfIveBeenClicked>().type && aBoard[1][1].GetComponent<IfIveBeenClicked>().type == aBoard[2][2].GetComponent<IfIveBeenClicked>().type && aBoard[1][1].GetComponent<IfIveBeenClicked>().type != 0 || aBoard[0][2].GetComponent<IfIveBeenClicked>().type == aBoard[1][1].GetComponent<IfIveBeenClicked>().type && aBoard[1][1].GetComponent<IfIveBeenClicked>().type == aBoard[2][0].GetComponent<IfIveBeenClicked>().type && aBoard[1][1].GetComponent<IfIveBeenClicked>().type != 0)
        {
            if (aBoard[1][1].GetComponent<IfIveBeenClicked>().type == 1)
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
            if (realCheck == true)
            {
                //Debug.Log("Tie");
                runGame = false;
                DebugLogBoard(board);
                OnLose();
            }
            else
            {
                return 0;
            }
        }
        else if (XHasWon == true)
        {
            if (realCheck == true)
            {
                //Debug.Log("X has acheived a victorious status");
                runGame = false;
                DebugLogBoard(board);
                OnWin();
            }
            else
            {
                return 1;
            }
        }
        else if (OHasWon == true)
        {
            if (realCheck == true)
            {
                //Debug.Log("O has won the day");
                runGame = false;
                DebugLogBoard(board);
                OnLose();
            }
            else
            {
                return 2;
            }
        }
        else
        {
            int deadBoard = 0;

            foreach (List<GameObject> boardLine in aBoard)
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
                if (realCheck == true)
                {
                    //Debug.Log("Tie");
                    runGame = false;
                    DebugLogBoard(board);
                    OnLose();
                }
                else
                {
                    return 0;
                }
            }
        }

        return 0;
    }

    public void Retry()
    {
        //Debug.Log("yoyoyoyo");

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



    public List<int> ticTacToeAI(List<List<GameObject>> aBoard)
    {

        int gameResult;

        List<int> coordsToPlaceO = new List<int>();

        List<List<GameObject>> anotherBoard = new List<List<GameObject>>();



        foreach (List<GameObject> gameObjectLists in aBoard)
        {
            List<GameObject> tmpList = new List<GameObject>();

            foreach (GameObject aGameObject in gameObjectLists)
            {
                GameObject Tile = Instantiate(tile, new Vector3(100, 100, 0), Quaternion.identity);
                Tile.GetComponent<IfIveBeenClicked>().type = aGameObject.GetComponent<IfIveBeenClicked>().type;
                tmpList.Add(Tile);
            }

            anotherBoard.Add(tmpList);
        }




        List<List<int>> corners = new List<List<int>>()
        {
            new List<int>() { 0, 0 },
            new List<int>() { 0, 2 },
            new List<int>() { 2, 2 },
            new List<int>() { 2, 0 }
        };



        tmp2 = anotherBoard[rP[0][0]][rP[0][1]];

        for (int i = 0; i < 8; i++)
        {
            //Debug.Log(i);

            tmp1 = anotherBoard[rP[i + 1][0]][rP[i + 1][1]];

            anotherBoard[rP[i + 1][0]][rP[i + 1][1]] = tmp2;

            tmp2 = tmp1;
        }
        Debug.Log("vvvvvvvvvvvvvvvvv");

        DebugLogBoard(anotherBoard);

        Debug.Log("^^^^^^^^^^^^^^^^");



        //Disconnect between places an O can be placed and how it checks it

        //Reconnect the disconnect by moving back the position in the rotated board, "anotherBoard" when placing | Something to do with rP[i+j-1][0] and rP[i+j-1][1]
        int index = 0;
        int before;





        for (int num = 1; num < 3; num++)
        {
            Debug.Log("Placing " + num + "s rn");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (aBoard[i][j].GetComponent<IfIveBeenClicked>().type == 0) //Board is getting perma-changed bc there is a disconnect between this position and this position on the rotated board
                    {

                        List<int> anotherAnotherTmpList = new List<int>() { i, j };


                        for (int k = 0; k < 7; k++)
                        {
                            if (rP[k][0] == anotherAnotherTmpList[0] && rP[k][1] == anotherAnotherTmpList[1])
                            {
                                index = k;
                                break;
                            }
                        }



                        if (index == 0)
                        {
                            before = anotherBoard[rP[7][0]][rP[7][1]].GetComponent<IfIveBeenClicked>().type;
                            anotherBoard[rP[7][0]][rP[7][1]].GetComponent<IfIveBeenClicked>().type = num;
                        }
                        else
                        {
                            before = anotherBoard[rP[index - 1][0]][rP[index - 1][1]].GetComponent<IfIveBeenClicked>().type;
                            anotherBoard[rP[index - 1][0]][rP[index - 1][1]].GetComponent<IfIveBeenClicked>().type = num;
                        }

                        Debug.Log("[[[[[[[[[[[[[[[[[[[[");
                        DebugLogBoard(anotherBoard);
                        Debug.Log("[[[[[[[[[[[[[[[[[[[[");


                        gameResult = WinDetection(anotherBoard, false);

                        if (gameResult == num)
                        {
                            Debug.Log("Found an " + num + "win with " + i + "," + j);


                            coordsToPlaceO = new List<int>() { rP[index - 1][0], rP[index - 1][1] };


                            return coordsToPlaceO;
                        }

                        if (index == 0)
                        {
                            anotherBoard[rP[7][0]][rP[7][1]].GetComponent<IfIveBeenClicked>().type = before;
                        }
                        else
                        {
                            anotherBoard[rP[index - 1][0]][rP[index - 1][1]].GetComponent<IfIveBeenClicked>().type = before;
                        }

                }
            }
        }
    }






        //Random.Range(0, waves.Count)
        Debug.Log("Didn't find any wins so I'm winging it");

        List<List<int>> anotherTmpList = new List<List<int>>();

        foreach (List<int> coords in corners)
        {
            if (board[coords[0]][coords[1]].GetComponent<IfIveBeenClicked>().type == 0)
            {
                anotherTmpList.Add(coords);
            }
        }

        coordsToPlaceO = anotherTmpList[Random.Range(0, anotherTmpList.Count)];


        //Debug.Log("Cant believe we got past that foreach loop");

        foreach (List<GameObject> gameObjectLists in anotherBoard)
        {
            foreach (GameObject aGameObject in gameObjectLists)
            {
                Destroy(aGameObject);
            }
        }

        //coordsToPlaceO.Add(1);
        //coordsToPlaceO.Add(1);

        return coordsToPlaceO;
    }








    public static bool currentlyRotating = false;

    private GameObject tmp1, tmp2;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            List<GameObject> row = new List<GameObject>();

            for (int j = 0; j < 3; j++)
            {
                GameObject Tile = Instantiate(tile, new Vector3(j+ distanceBetweenTiles * j, -i - distanceBetweenTiles * i), Quaternion.identity);
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
            if (turnCounter % 2 == 1)
            {
                if (unfair == true)
                {
                    //DebugLogBoard();
                    WinDetection(board, true);
                }

                currentlyRotating = true;

                if (turnCounter >= 9)
                {
                    //DebugLogBoard();
                    WinDetection(board, true);
                }

            }
            else
            {
                WinDetection(board, true);
            }
        }


        if (turnCounter % 2 == 1 && twoPlayer == false && runGame == true)
        {
            //DebugLogBoard(board);

            //Debug.Log("^ Before-----After v");


            aiCoordsToPlaceOAt = ticTacToeAI(board);

            

            board[aiCoordsToPlaceOAt[0]][aiCoordsToPlaceOAt[1]].GetComponent<IfIveBeenClicked>().type = 2;
            board[aiCoordsToPlaceOAt[0]][aiCoordsToPlaceOAt[1]].GetComponent<IfIveBeenClicked>().HasChanged = false;

            WinDetection(board, true);

            turnCounter++;

            //DebugLogBoard(board);

            //Debug.Log(turnCounter);
        }






        if (runGame == true)
        {

            //EACH THING NEEDS TO MOVE ALL THE TIME, SET ITS MOVE POSITIONS AND CHANGE THEM CONSTANTLY BUT WHEN GOAL REACHED SET BOTH TO THE SAME?

            
            //Make tiles rotate every 2 turns only once using "currentlyRotating" and "rotationPattern"
            //Iterate through "rotationPattern" and set and change temp1 and temp2
            //Hard code start change of board[1][0] -> board[0][0]

            if (turnCounter % 2 == 0 && currentlyRotating == true && toTwistOrNotToTwist == true)
            {

               // Debug.Log("Its rotatin time");

                tmp2 = board[rP[0][0]][rP[0][1]];

                 for (int i = 0; i < 8; i++)
                 {
                     //Debug.Log(i);

                     tmp1 = board[rP[i + 1][0]][rP[i + 1][1]];

                     board[rP[i + 1][0]][rP[i + 1][1]] = tmp2;

                     tmp2 = tmp1;
                 }



                 


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

                        Vector3 tmpPos = new Vector3(j + distanceBetweenTiles * j, -i - distanceBetweenTiles * i);

                        board[i][j].GetComponent<IfIveBeenClicked>().rotationBool = true;
                        board[i][j].GetComponent<IfIveBeenClicked>().tmpPos = tmpPos;


                        //board[i][j].transform.position = new Vector3(j+0.2f*j, -i - 0.2f * i, 0); <- ORIG CODE


                        //STUFF FROM TILE MATCHING GAME "DOTS" SCRIPT

                        //tempPosition = new Vector2(transform.position.x, targetY);
                        //transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
                    }
                }

                currentlyRotating = false;

                DebugLogBoard(board);

                Debug.Log("----------------");
            } 
        }
    }
}
