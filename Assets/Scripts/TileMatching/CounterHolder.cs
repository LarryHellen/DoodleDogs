using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHolder : MonoBehaviour
{
    public GameObject counterPrefab;
    public GameObject[] counterArray;
    public float yDistance;
    public float xDistance;
    //private Vector3 tempPos;
    private Board board;
    public int goals;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        SetUp();
    }

    public void Reset(){
        foreach(Transform child in transform){
            child.GetComponent<MatchCounter>().Reset();
        }
        SetUp();
    }

    public void SetUp(){
        /*tempPos = gameObject.transform.position;
        counterArray[0] = addCounter(tempPos,goals,"Bread Dot",false);
        counterArray[1] = addCounter(tempPos,goals,"Cheese Dot",true);
        counterArray[2] = addCounter(tempPos,goals,"Butter Dot",false);
        counterArray[3] = addCounter(tempPos,goals,"Ham Dot",false);*/
    }

    public void Update()
    {
        if (metAllGoals())
        {
            board.winGame();
        }
    }

    /*GameObject addCounter(Vector3 pos, int num, string type,bool newRow){
        GameObject tempCounter = Instantiate(counterPrefab,pos,Quaternion.identity,gameObject.transform);
        tempCounter.GetComponent<MatchCounter>().Create(num,type);
        if (newRow)
        {
            tempPos.y += yDistance;
            tempPos.x -= xDistance;
        }
        else
        {
            tempPos.x += xDistance;
        }
        return tempCounter;
    }*/

    public void addMatch(string type, int numberToAdd){
        foreach(GameObject counter in counterArray){
            MatchCounter mc = counter.GetComponent<MatchCounter>();
            if(mc.matchType == type){
                mc.IncreaseMatches(numberToAdd);
            }
        }
    }

    public bool metAllGoals(){
        bool met = true;
        foreach(GameObject counter in counterArray){
            MatchCounter mc = counter.GetComponent<MatchCounter>();
            if(!mc.full){
                met = false;
            }
        }
        return met;
    }

    public List<string> EmptyCounters(){
        List<string> emptyCounters = new List<string>();

        foreach(GameObject counter in counterArray){
            if(counter.GetComponent<MatchCounter>().GetMatches() == 0){
                emptyCounters.Add(counter.tag);
            }
        }

        return emptyCounters;
    }
    
}
