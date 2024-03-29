using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    public ActivateSettings ActivateSettings;
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public Board board;
    public float targetX;
    public float targetY;
    public bool isMatched = false;
    public FindMatches findMatches;
    public MoveCounter moveCounter;
    public GameObject otherDot;
    private Vector2 firstTouchPosition;
    public Vector2 finalTouchPosition;
    public Vector2 tempPosition;
    public float swipeAngle = 0;
    public float swipeResist;
    public bool isTutorial;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        findMatches = board.findMatches;
        moveCounter = board.moveCounter;
        ActivateSettings = board.activateSettings;
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;

    }

    

    // Update is called once per frame
    void Update()
    {
        if(board.currentState != GameState.win && board.currentState != GameState.lose)
        {
            if(isMatched){
                // SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
                // mySprite.color = new Color (1f,1f,1f,.2f);
                Image myImage = GetComponent<Image>();
                myImage.GetComponent<Image>().color = new Color (1f,1f,1f,.2f);
                //board.DestroyMatches();
            }

            targetX = column * board.xDistance + board.xSpawn;
            targetY = row * board.yDistance + board.ySpawn;
            if(Mathf.Abs(targetX - transform.position.x) > .1){
                //Move towards target
                tempPosition = new Vector2(targetX,transform.position.y);
                transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
                if(board.allDots[column,row] != this.gameObject){
                    board.allDots[column,row] = this.gameObject;
                }
                findMatches.FindAllMatches();
            }else{
                //Directly set the position
                tempPosition = new Vector2(targetX,transform.position.y);
                transform.position = tempPosition;
            }
            if(Mathf.Abs(targetY - transform.position.y) > .1){
                //Move towards target
                tempPosition = new Vector2(transform.position.x,targetY);
                transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
                if(board.allDots[column,row] != this.gameObject){
                    board.allDots[column,row] = this.gameObject;
                }
                findMatches.FindAllMatches();
            }else{
                //Directly set the position
                tempPosition = new Vector2(transform.position.x,targetY);
                transform.position = tempPosition;
            }
            if (board.currentState == GameState.move)
            {
                for(int i = 0; i < board.width; i++){
                    for(int j = 0; j < board.height; j++){
                        if(board.allDots[i,j] != gameObject && board.allDots[i,j].GetComponent<Dot>().column == column && board.allDots[i,j].GetComponent<Dot>().row == row){
                            Debug.LogWarning("Two dots merged, deleting one");
                            Destroy(gameObject);
                            board.StartFillBoardCoroutine();
                        }
                    }
                }
            }
        }
    }

    public IEnumerator CheckMoveCo(){
        yield return new WaitForSeconds(.5f);
        if(otherDot != null){
            if(!isMatched && !otherDot.GetComponent<Dot>().isMatched){
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                FindObjectOfType<AudioManager>().Play("Deny");
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            } else{
                moveCounter.SpendMove();
                board.DestroyMatches();
            }
            otherDot = null;
        } else{
            board.currentState = GameState.move;
        }
    }

    private void OnMouseDown(){
        if (ActivateSettings.gameRunning == true)
        { 
            if (board.currentState == GameState.move)
            {
                if (!board.tutorialEnabled || isTutorial)
                {
                    firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            
        }
    }

    private void OnMouseUp(){
        if (ActivateSettings.gameRunning == true)
        {
            if (board.currentState == GameState.move)
            {
                if (!board.tutorialEnabled || isTutorial)
                {
                    finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    CalculateAngle();
                }
            }
        }
    }

    void CalculateAngle(){
        if(Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
        }else{
            board.currentState = GameState.move;
        }
    }

    void MovePieces(){
        if(swipeAngle > -45 && swipeAngle <= 45 && column < board.width-1){
            //Right swipe
            otherDot = board.allDots[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        } else if(swipeAngle > 45 && swipeAngle <= 135 && row < board.height-1){
            //Up swipe
            otherDot = board.allDots[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        } else if((swipeAngle > 135 || swipeAngle <= -135) && column > 0){
            //Left swipe
            otherDot = board.allDots[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        } else if(swipeAngle < -45 && swipeAngle >= -135 && row > 0){
            //Down swipe
            otherDot = board.allDots[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    /*void FindMatches(){
        if(column > 0 && column < board.width - 1){
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if(leftDot1 != null && rightDot1 != null)
            {
                if(leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag){
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if(row > 0 && row < board.height - 1){
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            if(upDot1 != null && downDot1 != null){
                if(upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag){
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }*/
}
