using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    public GameObject placeholderDots;
    public FindMatches findMatches;
    public MoveCounter moveCounter;
    public CounterHolder counterHolder;
    public ActivateSettings activateSettings;
    public GoToNewScene GoToNewScene;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public GameObject francois;
    public int coCount;
    public int startingMoves;
    public float blockChance;
    private int blocksInPlay;
    public int maxBlocks;
    public bool advanced;
    public bool tutorialEnabled;
    public TileMatchingTriggers tutorialSystem;
    public GameObject alternateLoseScreen;


    public void TSR()
    {
        print("previous time scale: " + Time.timeScale);
        Time.timeScale = 1f;
        print("new time scale: " + Time.timeScale);
    }

    // Start is called before the first frame update
    void Start()
    {
        //LoadByJSON();
        RegisterAdvanced();
        Time.timeScale = 1;
        SetUp();
    }

    
    private void SetUp(){
        if(advanced == false){
            startingMoves = 12;
            moveCounter.Reset();
        }
        allDots = new GameObject[width,height];
        if(tutorialEnabled == true && advanced == false){
            print("got here");
            SetPresetLayout();
        } else{
            for(int i = 0; i < width; i++){
                for(int j = 0; j < height; j++){
                    SpawnDot(j,i,true);
                }
            }
        }
        if(IsDeadlocked()){
            ShuffleBoard();
        }
        activateSettings.gameRunning = true;

        if (tutorialEnabled == true){
            tutorialSystem.Setup();
        }
        
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
                if(allDots[column,row].tag == "Block Dot"){
                    List<int> exclude = new List<int>();
                    List<string> temp = counterHolder.EmptyCounters();
                    for(int i = 0; i < dots.Length; i++){
                        if(dots[i].tag == "Block Dot"){
                            exclude.Add(i);
                        }
                        else if(temp.IndexOf(dots[i].tag) != -1){
                            exclude.Add(i);
                        }
                    }
                    if(exclude.Count != dots.Length){
                        for(int i = 0; i < 100; i++){
                            int r = Random.Range(0,dots.Length-1);
                            if(exclude.IndexOf(r) == -1)
                            {
                                Debug.Log("Removing match of " + dots[r].tag);
                                counterHolder.addMatch(dots[r].tag,-1);
                                i = 100;
                            }
                        }
                    }
                } else {
                    counterHolder.addMatch(allDots[column,row].tag,1);
                }
                findMatches.currentMatches.Remove(allDots[column,row]);
                Destroy(allDots[column,row]);
                allDots[column,row] = null;
            }
        }
    }

    public void SetMatched(){
        tutorialSystem.clearedMatch = true;
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
            RegisterTutorial();
            GoToNewScene.GoToScene("RefactoredCutscenes");
        francois.SetActive(false);
        Debug.Log("you won " + counterHolder.counterArray);
        }
    }

    private void loseGame(){
        if(currentState != GameState.win && currentState != GameState.lose){
        currentState = GameState.lose;
        if (CheckForAlternateLoseScreen()) {alternateLoseScreen.SetActive(true);} else {defeatScreen.SetActive(true);}
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
            if(IsDeadlocked())
            {
                ShuffleBoard();
            }
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

            
            bool retry = false;
            int dotToUse;
            
            do{
                retry = false;
                dotToUse = Random.Range(0, dots.Length);

                if (noMatches)
                {
                    int maxIterations = 0;
                    while (MatchesAt(col, row, dots[dotToUse]) && maxIterations < 100)
                    {
                        dotToUse = Random.Range(0, dots.Length);
                        maxIterations++;
                    }
                }
                if(dots[dotToUse].tag == "Block Dot"){
                    float prob = Random.Range(1,100);
                    prob /= 100;
                    if (prob >= blockChance || advanced == false || blocksInPlay == maxBlocks){
                        retry = true;
                    } else{
                        blocksInPlay++;
                    }
                }
            } while(retry == true);

            GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
            allDots[col, row] = piece;
            piece.GetComponent<Dot>().row = row;
            piece.GetComponent<Dot>().column = col;
            //piece.transform.parent = this.transform;
            piece.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
            piece.transform.position = tempPosition;
            piece.name = col + ", " + row;
            piece.transform.SetParent(FindObjectOfType<Board>().transform, true);
            //Debug.Log("" + xPos + " " + yPos + " " + piece.name);
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

    public int[] FindSet(){
        int[] matchSet = new int[6];
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
                            matchSet[0] = i;
                            matchSet[1] = j;
                            matchSet[2] = i+1;
                            matchSet[3] = j;
                            matchSet[4] = i+2;
                            matchSet[5] = j;
                            return matchSet;
                        }
                    }
                    if(j < height-1){
                        if(SwitchAndCheck(i,j,Vector2.up))
                        {
                            matchSet[0] = i;
                            matchSet[1] = j;
                            matchSet[2] = i;
                            matchSet[3] = j+1;
                            matchSet[4] = i;
                            matchSet[5] = j+2;
                            return matchSet;
                        }
                    }
                }
            }
        }
        return matchSet;
    }

    public int[] GetSet(){
        int[] matchSet = new int[6];
        matchSet[0] = 4;
        matchSet[1] = 3;
        matchSet[2] = 4;
        matchSet[3] = 4;
        matchSet[4] = 5;
        matchSet[5] = 2;
        return matchSet;
    }

    private bool SwitchAndCheck(int column, int row, Vector2 direction){
        if(allDots[column,row].tag == "Block Dot"){
            return false;
        }
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

    private void ShuffleBoard()
    {
        List<GameObject> newBoard = new List<GameObject>();
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(allDots[i,j]!=null)
                {
                    newBoard.Add(allDots[i,j]);
                }
            }
        }
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                int pieceToUse = Random.Range(0, newBoard.Count);
                Dot piece = newBoard[pieceToUse].GetComponent<Dot>();
                piece.column = i;
                piece.row = j;
                allDots[i,j] = newBoard[pieceToUse];
                newBoard.Remove(newBoard[pieceToUse]);
            }
        }
        if(IsDeadlocked() || CheckForMatches()){
            ShuffleBoard();
        }
    }

    public void SetPresetLayout(){
        int count = 0;
        placeholderDots.SetActive(true);
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                allDots[i,j] = placeholderDots.transform.GetChild(count).gameObject;
                count++;
            }
        }

        Time.timeScale = 1;
        //activateSettings.gameRunning = true;
    }

    private void LoadFromPlayerData(PlayerData tempData)
    {
        int cutsceneNum = tempData.sceneNumber;
        if(cutsceneNum == 2){
            advanced = true;
        }
    }

    //This was originally private, I made it public so I could access it in other scripts. Was there a reason for making it private?
    //A: Mostly if we wanted another LoadByJSON script for a different save class. Ask me about it later FIXME: Delete if you understand
    public void LoadByJSON()
    {
        if(!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"))){
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"));
        }
        if (File.Exists(Application.persistentDataPath+"/Saves/JSONData.text"))
        {
            //LOAD THE GAME
            StreamReader sr = new StreamReader(Application.persistentDataPath+"/Saves/JSONData.text");

            string JsonString = sr.ReadToEnd();

            sr.Close();

            //Convert JSON to the Object(PlayerData)
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(JsonString);

            LoadFromPlayerData(playerData);

        }
        else
        {
            Debug.Log("NOT FOUND FILE");
        }
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
