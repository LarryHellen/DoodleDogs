using UnityEngine;
using System.Collections;

public class HoldNoteObject : MonoBehaviour
{
    //Init HoldNoteLengthSize
    //Init HoldNoteLengthConstant (Should be a percent of screen height)
    //Init HoldNoteBaseTime (1 Interval, Get from main spanwer script)
    //Init HoldNoteTimeConstant
    //Init HoldNoteHoldTime


    void Start()
    {
        //When note spawn, scale to the correct size (HoldNoteLengthSize*(HoldNoteLengthConstant*ScreenHeight))
        //Set HoldNoteHoldTime to the proper number of seconds [HoldNoteHoldTime = (HoldNoteLengthSize*HoldNoteTimeConstant)]
    }
}
