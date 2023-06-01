using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public bool placingEnabled = true;
    public List<GameObject> boardPositions;
    [HideInInspector] public List<Tile> tiles = new List<Tile>();
    

    void Start()
    {
        InstantiateAndPositionTiles();
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
        Debug.Log("CompleteTurn");
        placingEnabled = false;

       
        turnCounter++; //Account for the player's turn
        
        if (advanced)
        {
            int selectedIndex = TwistAI();
            tiles[selectedIndex].SetSprite(turnCounter);
            turnCounter++; //Account for the AI's turn
            
            //Rotate the board
            RotateBoard();

            //Check for win or lose
            StartCoroutine(WaitUntilLerpOverToCheckForWinOrLose()); //The coroutine waits until placingEnabled gets set to true in the LerpTile coroutine to check for win or lose to ensure that all tiles have moved before the game ends

            //if advanced, placingEnabled gets set to true in the LerpTile coroutine to ensure that all tiles have been moved before gameplay resumes
        }
        else 
        {
            int selectedIndex = NormalAI();
            tiles[selectedIndex].SetSprite(turnCounter);
            turnCounter++; //Account for the AI's turn

            //Check for win or lose
            CheckForWinOrLose();

            placingEnabled = true;
        }
    }


    void CheckForWinOrLose()
    {
        Debug.Log("CheckForWinOrLose");

        bool? result = CheckBoard(GetIntegerListFromTileList(tiles)); //null means no one wins, true means player wins, false means AI wins
        if(result != null)
        {
            if ((bool) result)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }
    }


    int TwistAI()
    {
        Debug.Log("TwistAI");

        List<int> integerBoard = GetIntegerListFromTileList(tiles); //Remember to set end selected tile to the proper one, accounting for the fact the "tiles" list isn't top-down, left-right

        //DO AI Alpha Beta search which includes rotating the board, Recursion will probably be used, Check Ben Dms for more info

        return 0;
    }


    int NormalAI()
    {
        Debug.Log("NormalAI");

        List<int> integerBoard = GetIntegerListFromTileList(tiles); //Remember to set end selected tile to the proper one, accounting for the fact the "tiles" list isn't top-down, left-right

        //DO AI Alpha Beta search, Recursion will probably be used, Check Ben Dms for more info

        return 0;
    }


    bool? CheckBoard(List<int> integerBoard) //null means no one wins, true means player wins, false means AI wins
    {
        Debug.Log("CheckBoard");

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
                Debug.Log("AI wins");
                return false;
            }
            else if (wins[i].Count == 1 && wins[i].Contains(0))
            {
                Debug.Log("Player wins");
                return true;
            }
        }

        Debug.Log("No one wins");
        return null;
    }


    List<int> GetIntegerListFromTileList(List<Tile> tiles)
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
    }


    void Lose()
    {  
        Debug.Log("Lose");
    }


    public void Reset()
    {
        SceneManager.LoadScene("RefactoredTicTacToe");
    }


    void RotateBoard()
    {
        Debug.Log("RotateBoard");

        List<Tile> outsideRing = GetOutsideRing(tiles);

        outsideRing = RotateOutsideRing(outsideRing);

        TransformOutsideRing(outsideRing);

        UpdateTilesList(outsideRing);
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


    List<GameObject> GetOutsideRing(List<GameObject> boardPositions) //Make an overload of this for lists of ints - Cai (Note to Self)
    {
        List<GameObject> outsideRing = new List<GameObject>();
        outsideRing.AddRange(boardPositions.GetRange(0, 4)); //Gets indexes 0, 1, 2, 3
        outsideRing.AddRange(boardPositions.GetRange(5, 4)); //Gets indexes 5, 6, 7, 8

        return outsideRing;
    }


    List<Tile> RotateOutsideRing(List<Tile> rotatedOutsideRing) //Make an overload of this for lists of ints - Cai (Note to Self)
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
}