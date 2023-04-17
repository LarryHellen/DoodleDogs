using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class ContinousNSS : MonoBehaviour
{
    [Header("Manual Variables")]


    public float spawnDelay;
    public float timeToOnBeatLocation;
    public float noteCooldown;
    public GameObject tapNotePrefab;

    public Dictionary<int, float> columnCooldowns = new Dictionary<int, float>();


    public Canvas canvas;
    public GameObject onBeatTestingLine;



    public float screenHeightPercentForNoteToLandOnBeat;

    [Header("BeatCheck Variables")]

    float bigSound = 0f;
    //float bigSoundBefore = 0f;

    public float necessarySoundLoudness;

    public float[] sounds = new float[128];

    [Space(25)]

    [Header("Automatic Variables")]

    public int playerCombo = 0;
    public int columns;
    public float screenWidth;
    public float screenHeight;

    private AudioSource[] audioArray;


    void Awake()
    {
        audioArray = GetComponents<AudioSource>();

        //Set column number according to the number of audio sources
        columns = audioArray.Length;

        print(columns);


        for (int i = 0; i < columns; i++)
        {
            columnCooldowns.Add(i, noteCooldown);
        }


        //Set ScreenWidth and ScreenHeight to the World Space width and height
        RectTransform canvasRt = canvas.GetComponent<RectTransform>();
        screenWidth = canvasRt.sizeDelta.x;
        screenHeight = canvasRt.sizeDelta.y;
    }


    void Start()
    {
        RectTransform lrt = onBeatTestingLine.GetComponent<RectTransform>();
        lrt.anchoredPosition = new Vector2(lrt.localPosition.x, (screenHeightPercentForNoteToLandOnBeat * screenHeight) - screenHeight / 2);

        StartCoroutine(CountdownCooldowns());
    }


    void Update()
    {
        ContinousNoteSpawning();
    }


    void ContinousNoteSpawning()
    {
        for (int column = 0; column < columns; column++)
        {
            if (BeatCheck(audioArray[column]) && columnCooldowns[column] == 0)
            {
                print("Passed Beat Check");
                columnCooldowns[column] = noteCooldown;
                //StartCoroutine(SpawnTapNote(column));
            }
        }
    }


    IEnumerator CountdownCooldowns()
    {
        yield return null;


        while (true)
        {
            for (int i = 0; i < columns; i++)
            {
                columnCooldowns[i] -= Time.deltaTime;

                if (columnCooldowns[i] < 0)
                {
                    columnCooldowns[i] = 0;
                }

            }
        }

    }


    IEnumerator SpawnTapNote(int column)
    {
        print("Trying to spawn a note");

        yield return new WaitForSeconds(spawnDelay);


        GameObject tapNote = Instantiate(tapNotePrefab, new Vector2(0f, 100f), Quaternion.identity, canvas.transform);
        BasicNoteObject bNO = tapNote.GetComponent<BasicNoteObject>();
        bNO.Start();
        Vector2 noteSize = bNO.SetAndReturnSize();



        print("NOTE SPAWN STUFF");

        float xPositionForNoteSpawn = screenWidth / 2;

        float yPositionForNoteSpawn = screenHeight;


        print("noteSizeX : " + noteSize.x);
        print("noteSizeY : " + noteSize.y);

        print(xPositionForNoteSpawn + " - " + yPositionForNoteSpawn);

        print("----------");



        RectTransform tNrt = tapNote.GetComponent<RectTransform>();
        tNrt.anchoredPosition = new Vector2(xPositionForNoteSpawn, yPositionForNoteSpawn);
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