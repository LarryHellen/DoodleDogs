using UnityEngine;
using System.Collections;

public class HoldNoteObject : MonoBehaviour
{
    //Init holdNoteLength
    //Init HoldNoteLengthConstant (Should be a percent of screen height)
    //Init HoldNoteBaseTime (1 Interval, Get from main spanwer script)
    //Init HoldNoteTimeConstant (Usually will be 1, just in case you want to slow down or speed up hold times)
    //Init HoldNoteHoldTime
    //Init screenHeight; (Get from main spanwer script)


    public int holdNoteLength;
    public float holdNoteLengthConstant;
    public float holdNoteBaseTime;
    public float holdNoteTimeConstant;
    private float holdNoteHoldTime;
    private RectTransform rt;

    private NoteSpawningSystem noteSpawningSystem;


    void Start()
    {
        //Getting the NoteSpawningSystem Script for its variables
        noteSpawningSystem = GameObject.Find("NoteSpawnManager").GetComponent<NoteSpawningSystem>();

        //When note spawn, scale to the correct size (HoldNoteLengthSize*(HoldNoteLengthConstant*ScreenHeight))
        rt = GetComponent<RectTransform>();
        var scale = rt.localScale;
        scale.y = (float) holdNoteLength * holdNoteLengthConstant * noteSpawningSystem.screenHeight;
        rt.localScale = scale;

        //Set HoldNoteHoldTime to the proper number of seconds [HoldNoteHoldTime = (HoldNoteLengthSize*HoldNoteTimeConstant*IntervalLength)]
        holdNoteHoldTime = holdNoteLength * noteSpawningSystem.intervalLength * holdNoteTimeConstant;
    }
}
