using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class NoteSpawningSystem : MonoBehaviour
{
    [Header("Manual Variables")]

    public bool holdNotes;
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
 
        audioArray = GetComponents<AudioSource>();

        //Set column number according to the number of audio sources
        columns = audioArray.Length;

        print(columns);

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
        if (holdNotes)
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
        else
        {
            for (int i = 0; i < columns; i++)
            {
                if (notePattern[currentInterval - maxHoldNoteLength][i])
                {
                    NoteDeterminingLogic(1, i);
                }
            }
        }
    }
    

    void NoteDeterminingLogic(int numOfTrues, int noteColumn)
    {
        if (numOfTrues > 1) //Spawn Hold Note
        {
            GameObject holdNote = Instantiate(holdNotePrefab, new Vector2(0f, 100f), Quaternion.identity, canvas.transform);
            HoldNoteObject hNO = holdNote.GetComponent<HoldNoteObject>();
            hNO.Start();
            hNO.holdNoteLength = numOfTrues;
            hNO.SetLengthBasedAttributes();


            float xPositionForNoteSpawn = 1;

            float yPositionForNoteSpawn = 1;

            RectTransform hNrt = holdNote.GetComponent<RectTransform>();
            //hNrt.anchoredPosition = new Vector2(xPositionForNoteSpawn, yPositionForNoteSpawn);
        }
        else //Spawn Tap Note
        {
            GameObject tapNote = Instantiate(tapNotePrefab, new Vector2(0f, 100f), Quaternion.identity, canvas.transform);
            BasicNoteObject bNO = tapNote.GetComponent<BasicNoteObject>();
            bNO.Start();
            Vector2 noteSize = bNO.SetAndReturnSize();
            


            print("NOTE SPAWN STUFF");

            float xPositionForNoteSpawn = screenWidth/2;

            float yPositionForNoteSpawn = screenHeight;


            print("noteSizeX : " + noteSize.x);
            print("noteSizeY : " + noteSize.y);

            print(xPositionForNoteSpawn + " - " + yPositionForNoteSpawn);

            print("----------");



            RectTransform tNrt = tapNote.GetComponent<RectTransform>();
            tNrt.anchoredPosition = new Vector2(xPositionForNoteSpawn, yPositionForNoteSpawn);
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


    bool BeatCheck(AudioSource sound) //Gonna need to become a coroutine at somepoint
    {
        sound.GetSpectrumData(sounds, 0, FFTWindow.Rectangular); //CHANGE NOTE CHECKING TO BE A LIST OF AVERAGES "NOTE POSSIBILTY" AT DIFF INTERVALS SO THAT CHECKS BEFORE AND AFTER NOTES STILL WORK


        //bigSoundBefore = bigSound;
        bigSound = sounds[0] * 100;

        //print(bigSound);

        if (bigSound > necessarySoundLoudness) //&& bigSoundBefore <= necessarySoundLoudness)
        {
            //print("Beat Detected");
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