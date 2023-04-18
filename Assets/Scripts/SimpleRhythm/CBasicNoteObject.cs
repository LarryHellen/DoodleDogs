using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CBasicNoteObject : MonoBehaviour
{
    [Header("Manual Variables")]
    
    public float noteHeight;
    private ContinousNoteSpawning nSS;
    private RectTransform rt;


    public void Start()
    {
        rt = GetComponent<RectTransform>();

        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();

        SetAndReturnSize();


        StartCoroutine(ContinousMovement());
    }


    void Update()
    {
        NoteDestruction();
    }


    public Vector2 SetAndReturnSize()
    {
        var sizeDelta = rt.sizeDelta;

        sizeDelta.x = (nSS.screenWidth - (nSS.spaceBetweenNotes * (nSS.columns - 1))) / nSS.columns;

        sizeDelta.y = nSS.screenHeight * noteHeight;
        
        rt.sizeDelta = sizeDelta;

        return rt.sizeDelta;
    }


    IEnumerator ContinousMovement()
    {
        float distanceToMove = rt.anchoredPosition.y - ((nSS.onBeatHeight * nSS.screenHeight) - (nSS.screenHeight / 2)) - (nSS.screenHeight * noteHeight / 2);

        while (true)
        {
            float startPos = rt.anchoredPosition.y;
            //print("StartPos: " + startPos);

            float endPos = rt.anchoredPosition.y - distanceToMove;
            //print("EndPos: " + endPos);

            float timeElapsed = 0;
            float lerpValue = 0;

            while (timeElapsed < nSS.timeToOnBeatHeight)
            {
                lerpValue = Mathf.Lerp(startPos, endPos, timeElapsed / nSS.timeToOnBeatHeight);
                timeElapsed += Time.deltaTime;

                //print(timeElapsed);

                
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, lerpValue);
                
                yield return null; //VERY IMPORTANT, CAUSES THE COROUTINE TO WAIT UNTIL THE NEXT FRAME
            }

            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, endPos);
        }
    }


    void NoteDestruction()
    {
        if (rt.anchoredPosition.y <= -nSS.screenHeight / 2 - (nSS.screenHeight * noteHeight / 2))
        {
            Destroy(gameObject);
            nSS.OnLose();
        }
    }


    public void OnPointerDownOnNote()
    {
        //print("Pointer Down On Note");
        Destroy(gameObject);
    }


    public void OnPointerUpOnNote()
    {
        //print("Pointer Up On Note");
    }

}