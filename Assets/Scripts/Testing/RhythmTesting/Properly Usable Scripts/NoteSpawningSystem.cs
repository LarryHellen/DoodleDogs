using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class NoteSpawningSystem : MonoBehaviour
{
    [Header("Manual Variables")]

    public float intervalLength;
    public float horizontalSpaceBetweenNotes;
    private float timeElapsedSinceLastInterval;
    public int maxHoldNoteLength;
    public float timeToOnBeatLocation;
    public float holdNoteHoldTimeTolerance;
    public GameObject OnBeatTestingLine;
    public GameObject BasicNotePrefab;
    public GameObject HoldNotePrefab;

    public float screenHeightPercentForNoteToLandOnBeat;

    [Space(25)]

    [Header("Automatic Variables")]

    public int playerCombo = 0;
    public int columns;
    public float screenWidth;
    public float screenHeight;


    private List<List<bool>> notePattern;
    private AudioSource[] audioArray;

    private BasicNoteObject basicNoteObject;


    void Awake()
    {
        //TouchSimulation.Enable(); //For testing touch input without exporting to a phone

        //Get all audio sources attached to the object that this script is attached to and put them in a list
        audioArray = GetComponents<AudioSource>();

        //Set column number according to the number of audio sources
        columns = audioArray.Length;

        //Set ScreenWidth and ScreenHeight to the World Space width and height
        GameObject canvas = GameObject.Find("GameField");
        RectTransform canvasRt = canvas.GetComponent<RectTransform>();
        screenWidth = canvasRt.sizeDelta.x;
        screenHeight = canvasRt.sizeDelta.y;
    }

    void Start()
    {
        RectTransform lrt = OnBeatTestingLine.GetComponent<RectTransform>();
        lrt.localPosition = new Vector3(lrt.localPosition.x, (screenHeightPercentForNoteToLandOnBeat * screenHeight) - screenHeight/2, lrt.localPosition.y);
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


        //****Get minimum viable product with just normal note spawning
    }
}
/*
    float bigSound = 0f;
    float bigSoundBefore = 0f;

    public float necessarySoundLoudness;

    public float[] sounds = new float[128];

    void Start()
    {
        
    }

    
    void Update()
    {
        sound.GetSpectrumData(sounds, 0, FFTWindow.Rectangular);

        bigSoundBefore = bigSound;
        bigSound = sounds[0] * 100;

        if (bigSound > necessarySoundLoudness && bigSoundBefore <= necessarySoundLoudness)
        {
            print("Beat Detected");
        }
    }
*/