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
    public Canvas canvas;
    public GameObject onBeatTestingLine;
    public GameObject tapNotePrefab;
    public GameObject holdNotePrefab;
    

    public float screenHeightPercentForNoteToLandOnBeat;

    [Header("BeatCheck Variables")]

    float bigSound = 0f;
    //float bigSoundBefore = 0f;

    public float necessarySoundLoudness;

    public float[] sounds = new float[128];

    [Space(25)]

    [Header("Automatic Variables")]

    public int currentInterval = 0;
    public int playerCombo = 0;
    public int columns;
    public float screenWidth;
    public float screenHeight;


    private List<List<bool>> notePattern = new List<List<bool>>();
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
        RectTransform canvasRt = canvas.GetComponent<RectTransform>();
        screenWidth = canvasRt.sizeDelta.x;
        screenHeight = canvasRt.sizeDelta.y;
    }


    void Start()
    {
        RectTransform lrt = onBeatTestingLine.GetComponent<RectTransform>();
        lrt.localPosition = new Vector3(lrt.localPosition.x, (screenHeightPercentForNoteToLandOnBeat * screenHeight) - screenHeight/2, lrt.localPosition.z);
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

        NoteSpawningAndSetting();
    }


    void NoteSpawningAndSetting()
    {
        if (intervalLength < timeElapsedSinceLastInterval)
        {
            timeElapsedSinceLastInterval = 0;


            notePattern.Add(BeatCheckList()); //Note Setting


            //notePattern.Add(new List<bool>() { true, true, true, true }); // Overriding note setting for testing purposes


            if (currentInterval >= maxHoldNoteLength) //If enough wait-time has passed for hold notes to be generated
            {
                print("Notes can currently spawn");

                NoteSpawning();
            }


            currentInterval++;
        }

        timeElapsedSinceLastInterval += Time.deltaTime;
    }


    void NoteSpawning()
    {
        for (int i = 0; i < columns; i++)
        {
            if (notePattern[currentInterval - maxHoldNoteLength][i])
            {
                int repeatedTrues = 1;

                for (int j = 1; j < maxHoldNoteLength; j++)
                {
                    if (notePattern[currentInterval - maxHoldNoteLength + j][i])
                    {
                        repeatedTrues++;
                    }
                    else
                    {
                        break;
                    }
                }

                NoteDeterminingLogic(repeatedTrues, i);
            }
        }
    }


    void NoteDeterminingLogic(int numOfTrues, int noteColumn)
    {
        if (numOfTrues > 1) //Spawn Hold Note
        {
            GameObject holdNote = Instantiate(holdNotePrefab, new Vector2(0f, 100f), Quaternion.identity, canvas.transform); //SET PROPER SPAWNING POSITION BASED ON COLUMN
            HoldNoteObject hNO = holdNote.GetComponent<HoldNoteObject>();
            hNO.Start();
            RectTransform hNrt = holdNote.GetComponent<RectTransform>();
            hNO.holdNoteLength = numOfTrues;
            hNO.SetLengthBasedAttributes();

            float xPositionForNoteSpawn = ((hNrt.sizeDelta.x * noteColumn + hNrt.sizeDelta.x/2) + ((screenWidth-columns*hNrt.sizeDelta.x)/(columns-1)) * noteColumn) - screenWidth/2;
            //THIS OFFSET DOES NOT WORK, FIND THE PROPER EQUATION

            float yPositionForNoteSpawn = (screenHeight/2 + hNrt.sizeDelta.y);

            hNrt.localPosition = new Vector3(xPositionForNoteSpawn, yPositionForNoteSpawn);
        }
        else //Spawn Tap Note
        {
            GameObject tapNote = Instantiate(tapNotePrefab, new Vector2(0f, 100f), Quaternion.identity, canvas.transform);
            RectTransform tNrt = tapNote.GetComponent<RectTransform>();

            float xPositionForNoteSpawn = ((tNrt.sizeDelta.x * noteColumn + tNrt.sizeDelta.x / 2) + ((screenWidth - columns * tNrt.sizeDelta.x) / (columns - 1)) * noteColumn) - screenWidth / 2;
            //THIS OFFSET DOES NOT WORK, FIND THE PROPER EQUATION

            float yPositionForNoteSpawn = (screenHeight/2 + tNrt.sizeDelta.y);

            tNrt.localPosition = new Vector3(xPositionForNoteSpawn, yPositionForNoteSpawn);
        }
    }


    List<bool> BeatCheckList()
    {
        List<bool> boolList = new List<bool>();

        for (int i = 0; i < columns; i++)
        {
            bool isThereCurrentlySound = BeatCheck(audioArray[i]);

            boolList.Add(isThereCurrentlySound);
        }

        return boolList;
    }


    bool BeatCheck(AudioSource sound)
    {
        sound.GetSpectrumData(sounds, 0, FFTWindow.Rectangular); //CHANGE NOTE CHECKING TO BE A LIST OF AVERAGES "NOTE POSSIBILTY" AT DIFF INTERVALS SO THAT CHECKS BEFORE AND AFTER NOTES STILL WORK


        //bigSoundBefore = bigSound;
        bigSound = sounds[0] * 100;

        print(bigSound);

        if (bigSound > necessarySoundLoudness) //&& bigSoundBefore <= necessarySoundLoudness)
        {
            print("Beat Detected");
            return true;
        }
        else
        {
            return false;
        }
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