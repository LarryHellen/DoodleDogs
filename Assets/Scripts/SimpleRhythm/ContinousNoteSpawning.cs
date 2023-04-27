using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinousNoteSpawning : MonoBehaviour
{
    [Header("Manual Variables")]
    public bool testing;
    public float songLength;
    public float progressBarLength;
    public float onBeatHeight;
    public float timeToOnBeatHeight;
    public float noteCooldown;
    public float missesAvaible;
    public bool invicibility = false;
    public int audioChoice;
    public GameObject[] audioObjects;
    public Dictionary<int, float> columnCooldowns = new Dictionary<int, float>();
    public Canvas canvas;
    public Canvas backgroundCanvas;
    public GameObject onBeatTestingLine;
    public TextMeshProUGUI elapsedTimeText;
    public GameObject notePrefab;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject bigRedX;
    public Slider progressBar;

    [Space(25)]
    

    [Header("BeatCheck Variables")]

    private float bigSound = 0f;
    private float bigSoundBefore = 0f;
    public float necessarySoundLoudness;
    private float[] sounds = new float[128];

    [Space(25)]


    [Header("Automatic Variables")]
    public int columns;
    public float screenWidth;
    public float screenHeight;
    private float timeElapsed = 0;
    private CAudioDelay cAudioDelay;
    public AudioSource[] audioArray;
    

    void Awake()
    {
        audioArray = audioObjects[audioChoice].GetComponents<AudioSource>();

        columns = audioArray.Length;
        print(columns);


        for (int i = 0; i < columns; i++) {columnCooldowns.Add(i, 0);}


        RectTransform canvasRt = canvas.GetComponent<RectTransform>();
        screenWidth = canvasRt.sizeDelta.x;
        screenHeight = canvasRt.sizeDelta.y;


        cAudioDelay = GameObject.Find("AudioPlayer").GetComponent<CAudioDelay>();

        //SET THE PROPER SETTINGS FOR THE LEVEL RIGHT HERE
        if (!testing)
        {
            if (audioChoice == 0)
            {
                Audio1Settings();
            }
            else if (audioChoice == 1)
            {
                Audio2Settings();
            }
        }

    }


    void Start()
    {
        Time.timeScale = 1;

        RectTransform lrt = onBeatTestingLine.GetComponent<RectTransform>();
        lrt.anchoredPosition = new Vector2(lrt.anchoredPosition.x, (onBeatHeight * screenHeight) - (screenHeight / 2));

        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        StartCoroutine(ProgressBarSlider());
    }

    
    void Update()
    {
        ContinousSpawning();
        timeElapsed += Time.deltaTime;
        elapsedTimeText.SetText("Time Elapsed: " + timeElapsed);


        if (timeElapsed >= songLength) //Win Condition
        {
            OnWin();
        }
    }


    void ContinousSpawning()
    {
        for (int column = 0; column < columns; column++)
        {
            if (columnCooldowns[column] <= 0)
            {
                if (BeatCheck(audioArray[column]))
                {
                    GameObject newNote = Instantiate(notePrefab, new Vector3(0, screenHeight, 0), notePrefab.transform.rotation, canvas.transform);

                    CBasicNoteObject noteScript = newNote.GetComponent<CBasicNoteObject>();
                    noteScript.Start();
                    Vector2 noteSize = noteScript.SetAndReturnSize();

                    RectTransform rt = newNote.GetComponent<RectTransform>();

                    //SET X AND Y POSITION OF NOTE SPAWN
                    float xPos = ((screenWidth / columns) * column + noteSize.x/2) - screenWidth/2;
                    float yPos = (screenHeight + noteSize.y/2) - screenHeight/2;


                    
                    rt.anchoredPosition = new Vector2(xPos,yPos);

                    columnCooldowns[column] = noteCooldown;
                }
            }
            else
            {
                columnCooldowns[column] -= Time.deltaTime;
            }
        }
    }


    bool BeatCheck(AudioSource sound)
    {
        sound.GetSpectrumData(sounds, 0, FFTWindow.Rectangular);
        bigSoundBefore = bigSound;
        bigSound = sounds[0] * 100;

        if (bigSound > necessarySoundLoudness && bigSound >= bigSoundBefore)
        {
            //print("Beat Detected");
            return true;
        }
        else
        {
            return false;
        }
    }


    public void OnWin()
    {
        Time.timeScale = 0;
        cAudioDelay.PauseAudio();
        winScreen.SetActive(true);
        print("You win!");
    }


    public void OnLose()
    {
        if (!invicibility)
        {
            missesAvaible--;
            if (missesAvaible <= -1)
            {
                Time.timeScale = 0;
                cAudioDelay.PauseAudio();
                loseScreen.SetActive(true);
                print("You lose!");
            }
        }
    }


    IEnumerator ProgressBarSlider()
    {
        float elapsedTime = 0;
        float progress = 0;

        while (elapsedTime < progressBarLength)
        {
            elapsedTime += Time.deltaTime;
            progress = Mathf.Lerp(0, 1, elapsedTime / progressBarLength);
            progressBar.value = progress;
            yield return null;
        }
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene("SimpleRhythm");
    }


    public void Audio1Settings(){
        songLength = 50;
        progressBarLength = 43;
        onBeatHeight = 0.2f;
        timeToOnBeatHeight = 0.75f;
        noteCooldown = 0.5f;
        missesAvaible = 3;
    }


    public void Audio2Settings(){
        songLength = 42;
        progressBarLength = 40;
        onBeatHeight = 0.2f;
        timeToOnBeatHeight = 0.6f;
        noteCooldown = 0.4f;
        missesAvaible = 3;
    }


    public void OnPointerDownOnCanvas()
    {
        print("Pointer Down On Canvas");
        MissAnimation();
    }


    void MissAnimation()
    {
        //Input.mousePosition

        //Get mouse position

        /*
        Instantiate(bigRedX);
        missesAvaible--;
        */
        
        //Instanstiate a big red x game object at mouse position
        //Subtract 1 from total missesAvailable
    }
}