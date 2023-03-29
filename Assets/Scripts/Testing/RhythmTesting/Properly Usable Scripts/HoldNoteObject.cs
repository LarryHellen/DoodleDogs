using UnityEngine;
using System.Collections;

public class HoldNoteObject : MonoBehaviour
{
    [Header("Manual Variables")]

    public int holdNoteLength;
    public float holdNoteLengthConstant;
    public float holdNoteTimeConstant;

    [Space(25)]

    [Header("Automatic Variables")]

    public float timeBeenTapped = 0;
    public float holdNoteHoldTime;
    private RectTransform rt;

    private NoteSpawningSystem nSS;


    void Start()
    {
        //Getting the NoteSpawningSystem Script for its variables
        nSS = GameObject.Find("NoteSpawnManager").GetComponent<NoteSpawningSystem>();

        //When note spawn, scale to the correct size (HoldNoteLengthSize*(HoldNoteLengthConstant*ScreenHeight))
        rt = GetComponent<RectTransform>();
    }


    public void SetLengthBasedAbilities()
    {
        var sizeDelta = rt.sizeDelta;
        sizeDelta.y = holdNoteLength * holdNoteLengthConstant * nSS.screenHeight;
        rt.sizeDelta = sizeDelta;

        //Set HoldNoteHoldTime to the proper number of seconds [HoldNoteHoldTime = (HoldNoteLengthSize*HoldNoteTimeConstant*IntervalLength)]
        holdNoteHoldTime = holdNoteLength * nSS.intervalLength * holdNoteTimeConstant;
    }
}
