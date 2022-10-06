using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHolder : MonoBehaviour
{
    public GameObject counterPrefab;
    public GameObject[] counterArray;
    public float distanceBetweenCounters;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        tempPos = gameObject.transform.position;

        counterArray[0] = addCounter(tempPos,10,"Yellow Dot");
        counterArray[1] = addCounter(tempPos,10,"Teal Dot");
        counterArray[2] = addCounter(tempPos,10,"Red Dot");
        counterArray[3] = addCounter(tempPos,10,"Purple Dot");
        
    }

    GameObject addCounter(Vector3 pos, int num, string type){
        GameObject tempCounter = Instantiate(counterPrefab,pos,Quaternion.identity,gameObject.transform);
        tempCounter.GetComponent<MatchCounter>().Create(num,type);
        tempPos.y -= distanceBetweenCounters;
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
    
}
