using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHolder : MonoBehaviour
{
    public GameObject counterPrefab;
    private Board board;
    public GameObject[] counterArray;
    public float distanceBetweenCounters;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        tempPos = gameObject.transform.position;

        addCounter(tempPos,2,"bread");
        addCounter(tempPos,1,"butter");
        addCounter(tempPos,3,"ham");
        addCounter(tempPos,3,"cheese");
        
    }

    GameObject addCounter(Vector3 pos, int num, string type){
        GameObject tempCounter = Instantiate(counterPrefab,pos,Quaternion.identity,gameObject.transform);
        tempCounter.GetComponent<MatchCounter>().Create(num,type);
        tempPos.y -= distanceBetweenCounters;
        return tempCounter;
    }
    
}
