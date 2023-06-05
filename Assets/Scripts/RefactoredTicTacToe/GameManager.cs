using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour 
{
    [Header("Tile Settings")]
    [SerializeField] GameObject tileGameObjectPrefab;
    [SerializeField] Transform parentTransform;
    [SerializeField] List<Sprite> tileSprites;
    [SerializeField] float timeToMove;


    [Header("Game Settings")]
    public int turnCounter = 0;
    public bool advanced;
    [Range(0, 10)]
    public int RandomMoveChance;
    public float waitTimeBeforeEnd;
    public float waitToThinkTime;
    public bool placingEnabled = true;
    public List<GameObject> boardPositions;
    public TicTacToeTriggers tutorialObject;
    public bool tutorialEnabled;
    [HideInInspector] public List<Tile> tiles = new List<Tile>();


    [Header("GameObjects")]
    public GameObject loseScreen;
    public GameObject alternateLoseScreen;


    void Start()
    {
        RegisterAdvanced(); //Sets timeScale to 0 for some reason
        Time.timeScale = 1; //Fix for the above problem

        InstantiateAndPositionTiles();
        if (tutorialEnabled)
        {
            tutorialObject.Setup();
        }

        FindObjectOfType<AudioManager>().Play("GoldenDoodleMusicBackground");
    }


    void InstantiateAndPositionTiles()
    {
        for (int i = 0; i < boardPositions.Count; i++)
        {
            GameObject tileGameObject = Instantiate(tileGameObjectPrefab, parentTransform);
            tiles.Add(new Tile(tileSprites, tileGameObject));
            tiles[i].tileGameObject.transform.position = boardPositions[i].transform.position;
        }
    }


    public void CompleteTurn()
    {
        //Debug.Log("CompleteTurn");
        placingEnabled = false;
       
        turnCounter++; //Account for the player's turn
        
        if (advanced)
        {
            Debug.Log("Advanced");
            AIMove();

            //Rotate the board
            StartCoroutine(WaitThenRotate());

            //Check for win or lose
            StartCoroutine(WaitUntilLerpOverToCheckForWinOrLose()); //The coroutine waits until placingEnabled gets set to true in the LerpTile coroutine to check for win or lose to ensure that all tiles have moved before the game ends

            //if advanced, placingEnabled gets set to true in the LerpTile coroutine to ensure that all tiles have been moved before gameplay resumes
        }
        else 
        {
            Debug.Log("Not Advanced");
            AIMove();

            //Check for win or lose
            CheckForWinOrLose(); 
            placingEnabled = true; // Resume gameplay
        }
    }


    void CheckForWinOrLose()
    {
        //Debug.Log("CheckForWinOrLose");

        bool? result = CheckBoard(GetIntegerListFromTileList(tiles)); //null means no one wins, true means player wins, false means AI wins
        if(result != null)
        {
            if ((bool) result)
            {
                StartCoroutine(WaitThenWin());
            }
            else
            {
                StartCoroutine(WaitThenLose());
            }
        }
        else 
        {
            if (!GetIntegerListFromTileList(tiles).Contains(-1))
            {
                StartCoroutine(WaitThenLose());
            }
        }            
    }

    void AIMove()
    {
        if (turnCounter < 8)
        {
            int selectedIndex;
            if (Random.Range(1, RandomMoveChance+1) == 1)
            {
                selectedIndex = FindRandomMove();
            }
            else
            {
                selectedIndex = AISelectMove();
            }
            tiles[selectedIndex].SetSprite(turnCounter);
            FindObjectOfType<AudioManager>().Play("PlaceSock");
            turnCounter++; //Account for the AI's turn
        }
    }

    int AISelectMove()
    {
        Debug.Log("AISelectMove");
        List<int> integerBoard = GetIntegerListFromTileList(tiles);
        
        int aiTurnCounter = turnCounter;
        List<int> indexRatings = RepairBestMoves(AIFindBestMove(integerBoard, aiTurnCounter, true));
        Debug.Log("Index Ratings: ");
        PrintIntegerList(indexRatings);
        List<int> bestMoveIndexes = GetBestMovesFromIndexRatings(indexRatings);

        int selectedIndex = bestMoveIndexes[Random.Range(0, bestMoveIndexes.Count)];

        return selectedIndex;
    }


    List<int> GetBestMovesFromIndexRatings(List<int> indexRatings)
    {
        List<int> bestMoveIndexes = new List<int>();

        for (int i = 0; i < indexRatings.Count; i++)
        {
            if (indexRatings[i] == indexRatings.Max())
            {
                bestMoveIndexes.Add(i);
            }
        }

        return bestMoveIndexes;
    }


    List<int> RepairBestMoves(List<int> indexRatings)
    {
        List<int> newIndexRatings = new List<int>(indexRatings);
        List<int> board = GetIntegerListFromTileList(tiles);

        for (int i = 0; i < board.Count; i++)
        {
            if (board[i] != -1)
            {
                newIndexRatings[i] = int.MinValue;
            }
        }

        return newIndexRatings;
    }


    int FindRandomMove()
    {
        Debug.Log("FindRandomMove");
        List<int> board = GetIntegerListFromTileList(tiles);
        List<int> possibleMoves = new List<int>();

        for (int i = 0; i < board.Count; i++)
        {
            if (board[i] == -1)
            {
                possibleMoves.Add(i);
            }
        }

        int selectedIndex = possibleMoves[Random.Range(0, possibleMoves.Count)];

        return selectedIndex;
    }


    List<int> AIFindBestMove(List<int> integerBoard, int aiTurnCounter, bool topLevel=false) //Only checks the first position avaible each time, needs to be fixed
    {
        List<int> indexRatings = new List<int>(new int[9]);


        for (int i = 0; i < integerBoard.Count; i++)
        {
            if (integerBoard[i] == -1)
            {
                List<int> newIntegerBoard = new List<int>(integerBoard);
                newIntegerBoard[i] = aiTurnCounter % 2;

                if (aiTurnCounter % 2 == 0 && advanced)
                {
                    newIntegerBoard = RotateBoard(newIntegerBoard);
                }

                bool? result = CheckBoard(newIntegerBoard); //null means no one wins, true means player wins, false means AI wins
                if (result != null)
                {
                    if ((bool)result)
                    {
                        indexRatings[i] = -3;
                        return indexRatings;
                    }
                    else
                    {
                        indexRatings[i] = 4;
                        return indexRatings;
                    }
                }
                else
                {
                    if (!newIntegerBoard.Contains(-1))
                    {
                        indexRatings[i] = -2;
                        return indexRatings;
                    }
                }

                List<int> recursiveIndexRatings = AIFindBestMove(newIntegerBoard, aiTurnCounter+1);

                for (int j = 0; j < recursiveIndexRatings.Count; j++)
                {
                    indexRatings[i] += recursiveIndexRatings[j];
                }
            }
            else
            {
                if (topLevel)
                {
                    indexRatings[i] = int.MinValue;
                }
            }
        }

        return indexRatings;
    }


    bool? CheckBoard(List<int> integerBoard) //null means no one wins, true means player wins, false means AI wins
    {
        //Debug.Log("CheckBoard");

        integerBoard = GetTopToBottomLeftToRightIntegerListFromIntegerList(integerBoard);

        List<HashSet<int>> wins = new List<HashSet<int>>();

        HashSet<int> firstRow = new HashSet<int>(integerBoard.GetRange(0, 3));
        wins.Add(firstRow);
        HashSet<int> secondRow = new HashSet<int>(integerBoard.GetRange(3, 3));
        wins.Add(secondRow);
        HashSet<int> thirdRow = new HashSet<int>(integerBoard.GetRange(6, 3));
        wins.Add(thirdRow);

        HashSet<int> firstColumn = new HashSet<int>(new List<int> { integerBoard[0], integerBoard[3], integerBoard[6] });
        wins.Add(firstColumn);
        HashSet<int> secondColumn = new HashSet<int>(new List<int> { integerBoard[1], integerBoard[4], integerBoard[7] });
        wins.Add(secondColumn);
        HashSet<int> thirdColumn = new HashSet<int>(new List<int> { integerBoard[2], integerBoard[5], integerBoard[8] });
        wins.Add(thirdColumn);

        HashSet<int> leftDiag = new HashSet<int>(new List<int> { integerBoard[0], integerBoard[4], integerBoard[8] });
        wins.Add(leftDiag);
        HashSet<int> rightDiag = new HashSet<int>(new List<int> { integerBoard[2], integerBoard[4], integerBoard[6] });
        wins.Add(rightDiag);

        for (int i = 0; i < wins.Count; i++)
        {
            if (wins[i].Count == 1 && wins[i].Contains(1))
            {
                //Debug.Log("AI wins");
                return false;
            }
        }

        for (int i = 0; i < wins.Count; i++)
        {
            if (wins[i].Count == 1 && wins[i].Contains(0))
            {
                //Debug.Log("Player wins");
                return true;
            }
        }

        //Debug.Log("No one wins");
        return null;
    }


    public List<int> GetIntegerListFromTileList(List<Tile> tiles)
    {
        List<int> integerBoard = new List<int>();

        for (int i = 0; i < tiles.Count; i++)
        {
            integerBoard.Add(tiles[i].type);
        }

        return integerBoard;
    }


    List<int> GetTopToBottomLeftToRightIntegerListFromIntegerList(List<int> integerBoard)
    {
        List<int> tempIntegerBoard = new List<int>();

        tempIntegerBoard.Add(integerBoard[0]);
        tempIntegerBoard.Add(integerBoard[1]);
        tempIntegerBoard.Add(integerBoard[2]);

        tempIntegerBoard.Add(integerBoard[8]);
        tempIntegerBoard.Add(integerBoard[4]);
        tempIntegerBoard.Add(integerBoard[3]);

        tempIntegerBoard.Add(integerBoard[7]);
        tempIntegerBoard.Add(integerBoard[6]);
        tempIntegerBoard.Add(integerBoard[5]);

        return tempIntegerBoard;
    }


    List<int> GetNormalizedIntegerList(List<int> integerBoard) //Effectively puts the integer board into the form of the tiles in the "tiles" list
    {
        List<int> tempIntegerBoard = new List<int>();

        tempIntegerBoard.Add(integerBoard[0]);
        tempIntegerBoard.Add(integerBoard[1]);
        tempIntegerBoard.Add(integerBoard[2]);

        tempIntegerBoard.Add(integerBoard[5]);
        tempIntegerBoard.Add(integerBoard[4]);
        tempIntegerBoard.Add(integerBoard[8]);

        tempIntegerBoard.Add(integerBoard[7]);
        tempIntegerBoard.Add(integerBoard[6]);
        tempIntegerBoard.Add(integerBoard[3]);

        return tempIntegerBoard;
    }


    void Win()
    {
        Debug.Log("Win");
        FindObjectOfType<AudioManager>().Play("Scratch");
        RegisterTutorial();
        SceneManager.LoadScene("RefactoredCutscenes");
    }


    void Lose()
    {  
        Debug.Log("Lose");
        FindObjectOfType<AudioManager>().Play("DefeatScreen");
        if (CheckForAlternateLoseScreen()) {alternateLoseScreen.SetActive(true);} else {loseScreen.SetActive(true);}
    }


    IEnumerator WaitThenLose()
    {
        yield return new WaitForSeconds(waitTimeBeforeEnd);
        Lose();
    }


    IEnumerator WaitThenWin()
    {
        yield return new WaitForSeconds(waitTimeBeforeEnd);
        Win();
    }


    IEnumerator WaitThenRotate()
    {
        yield return new WaitForSeconds(waitToThinkTime);
        RotateBoard();
    }


    public void Reset()
    {
        SceneManager.LoadScene("RefactoredTicTacToe");
    }


    void RotateBoard()
    {
        //Debug.Log("RotateBoard");

        List<Tile> outsideRing = GetOutsideRing(tiles);

        outsideRing = RotateOutsideRing(outsideRing);

        TransformOutsideRing(outsideRing);

        UpdateTilesList(outsideRing);
    }

    List<int> RotateBoard(List<int> integerBoard)
    {
        //Debug.Log("INT - RotateBoard");
        
        List<int> outsideRing = GetOutsideRing(integerBoard);

        outsideRing = RotateOutsideRing(outsideRing);

        integerBoard = UpdateTilesList(outsideRing, integerBoard);
        
        return integerBoard;
    }


    void PrintTileList(List<Tile> tiles)
    {
        for (int i = 0; i < tiles.Count; i+=3)
        {
            Debug.Log(tiles[i].type + " " + tiles[i + 1].type + " " + tiles[i + 2].type);
        }
    }


    void PrintIntegerList(List<int> integerList)
    {
        for (int i = 0; i < integerList.Count; i+=3)
        {
            Debug.Log(integerList[i] + " " + integerList[i + 1] + " " + integerList[i + 2]);
        }
    }


    List<Tile> GetOutsideRing(List<Tile> tiles)
    {
        List<Tile> outsideRing = new List<Tile>();
        outsideRing.AddRange(tiles.GetRange(0, 4)); //Gets indexes 0, 1, 2, 3
        outsideRing.AddRange(tiles.GetRange(5, 4)); //Gets indexes 5, 6, 7, 8

        return outsideRing;
    }


    List<GameObject> GetOutsideRing(List<GameObject> boardPositions)
    {
        List<GameObject> outsideRing = new List<GameObject>();
        outsideRing.AddRange(boardPositions.GetRange(0, 4)); //Gets indexes 0, 1, 2, 3
        outsideRing.AddRange(boardPositions.GetRange(5, 4)); //Gets indexes 5, 6, 7, 8

        return outsideRing;
    }


    List<Tile> RotateOutsideRing(List<Tile> rotatedOutsideRing) 
    {
        Tile tempTile = rotatedOutsideRing[0];
        Tile otherTempTile;

        for (int i = 1; i < rotatedOutsideRing.Count; i++)
        {
            otherTempTile = rotatedOutsideRing[i];
            rotatedOutsideRing[i] = tempTile;
            tempTile = otherTempTile;
        }
        rotatedOutsideRing[0] = tempTile;

        return rotatedOutsideRing;
    }


    List<int> GetOutsideRing(List<int> boardPositions) 
    {
        List<int> outsideRing = new List<int>();
        outsideRing.AddRange(boardPositions.GetRange(0, 4)); //Gets indexes 0, 1, 2, 3
        outsideRing.AddRange(boardPositions.GetRange(5, 4)); //Gets indexes 5, 6, 7, 8

        return outsideRing;
    }


    List<int> RotateOutsideRing(List<int> rotatedOutsideRing) 
    {
        int tempTile = rotatedOutsideRing[0];
        int otherTempTile;

        for (int i = 1; i < rotatedOutsideRing.Count; i++)
        {
            otherTempTile = rotatedOutsideRing[i];
            rotatedOutsideRing[i] = tempTile;
            tempTile = otherTempTile;
        }
        rotatedOutsideRing[0] = tempTile;

        return rotatedOutsideRing;
    }


    void TransformOutsideRing(List<Tile> rotatedOutsideRing)
    {
        List<GameObject> outsideRingOfBoardPositions = GetOutsideRing(boardPositions);

        for (int i = 0; i < rotatedOutsideRing.Count; i++)
        {
            StartCoroutine(LerpTile(rotatedOutsideRing[i], outsideRingOfBoardPositions[i], timeToMove));
        }
    }


    IEnumerator LerpTile(Tile tile, GameObject target, float timeToMove)
    {
        float elapsedTime = 0;
        Vector3 startPosition = tile.tileGameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        while (elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;
            tile.tileGameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / timeToMove);
            yield return null;
        }

        placingEnabled = true; // Resume gameplay because all tiles have been moved. P.S. I know this happens 8 times lol
        tile.tileGameObject.transform.position = targetPosition;
    }


    IEnumerator WaitUntilLerpOverToCheckForWinOrLose()
    {
        yield return new WaitUntil(() => placingEnabled == true);
        CheckForWinOrLose();
    }


    void UpdateTilesList(List<Tile> outsideRing)
    {
        Tile centerTile = tiles[4];
        tiles = new List<Tile>(outsideRing);
        tiles.Insert(4, centerTile);
    }


    List<int> UpdateTilesList(List<int> outsideRing, List<int> integerBoard)
    {
        int centerTile = integerBoard[4];
        integerBoard = new List<int>(outsideRing);
        integerBoard.Insert(4, centerTile);

        return integerBoard;
    }


    private void RegisterAdvanced()
    {
        GameObject tutorialHandler = GameObject.Find("TutorialHandler");
        TutorialHandler tutorialHandlerScript = tutorialHandler.GetComponent<TutorialHandler>();

        List<bool> bools = tutorialHandlerScript.RegisterAdvanced();
        advanced = bools[0];
        tutorialEnabled = bools[1];
    }


    private void RegisterTutorial()
    {
        GameObject tutorialHandler = GameObject.Find("TutorialHandler");
        TutorialHandler tutorialHandlerScript = tutorialHandler.GetComponent<TutorialHandler>();

        tutorialHandlerScript.RegisterTutorial();
    }


    public bool CheckForAlternateLoseScreen()
    {
        JsonDataManipulation jsonDataManipulation = new JsonDataManipulation();
        jsonDataManipulation.LoadByJSON();

        if (jsonDataManipulation.currentChapter == 3)
        {
            return true;
        }

        return false;
    }
}