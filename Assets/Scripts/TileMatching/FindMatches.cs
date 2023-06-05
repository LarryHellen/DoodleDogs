using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{

    public Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("TileMatchingMusicBackground");
        //board = FindObjectOfType<Board>();
    }

    public void FindAllMatches(){
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo(){
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++){
            for(int j = 0; j < board.height; j++){
                GameObject currentDot = board.allDots[i,j];
                if(currentDot != null){
                    if(i > 0 && i < board.width - 1){
                        GameObject leftDot = board.allDots[i-1,j];
                        GameObject rightDot = board.allDots[i+1,j];
                        if(leftDot != null && rightDot != null){
                            if(leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag){
                                if(board.tutorialEnabled == true){
                                    board.SetMatched();
                                }
                                if(!currentMatches.Contains(leftDot)){
                                    matchDot(leftDot);
                                }
                                if(!currentMatches.Contains(rightDot)){
                                    matchDot(rightDot);
                                }
                                if(!currentMatches.Contains(currentDot)){
                                    matchDot(currentDot);
                                }
                                leftDot.GetComponent<Dot>().isMatched = true;
                                rightDot.GetComponent<Dot>().isMatched = true;
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                    if(j > 0 && j < board.height - 1){
                        GameObject downDot = board.allDots[i,j-1];
                        GameObject upDot = board.allDots[i,j+1];
                        if(downDot != null && upDot != null){
                            if(downDot.tag == currentDot.tag && upDot.tag == currentDot.tag){
                                if(board.tutorialEnabled == true){
                                    board.SetMatched();
                                }
                                if(!currentMatches.Contains(downDot)){
                                    matchDot(downDot);
                                }
                                if(!currentMatches.Contains(upDot)){
                                    matchDot(upDot);
                                }
                                if(!currentMatches.Contains(currentDot)){
                                    matchDot(currentDot);
                                }
                                downDot.GetComponent<Dot>().isMatched = true;
                                upDot.GetComponent<Dot>().isMatched = true;
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }    
    }

    private void matchDot(GameObject selectedDot){
        currentMatches.Add(selectedDot);
        // if(board.currentState == GameState.wait){
        //     board.counterHolder.addMatch(selectedDot.tag,1);
        // }
    }

}
