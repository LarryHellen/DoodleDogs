using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    wait,
    move,
    win,
    lose
}
public class Board : MonoBehaviour
{

    public GameState currentState = GameState.move;
    public int width;
    public int height;
    public float xSpawn;
    public float ySpawn;
    public float xDistance;
    public float yDistance;
    public GameObject tilePrefab;
    public GameObject[] dots;
    public GameObject[,] allDots;
    private FindMatches findMatches;
    private MoveCounter moveCounter;
    [HideInInspector]
    public CounterHolder counterHolder;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public GameObject francois;
    private int coCount;
    public int startingMoves;

    

    // Start is called before the first frame update
    void Start()
    {
        findMatches = FindObjectOfType<FindMatches>();
        moveCounter = FindObjectOfType<MoveCounter>();
        allDots = new GameObject[width,height];
        counterHolder = FindObjectOfType<CounterHolder>();
        SetUp();
    }

    
    private void SetUp(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                SpawnDot(j,i,true);
            }
        }
        CheckDeadlock();
    }

    private void CheckDeadlock()
    {
        currentState = GameState.wait;
        if (IsDeadlocked())
        {
            Debug.Log("Deadlocked, resetting game board");
            ResetGame();
        }
        currentState = GameState.move;
    }

    private bool MatchesAt(int column, int row, GameObject piece){
        if(column > 1 && row > 1){
            if(allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag){
                return true;
            }
            if(allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag){
                return true;
            }
        }else if (column <= 1 || row <= 1){
            if(row > 1){
                if(allDots[column,row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag){
                    return true;
                }
            }
            if(column > 1){
                if(allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag){
                    return true;
                }
            }
        }

        return false;
    }

    private void DestroyMatchesAt(int column, int row){
        if(currentState != GameState.win && currentState != GameState.lose){
            if(allDots[column,row].GetComponent<Dot>().isMatched){
                FindObjectOfType<AudioManager>().Play("MatchSuccess");
                counterHolder.addMatch(allDots[column,row].tag,1);
                findMatches.currentMatches.Remove(allDots[column,row]);
                Destroy(allDots[column,row]);
                allDots[column,row] = null;
            }
        }
    }

    public void DestroyMatches(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(allDots[i,j] != null){
                    DestroyMatchesAt(i,j);
                }
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo(){
        int nullCount = 0;
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(allDots[i,j] == null){
                    nullCount++;
                } else if(nullCount > 0){
                    allDots[i,j].GetComponent<Dot>().row -= nullCount;
                    allDots[i,j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(allDots[i,j] == null){
                    SpawnDot(j,i,false);
                }
            }
        }
    }

    private bool MatchesOnBoard(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(allDots[i,j]!=null){
                    if(allDots[i,j].GetComponent<Dot>().isMatched){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void endGame(){
        if(currentState != GameState.win && currentState != GameState.lose){
            if(counterHolder.metAllGoals()){
                winGame();
            } else{
                loseGame();
            }
        }
    }

    public void winGame(){
        if(currentState != GameState.win && currentState != GameState.lose){
        currentState = GameState.win;
        victoryScreen.SetActive(true);
        francois.SetActive(false);
        Debug.Log("you won");
        }
    }

    private void loseGame(){
        if(currentState != GameState.win && currentState != GameState.lose){
        currentState = GameState.lose;
        defeatScreen.SetActive(true);
        francois.SetActive(false);
        Debug.Log("you lose");
        }
    }

    public void ResetGame(){
        foreach(GameObject dot in allDots){
            Destroy(dot);
        }
        counterHolder.Reset();
        moveCounter.Reset();
        defeatScreen.SetActive(false);
        francois.SetActive(true);
        currentState = GameState.move;
        SetUp();
    }

    private IEnumerator FillBoardCo() {
        coCount++;
        do {
            RefillBoard();
            yield return new WaitForSeconds(.5f);
            findMatches.FindAllMatches();
            while (MatchesOnBoard()) {
                DestroyMatches();
                findMatches.FindAllMatches();
                yield return new WaitForSeconds(.5f);
            }
            //Debug.Log(findMatches.currentMatches.Count);
            yield return new WaitForSeconds(.3f);
        } while (findMatches.currentMatches.Count != 0);
        coCount--;
        if (coCount == 0)
        {
            currentState = GameState.move;
        }
        
        if(counterHolder.metAllGoals()){
            endGame();
        }
    }

    public void StartFillBoardCoroutine(){
        StartCoroutine(FillBoardCo());
    }

    private void SpawnDot(int row, int col, bool noMatches){
        if (allDots[col, row] == null)
        {
            FindObjectOfType<AudioManager>().Play("TileSpawn");
            float xPos = col * xDistance + xSpawn;
            float yPos = row * yDistance + ySpawn;
            Vector2 tempPosition = new Vector2(xPos, yPos);

            int dotToUse = Random.Range(0, dots.Length);

            if (noMatches)
            {
                int maxIterations = 0;
                while (MatchesAt(col, row, dots[dotToUse]) && maxIterations < 100)
                {
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;
                }
            }

            GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
            allDots[col, row] = piece;
            piece.GetComponent<Dot>().row = row;
            piece.GetComponent<Dot>().column = col;
            //piece.transform.parent = this.transform;
            piece.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
            piece.transform.position = tempPosition;
            piece.name = col + ", " + row;
            piece.transform.SetParent(FindObjectOfType<Board>().transform, true);
            Debug.Log("" + xPos + " " + yPos + " " + piece.name);
        }
    }

    private void SwitchPieces(int column, int row, Vector2 direction){
        GameObject holder = allDots[column + (int)direction.x,row + (int)direction.y] as GameObject;
        allDots[column + (int)direction.x,row + (int)direction.y] = allDots[column,row];
        allDots[column,row] = holder;
    }

    private bool CheckForMatches(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(allDots[i,j]!=null){
                    if(i < width - 2){
                        if(allDots[i+1,j]!=null && allDots[i+2,j]!=null){
                            if(allDots[i+1,j].tag == allDots[i,j].tag && allDots[i,j].tag == allDots[i+2,j].tag){
                                return true;
                            }
                        }
                    }
                    if(j < height - 2){
                        if(allDots[i,j+1] != null && allDots[i,j+2] != null){
                            if(allDots[i,j+1].tag == allDots[i,j].tag && allDots[i,j].tag == allDots[i,j+2].tag){
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    private bool SwitchAndCheck(int column, int row, Vector2 direction){
        SwitchPieces(column,row,direction);
        if(CheckForMatches()){
            SwitchPieces(column,row,direction);
            return true;
        }
        SwitchPieces(column,row,direction);
        return false;
    }

    private bool IsDeadlocked(){
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(allDots[i,j]!=null)
                {
                    if(i < width-1)
                    {
                        if(SwitchAndCheck(i,j,Vector2.right))
                        {
                            return false;
                        }
                    }
                    if(j < height-1){
                        if(SwitchAndCheck(i,j,Vector2.up))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
