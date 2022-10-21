using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHolder : MonoBehaviour
{
    public GameObject counterPrefab;
    public GameObject[] counterArray;
    public float yDistance;
    public float xDistance;
    private Vector3 tempPos;
    private Board board;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        SetUp();
    }

    public void Reset(){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        SetUp();
    }

    public void SetUp(){
        tempPos = gameObject.transform.position;
        counterArray[0] = addCounter(tempPos,10,"Bread Dot");
        counterArray[1] = addCounter(tempPos,10,"Butter Dot");
        counterArray[2] = addCounter(tempPos,10,"Cheese Dot");
        counterArray[3] = addCounter(tempPos,10,"Ham Dot");
    }

    GameObject addCounter(Vector3 pos, int num, string type){
        GameObject tempCounter = Instantiate(counterPrefab,pos,Quaternion.identity,gameObject.transform);
        tempCounter.GetComponent<MatchCounter>().Create(num,type);
        tempPos.y += yDistance;
        tempPos.x += xDistance;
        return tempCounter;
    }

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
    
}
