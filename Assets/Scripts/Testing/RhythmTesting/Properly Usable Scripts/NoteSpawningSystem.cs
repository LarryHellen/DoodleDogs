using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class NoteSpawningSystem : MonoBehaviour
{
    //Init ScreenWidth ("Screen.width") (Get from main spanwer script)
    //Init ScreenHeight (Get from main spanwer script)


    //Init NotePattern (list of lists of booleans)
    //Init AudioArray (Array of AudioSources)


    //Init BasicNotePrefab
    //Init HoldNotePrefab


    //Init Columns (Get from legnth of Audio Array)
    //Init IntervalLength (Get from main spanwer script)
    //Init HorizontalSpaceBetweenNotes (Get from main spanwer script)
    //Init TimeElapsedSinceLastInterval
    //Init ScreenHeightPercentForNoteHide (Get from main spanwer script)
    //Init ScreenHeightPercentForNoteToLandOnBeat (Get from main spanwer script)
    //Init ScreenHeightPercentForNoteShow (Get from main spanwer script)
    //Init MaxHoldNoteLength
    //Init TimeToOnBeatLocation (Get from main spanwer script, in intervals)
    

    public float screenWidth;
    public float screenHeight;

    private List<List<bool>> notePattern;
    private AudioSource[] audioArray;

    public GameObject BasicNotePrefab;
    public GameObject HoldNotePrefab;

    public int columns;
    public float notePercentLengthOfScreen;
    public float intervalLength;
    public float horizontalSpaceBetweenNotes;
    private float timeElapsedSinceLastInterval;
    public float ScreenHeightPercentForNoteHide;
    public float ScreenHeightPercentForNoteToLandOnBeat;
    public float ScreenHeightPercentForNoteShow;
    public int maxHoldNoteLength;
    public float timeToOnBeatLocation;
    public AudioMixer silencer;


    void Start()
    {
        //Get all audio sources attached to the object that this script is attached to and put them in a list
        audioArray = GetComponents<AudioSource>();

        //Set column number according to the number of audio sources
        columns = audioArray.Length;

        //Set ScreenWidth and ScreenHeight to the World Space width and height
        Vector3 tempScreenWidthAndHeightCoords;
        Vector3 screenWidthAndHeightCoords = new Vector3(Screen.width, Screen.height, 0f);
        tempScreenWidthAndHeightCoords = Camera.main.ScreenToWorldPoint(screenWidthAndHeightCoords) * 2; //"*2" so that it is the full screen height and width as opposed to half of it

        screenWidth = tempScreenWidthAndHeightCoords.x;
        screenHeight = tempScreenWidthAndHeightCoords.y;

        //Silence all audios
        silencer.SetFloat("silencePlease", -80f);
    }


    void Update()
    {
        //Every interval, check if a beat is occuring on every audio (with a seperate function) and if one is, have that section of the list return True
        //Every interval, check if True is present at the position interval-MaxHoldNoteLength and, if so, step through MaxHoldNoteLength Intervals in the NotePattern breaking if one is not True and return the number of Trues present
        //Every interval,
            //if the number of Trues present is greater than 1, 
                //spawn a hold note with the proper length
                //remember to increase the spawn height of a hold note by: (HoldNoteLength * (HoldNoteLengthConstant * ScreenHeight))/2

            //else spawn a normal note
    }
}
