using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeRunner : MonoBehaviour
{
    public GameObject tile;

    public GameObject VictoryScreen, DefeatScreen, TieScreen;

    public bool toTwistOrNotToTwist;

    public float distanceBetweenTiles = 0.2f;

    private List<List<GameObject>> beforeBoard = new List<List<GameObject>>();

    public float xBoardOffset;
    public float yBoardOffset;

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


    private List<List<int>> corners = new List<List<int>>()
     {
         new List<int>() { 0, 0 },
         new List<int>() { 0, 2 },
         new List<int>() { 2, 2 },
         new List<int>() { 2, 0 }
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
        FindObjectOfType<AudioManager>().Play("Scratch");
        VictoryScreen.SetActive(true);
    }

    void OnLose()
    {
        FindObjectOfType<AudioManager>().Play("DefeatScreen");
        DefeatScreen.SetActive(true);
    }

    void OnTie()
    {
        FindObjectOfType<AudioManager>().Play("DefeatScreen");
        TieScreen.SetActive(true);
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

    int TestingRotatedBoard(List<List<GameObject>> aBoard)
    {
        int aGameResult;

        List<List<GameObject>> anotherBoard = new List<List<GameObject>>();

        foreach (List<GameObject> gameObjectLists in aBoard) //CREATING DUPLICATE OF CURRENT BOARD
        {
            List<GameObject> tmpList = new List<GameObject>();

            foreach (GameObject aGameObject in gameObjectLists)
            {
                GameObject Tile = Instantiate(tile, new Vector3(100, 100, 0), Quaternion.identity);
                Tile.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
                Tile.GetComponent<IfIveBeenClicked>().type = aGameObject.GetComponent<IfIveBeenClicked>().type;
                tmpList.Add(Tile);
            }

            anotherBoard.Add(tmpList);
        } //CREATING DUPLICATE OF CURRENT BOARD END

        //ROTATING BOARD
        tmp2 = anotherBoard[rP[0][0]][rP[0][1]];

        for (int i = 0; i < 8; i++)
        {
            tmp1 = anotherBoard[rP[i + 1][0]][rP[i + 1][1]];

            anotherBoard[rP[i + 1][0]][rP[i + 1][1]] = tmp2;

            tmp2 = tmp1;
        }
        //ROTATING BOARD END

        aGameResult = WinDetection(anotherBoard, false, true);

        DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);

        return aGameResult;
    }

    int WinDetection(List<List<GameObject>> aBoard, bool realCheck, bool simple = false)
    {
        bool XHasWon = false;
        bool OHasWon = false;
        int gameResult;

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
                OnTie();
            }
            else
            {
                return 2;
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
                if (simple == true)
                {
                    gameResult = TestingRotatedBoard(board);
                    return gameResult;
                }
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
                    OnTie();
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
        TieScreen.SetActive(false);

        turnCounter = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i][j].GetComponent<IfIveBeenClicked>().type = 0;
                board[i][j].GetComponent<IfIveBeenClicked>().ResetSprite();

            }
        }

        runGame = true;
    }

    public void DeleteAllTheThingsInThisListOfListOfGameObjects(List<List<GameObject>> aListOfListsOfGameObjects)
    {
        foreach (List<GameObject> gameObjectLists in aListOfListsOfGameObjects)
        {
            foreach (GameObject aGameObject in gameObjectLists)
            {
                Destroy(aGameObject);
            }
        }
    }


    public List<int> ticTacToeAI(List<List<GameObject>> aBoard)
    {

        List<int> coordsToPlaceO = new List<int>();

        List<List<GameObject>> anotherBoard = new List<List<GameObject>>();



        foreach (List<GameObject> gameObjectLists in aBoard) //CREATING DUPLICATE OF CURRENT BOARD
        {
            List<GameObject> tmpList = new List<GameObject>();

            foreach (GameObject aGameObject in gameObjectLists)
            {
                GameObject Tile = Instantiate(tile, new Vector3(100, 100, 0), Quaternion.identity);
                Tile.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
                Tile.GetComponent<IfIveBeenClicked>().type = aGameObject.GetComponent<IfIveBeenClicked>().type;
                tmpList.Add(Tile);
            }

            anotherBoard.Add(tmpList);
        } //CREATING DUPLICATE OF CURRENT BOARD END

        DebugLogBoard(anotherBoard);

        Debug.Log("==============");


        int gameResult;

        //ROTATING BOARD
        tmp2 = anotherBoard[rP[0][0]][rP[0][1]];

        for (int i = 0; i < 8; i++)
        {
            tmp1 = anotherBoard[rP[i + 1][0]][rP[i + 1][1]];

            anotherBoard[rP[i + 1][0]][rP[i + 1][1]] = tmp2;

            tmp2 = tmp1;
        }
        //ROTATING BOARD END




        DebugLogBoard(anotherBoard);

        Debug.Log("+++++++++++++");





        int index = -1;
        int before;

        for (int num = 2; num > 0; num--)
        { //LOOPING THROUGH BOTH Xs AND Os - TWISTED
            Debug.Log("Placing " + num + "s rn");

            //LOOPING THROUGH EVERY POSSIBLE BOARD POSITION
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 7; k++) //FINDING WHAT INDEX THIS (i,j) POSITION IS IN the "rP" list
                    {
                        if (rP[k][0] == i && rP[k][1] == j)
                        {
                            index = k;
                            break;
                        }
                    } //END OF THAT



                    if (board[i][j].GetComponent<IfIveBeenClicked>().type == 0)
                    {
                        before = anotherBoard[rP[index + 1][0]][rP[index + 1][1]].GetComponent<IfIveBeenClicked>().type;
                        anotherBoard[rP[index + 1][0]][rP[index + 1][1]].GetComponent<IfIveBeenClicked>().type = num; //PLACING AT THE ROTATED POSITION OF CURRENT INDEX TO CHECK IT

                        gameResult = WinDetection(anotherBoard, false);


                        Debug.Log(",.,.,.,.,.,.,.");

                        DebugLogBoard(anotherBoard);

                        Debug.Log(",.,.,.,.,.,.,.");


                        if (gameResult == num)
                        {
                            Debug.Log("I found a " + num + "win so I'm going in - TWISTED");
                            Debug.Log(";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
                            DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                            return new List<int>() { i, j };
                        }

                        anotherBoard[rP[index + 1][0]][rP[index + 1][1]].GetComponent<IfIveBeenClicked>().type = before; //THIS MIGHT CAUSE AN ERROR WHERE O REPLACES AN X TO WIN THE GAME
                    }
                }
            }
        } //LOOPING THROUGH BOTH Xs and Os END - TWISTED




        DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);

        anotherBoard = new List<List<GameObject>>(); //REMAKING BOARD FOR NORMAL WIN CHECKING ON Xs


        foreach (List<GameObject> gameObjectLists in aBoard) //REMAKING DUPLICATE OF CURRENT BOARD
        {
            List<GameObject> tmpList = new List<GameObject>();

            foreach (GameObject aGameObject in gameObjectLists)
            {
                GameObject Tile = Instantiate(tile, new Vector3(100, 100, 0), Quaternion.identity);
                Tile.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
                Tile.GetComponent<IfIveBeenClicked>().type = aGameObject.GetComponent<IfIveBeenClicked>().type;
                tmpList.Add(Tile);
            }

            anotherBoard.Add(tmpList);
        } //REMAKING DUPLICATE OF CURRENT BOARD END


        for (int num = 2; num > 0; num--)
        { //LOOPING THROUGH BOTH Xs AND Os - NORMAL
            //Debug.Log(num);

            //LOOPING THROUGH EVERY POSSIBLE BOARD POSITION
            if (unfair == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i][j].GetComponent<IfIveBeenClicked>().type == 0)
                        {
                            anotherBoard[i][j].GetComponent<IfIveBeenClicked>().type = num;

                            gameResult = WinDetection(anotherBoard, false);



                            if (gameResult == num)
                            {
                                //Debug.Log("-------");
                                //DebugLogBoard(anotherBoard);
                                //Debug.Log("-------");

                                

                                Debug.Log("I found a " + num + " win so I'm going in - NORMAL");
                                if (toTwistOrNotToTwist == true)
                                {
                                    for (int k = 0; k < 7; k++) //FINDING WHAT INDEX THIS (i,j) POSITION IS IN the "rP" list
                                    {
                                        if (rP[k][0] == i && rP[k][1] == j)
                                        {
                                            index = k;
                                            break;
                                        }
                                    } //END OF THAT

                                    Debug.Log(index);
                                    if (index != 0)
                                    {
                                        if (board[rP[index - 1][0]][rP[index - 1][1]].GetComponent<IfIveBeenClicked>().type == 0)
                                        {
                                            DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                                            return new List<int>() { rP[index - 1][0], rP[index - 1][1] };
                                        }
                                    }
                                    else
                                    {
                                        if (board[rP[7][0]][rP[7][1]].GetComponent<IfIveBeenClicked>().type == 0)
                                        {
                                            DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                                            return new List<int>() { rP[7][0], rP[7][1] };
                                        }
                                    }
                                }
                                else
                                {
                                    DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                                    return new List<int>() { i, j };
                                }
                            }

                            anotherBoard[i][j].GetComponent<IfIveBeenClicked>().type = 0;
                        }
                    }
                }
            }
        } //LOOPING THROUGH BOTH Xs and Os END - NORMAL
        


        //ROTATING BOARD FOR THE REST OF THE OPTION DETECTIONS
        tmp2 = anotherBoard[rP[0][0]][rP[0][1]];

        for (int i = 0; i < 8; i++)
        {
            tmp1 = anotherBoard[rP[i + 1][0]][rP[i + 1][1]];

            anotherBoard[rP[i + 1][0]][rP[i + 1][1]] = tmp2;

            tmp2 = tmp1;
        }
        //ROTATING BOARD END


        //IN THE CASE NO WINS ARE FOUND ->
        if (turnCounter == 1 && anotherBoard[1][1].GetComponent<IfIveBeenClicked>().type == 0) //GO MIDDLE IF YOU CAN
        {
            DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
            return new List<int>() { 1, 1 };
        }
        else if (turnCounter == 1) //IF MIDDLE IS NOT AVAIBLE, FOLLOW 1 ROTAION BEHIND THE LAST X
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j].GetComponent<IfIveBeenClicked>().type == 1)
                    {
                        if (i == 1 && j == 0)
                        {
                            DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                            return new List<int>() { rP[6][0], rP[6][1] };
                        }

                        for (int k = 0; k < 7; k++) //FINDING WHAT INDEX THIS (i,j) POSITION IS IN the "rP" list
                        {
                            if (rP[k][0] == i && rP[k][1] == j)
                            {
                                index = k;
                                break;
                            }
                        } //END OF THAT



                        DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                        Debug.Log(index);
                        if (index > 2)
                        {
                            return new List<int>() { rP[index - 3][0], rP[index - 3][1] };
                        }
                        else if (index == 2)
                        {
                            return new List<int>() { rP[7][0], rP[7][1] };
                        }
                        else if (index == 1)
                        {
                            return new List<int>() { rP[6][0], rP[6][1] };
                        }
                        else if (index == 0)
                        {
                            return new List<int>() { rP[5][0], rP[5][1] };
                        }
                    }
                }
            }
        }
        else
        {

            //OPTIMAL POSITION FINDER NEEDS TO BE REMADE JUST FOLLOW 2nd X PLACEMENT

            Debug.Log("Optimal Position?");

            DebugLogBoard(board);

            Debug.Log("[[[[[[[[[[[[[[[[]]]]]]]]]]]]");

            DebugLogBoard(beforeBoard);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (beforeBoard[i][j] != board[i][j])
                    {
                        for (int k = 0; k < 7; k++) //FINDING WHAT INDEX THIS (i,j) POSITION IS IN the "rP" list
                        {
                            if (rP[k][0] == i && rP[k][1] == j)
                            {
                                index = k;
                                break;
                            }
                        } //END OF THAT

                        Debug.Log("Found difference");

                        if (index > 1)
                        {
                            if (board[rP[index - 2][0]][rP[index - 2][1]].GetComponent<IfIveBeenClicked>().type == 0)
                            {
                                DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                                return new List<int>() { rP[index - 2][0], rP[index - 2][1] };
                            }
                        }
                        else if (index == 1)
                        {
                            if (board[rP[7][0]][rP[7][1]].GetComponent<IfIveBeenClicked>().type == 0)
                            {
                                DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                                return new List<int>() { rP[7][0], rP[7][1] };
                            }
                        }
                        else if (index == 0)
                        {
                            if (board[rP[6][0]][rP[6][1]].GetComponent<IfIveBeenClicked>().type == 0)
                            {
                                DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                                return new List<int>() { rP[6][0], rP[6][1] };
                            }
                        }
                    }
                }
            }

            //IF NO OPTIMAL POSITIONS CAN BE FOUND USING ANY OF THE ABOVE METHODS USE THIS ->

            Debug.Log("Going a random spot");


            bool any0s = false;

            //FIND ANY TILE LEFT OPEN AND PLACE THERE
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j].GetComponent<IfIveBeenClicked>().type == 0)
                    {
                        any0s = true;
                        DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                        print(i + " and " + j);
                        return new List<int>() { i, j };
                    }
                }
            }

            if (any0s == false)
            {
                WinDetection(board, true);
                print("This thingy rn");
                DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                return new List<int>() { 0, 100 };
            }
        }

        DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
        return coordsToPlaceO; //Never used?
    }



    public static bool currentlyRotating = false;

    private GameObject tmp1, tmp2;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("GoldenDoodleMusicBackground");
        for (int i = 0; i < 3; i++)
        {
            List<GameObject> row = new List<GameObject>();

            for (int j = 0; j < 3; j++)
            {
                GameObject Tile = Instantiate(tile);
                Vector3 tmpPos = new Vector3((j + distanceBetweenTiles * j) - xBoardOffset, (-i - distanceBetweenTiles * i) - yBoardOffset);
                Tile.transform.SetParent(FindObjectOfType<Canvas>().transform, false);

                Tile.GetComponent<IfIveBeenClicked>().rotationBool = true;
                Tile.GetComponent<IfIveBeenClicked>().tmpPos = tmpPos;
                

                row.Add(Tile);
            }

            board.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (turnCounter % 2 == 1 && twoPlayer == false && runGame == true)
        {
            //DebugLogBoard(board);

            //Debug.Log("^ Before-----After v");


            aiCoordsToPlaceOAt = ticTacToeAI(board);

            if (aiCoordsToPlaceOAt[1] != 100)
            {
                board[aiCoordsToPlaceOAt[0]][aiCoordsToPlaceOAt[1]].GetComponent<IfIveBeenClicked>().type = 2;
                board[aiCoordsToPlaceOAt[0]][aiCoordsToPlaceOAt[1]].GetComponent<IfIveBeenClicked>().HasChanged = false;

                FindObjectOfType<AudioManager>().Play("PlaceSock");

                StartCoroutine(pauseGame(.5f));


                //OVER THE SPAN OF A COUPLE SECONDS, ANIMATE THE PLACING OF THE PIECE AT THE POSITION


                currentlyRotating = true;

                if (unfair == true)
                {
                    WinDetection(board, true);
                }


                turnCounter++;
            }





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
                tmp2 = board[rP[0][0]][rP[0][1]];

                for (int i = 0; i < 8; i++)
                {
                    //Debug.Log(i);

                    tmp1 = board[rP[i + 1][0]][rP[i + 1][1]];

                    board[rP[i + 1][0]][rP[i + 1][1]] = tmp2;

                    tmp2 = tmp1;
                }

                DeleteAllTheThingsInThisListOfListOfGameObjects(beforeBoard);
                beforeBoard = new List<List<GameObject>>();

                foreach (List<GameObject> gameObjectLists in board) //CREATING DUPLICATE OF CURRENT BOARD
                {
                    List<GameObject> tmpList = new List<GameObject>();

                    foreach (GameObject aGameObject in gameObjectLists)
                    {
                        GameObject Tile = Instantiate(tile, new Vector3(10000, 10000, 0), Quaternion.identity);
                        Tile.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
                        Tile.GetComponent<IfIveBeenClicked>().type = aGameObject.GetComponent<IfIveBeenClicked>().type;
                        tmpList.Add(Tile);
                    }

                    beforeBoard.Add(tmpList);
                } //CREATING DUPLICATE OF CURRENT BOARD END



                //EXAMPLE OF WHAT THE ABOVE FOR LOOP DOES ->

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

                        Vector3 tmpPos = new Vector3((j + distanceBetweenTiles * j) - xBoardOffset, (-i - distanceBetweenTiles * i) - yBoardOffset);

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


        if (runGame == true)
        {
            if (turnCounter % 2 == 1)
            {
                if (unfair == true)
                {
                    //DebugLogBoard();
                    WinDetection(board, true);
                }
                else if (turnCounter >= 9)
                {
                    WinDetection(board, true);
                }
            }
            else
            {
                WinDetection(board, true);
            }
        }

    }

    //StartCoroutine(pauseGame(.2f));
    private IEnumerator pauseGame(float secondsToPause){
        runGame = false;
        yield return new WaitForSeconds(secondsToPause);
        runGame = true;
    }

}
















//OLD AI CODE


/*
     int gameResult;

     List <int> coordsToPlaceO = new List<int>();

     List<List<GameObject>> anotherBoard = new List<List<GameObject>>();



     foreach (List<GameObject> gameObjectLists in aBoard)
     {
         List<GameObject> tmpList = new List<GameObject>();

         foreach (GameObject aGameObject in gameObjectLists)
         {
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
     int index = -1;
     int before;





     for (int num = 1; num < 3; num++)
     {
         Debug.Log("Placing " + num + "s rn");

         for (int i = 0; i < 3; i++)
         {
             for (int j = 0; j < 3; j++)
             {

                 Debug.Log(i + "," + j);

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



                     if (index == -1)
                     {
                         before = anotherBoard[1][1].GetComponent<IfIveBeenClicked>().type;
                         anotherBoard[1][1].GetComponent<IfIveBeenClicked>().type = num;
                     }
                     else
                     {
                         before = anotherBoard[rP[index][0]][rP[index][1]].GetComponent<IfIveBeenClicked>().type;
                         anotherBoard[rP[index][0]][rP[index][1]].GetComponent<IfIveBeenClicked>().type = num;
                     }



                     Debug.Log("[[[[[[[[[[[[[[[[[[[[");
                     DebugLogBoard(anotherBoard);
                     Debug.Log("[[[[[[[[[[[[[[[[[[[[");




                     gameResult = WinDetection(anotherBoard, false);

                     if (gameResult == num)
                     {
                         if (index == 0)
                         {

                             Debug.Log("Found an " + num + "win with " + rP[7][0] + "," + rP[7][1]);


                             coordsToPlaceO = new List<int>() { rP[7][0], rP[7][1] };

                             if (board[coordsToPlaceO[0]][coordsToPlaceO[1]].GetComponent<IfIveBeenClicked>().type != 0)
                             {
                                 coordsToPlaceO = new List<int>();
                             }
                             else
                             {
                                 DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);

                                 return coordsToPlaceO;
                             }
                         }
                         else if (index == -1)
                         {
                             Debug.Log("Found an " + num + "win with " + 1 + "," + 1);


                             coordsToPlaceO = new List<int>() { 1, 1 };

                             if (board[coordsToPlaceO[0]][coordsToPlaceO[1]].GetComponent<IfIveBeenClicked>().type != 0)
                             {
                                 coordsToPlaceO = new List<int>();
                             }
                             else
                             {
                                 DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);

                                 return coordsToPlaceO;
                             }
                         }
                         else
                         {

                             Debug.Log("Found an " + num + "win with " + rP[index][0] + "," + rP[index][1]);


                             coordsToPlaceO = new List<int>() { rP[index - 1][0], rP[index - 1][1] };

                             if (board[coordsToPlaceO[0]][coordsToPlaceO[1]].GetComponent<IfIveBeenClicked>().type != 0)
                             {
                                 coordsToPlaceO = new List<int>();
                             }
                             else
                             {
                                 DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);

                                 return coordsToPlaceO;
                             }
                         }
                     }

                     if (index == 0)
                     {
                         anotherBoard[rP[0][0]][rP[0][1]].GetComponent<IfIveBeenClicked>().type = before;
                     }
                     else if (index == -1)
                     {
                         anotherBoard[1][1].GetComponent<IfIveBeenClicked>().type = before;
                     }
                     else
                     {
                         anotherBoard[rP[index][0]][rP[index][1]].GetComponent<IfIveBeenClicked>().type = before;
                     }

             }
         }
     }
 }






     //Random.Range(0, waves.Count)
     if (turnCounter == 1 && board[1][1].GetComponent<IfIveBeenClicked>().type == 0)
     {
         return new List<int>() {1, 1};






         for (int i = 0; i < 3; i++)
         {
             for (int j = 0; j < 3; j++)
             {
                 if (anotherBoard[i][j].GetComponent<IfIveBeenClicked>().type == 1)
                 {
                     if (i == 0 && j == 1)
                     {
                         coordsToPlaceO.Add(rP[7][0]);
                         coordsToPlaceO.Add(rP[7][1]);
                         DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                         return coordsToPlaceO;
                     }
                     else
                     {
                         for (int k = 0; k < 7; k++)
                         {
                             if (rP[k][0] == i && rP[k][1] == j)
                             {
                                 index = k;
                                 Debug.Log(index);
                                 break;
                             }
                         }

                         coordsToPlaceO.Add(rP[index - 2][0]);
                         coordsToPlaceO.Add(rP[index - 2][1]);
                         DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                         return coordsToPlaceO;
                     }
                 }
             }
         }

     }


     Debug.Log("Didn't find any wins so I'm winging it");


     List<List<int>> anotherTmpList = new List<List<int>>();

     foreach (List<int> coords in corners)
     {
         if (board[coords[0]][coords[1]].GetComponent<IfIveBeenClicked>().type == 0)
         {
             anotherTmpList.Add(coords);
         }
     }

     if (anotherTmpList.Count != 0)
     {
         coordsToPlaceO = anotherTmpList[Random.Range(0, anotherTmpList.Count)];
     }


     //IF NO CORNERS LEFT RANDOMLY SELECT A REMAINING TILE
     if (coordsToPlaceO.Count == 0)
     {
         for (int i = 0; i < 3; i++)
         {
             for (int j = 0; j < 3; j++)
             {
                 if (board[i][j].GetComponent<IfIveBeenClicked>().type == 0)
                 {
                     coordsToPlaceO.Add(i);
                     coordsToPlaceO.Add(j);
                     DeleteAllTheThingsInThisListOfListOfGameObjects(anotherBoard);
                     return coordsToPlaceO;
                 }
             }
         }
     }
     */