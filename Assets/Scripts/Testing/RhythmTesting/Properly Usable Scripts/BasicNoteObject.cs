﻿using UnityEngine;
using System.Collections;

public class BasicNoteObject : MonoBehaviour
{
    //Get ScreenWidth ("Screen.width")
    //Get ScreenHeight
    //Init Columns (Get from main spanwer script)
    //Init NotePercentLengthOfScreen (Get from main spanwer script)
    //Init NoteWidth
    //Init NoteHeight
    //Init IntervalLength (Get from main spanwer script)
    //Init SpaceBetweenNotes (Get from main spanwer script)
    //Init PublicDeltaTime (Get from main spanwer script)
    //Init TimeElapsedSinceLastInterval
    //Init ScreenHeightPercentForNoteHide (Get from main spanwer script)
    //Init ScreenHeightPercentForNoteToLandOnBeat (Get from main spanwer script)
    //Init NoteSpeedInIntervals (Get from main spanwer script)

    //Init TimeBeenTapped


    // OPACITY //
    //Init ScreenHeightPercentForNoteShow (Get from main spanwer script)
    //Init InitialOpacity
    //Init EndOpacity
    //Init SpriteRenderer





    void Start()
    {
        //Set all variables for proper positioning and size (change sizze only if HoldNoteObject script not attached)
    }


    void Update()
    {
        //NOTE - MOVEMENT


        //USE PHONE PHOTO TO MAKE THIS SYSTEM


        //END OF "NOTE - MOVEMENT"



        //NOTE - WINNING AND LOSING


        //if HoldNoteObject script is attached and object being clicked, increase a TimeBeenTapped variable by deltaTime
        //if TimeBeenTapped is greater than this objects HoldTime, [Destroy this object and give the player score] <- Seperate function?

        //if HoldNoteObject script is attached and object not being clicked
        //Set TimeBeenTapped to 0


        //else if (HoldNoteObject script not attached and object being clicked), [Destroy this object and give the player score] <- Seperate function?


        //if object has passed ScreenHeightForNoteHide, [Destroy this object and set the player's score to 0]


        //END OF "NOTE - WINNING AND LOSING"
    }
}


//THINGS I NEED TO FIGURE OUT
// - HOLD NOTE SPAWNING SYSTEM (OLD METHOD? FIND A NEW METHOD?)
// - BASIC NOTE AND HOLD NOTE TAPPING SYSTEMS
// - NOTE MOVEMENT SO THAT NOTES LAND ON A CERTAIN SCREEN HEIGHT PERCENTAGE ON THE BEAT
// - NOTE PATTERN CREATOR + DISPLAYER
// - NEW HOLD AND BASIC NOTE PREFABS
// - DELAY THE START OF AUDIO BY (MaxHoldNoteLength + TimeToReachOnBeatPosition) number of Intervals