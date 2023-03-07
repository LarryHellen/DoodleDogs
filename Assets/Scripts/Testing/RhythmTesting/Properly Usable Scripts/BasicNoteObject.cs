using UnityEngine;
using System.Collections;

public class BasicNoteObject : MonoBehaviour
{
    //Init ScreenWidth ("Screen.width") (Get from main spanwer script)
    //Init ScreenHeight (Get from main spanwer script)


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
    //Init TimeToOnBeatLocation (Get from main spanwer script)
    //Init DistanceToMoveIn1Interval
    //Init TimeBeenTapped


    // OPACITY //
    //Init ScreenHeightPercentForNoteShow (Get from main spanwer script)
    //Init InitialOpacity
    //Init EndOpacity
    //Init SpriteRenderer


    void Start()
    {
        //Set all variables for proper positioning and size (change sizze only if HoldNoteObject script not attached)


        //NOTE - MOVEMENT


        //DistanceToMoveIn1Interval = (ScreenHeight - ScreenHeight*ScreenHeightPercentForNoteToLandOnBeat)/TimeToOnBeatLocation


        //END OF "NOTE - MOVEMENT"
    }


    void Update()
    {
        //NOTE - MOVEMENT


        //SETUP LERP VARIABLES

        //LERP TO DistanceToMoveIn1Interval from a starting position over 1 Interval of time

        //if (lerp complete)
            //RESET LERP VARIABLES


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
// y- HOLD NOTE SPAWNING SYSTEM (OLD METHOD? FIND A NEW METHOD?)
// y- BASIC NOTE AND HOLD NOTE TAPPING SYSTEMS
// y- NOTE MOVEMENT SO THAT NOTES LAND ON A CERTAIN SCREEN HEIGHT PERCENTAGE ON THE BEAT
// y- NOTE PATTERN CREATOR + DISPLAYER
// y- NEW HOLD AND BASIC NOTE PREFABS
// check logic- DELAY THE START OF AUDIO BY (MaxHoldNoteLength + TimeToOnBeatLocation) number of Intervals (it works)