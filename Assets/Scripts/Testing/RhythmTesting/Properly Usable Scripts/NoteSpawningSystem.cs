using UnityEngine;
using System.Collections;

public class NoteSpawningSystem : MonoBehaviour
{
    //Init NotePattern (list of lists of booleans)
    //Init AudioList (list of AudioSources)


    //Init Columns(Get from main spanwer script)
    //Init NotePercentLengthOfScreen (Get from main spanwer script)
    //Init IntervalLength (Get from main spanwer script)
    //Init SpaceBetweenNotes (Get from main spanwer script)
    //Init PublicDeltaTime (Get from main spanwer script)
    //Init TimeElapsedSinceLastInterval
    //Init ScreenHeightPercentForNoteHide (Get from main spanwer script)
    //Init ScreenHeightPercentForNoteToLandOnBeat (Get from main spanwer script)
    //Init ScreenHeightPercentForNoteShow (Get from main spanwer script)
    //Init MaxHoldNoteLength
    //Init NoteSpeedInIntervals (Get from main spanwer script)




    void Start()
    {
        //Get all audio sources attached to the object that this script is attached to and put them in a list
        //Set column number according to the number of audio sources
    }

    
    void Update()
    {
        //Every interval, check if a beat is occuring on every audio (with a seperate function) and if one is, have that section of the list return True
        //Every interval, check if True is present at the position interval-MaxHoldNoteLength and, if so, step through MaxHoldNoteLength Intervals in the NotePattern breaking if one is not True and return the number of Trues present
        //Every interval, if the number of Trues present is greater than 1, spawn a hold note with the proper length else spawn a normal note
    }
}
