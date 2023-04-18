using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ContinousNoteSpawning : MonoBehaviour
{
    [Header("Manual Variables")]
    public float songLength;
    public float onBeatHeight;
    public float timeToOnBeatHeight;
    public float noteCooldown;
    public float spaceBetweenNotes; //YET TO BE IMPLEMENTED
    public bool invicibility = false;
    
    public Dictionary<int, float> columnCooldowns = new Dictionary<int, float>();
    public Canvas canvas;
    public Canvas backgroundCanvas;
    public GameObject onBeatTestingLine;
    public TextMeshProUGUI elapsedTimeText;
    public GameObject notePrefab;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject progressBar;

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
    private AudioSource[] audioArray;
    private float timeElapsed = 0;
    private CAudioDelay cAudioDelay;

    void Awake(){
        audioArray = GetComponents<AudioSource>();
        columns = audioArray.Length;
        print(columns);


        for (int i = 0; i < columns; i++) {columnCooldowns.Add(i, 0);}


        RectTransform canvasRt = canvas.GetComponent<RectTransform>();
        screenWidth = canvasRt.sizeDelta.x;
        screenHeight = canvasRt.sizeDelta.y;


        cAudioDelay = GameObject.Find("AudioPlayer").GetComponent<CAudioDelay>();


        //print("screenWidth: " + screenWidth);
        //print("screenHeight: " + screenHeight);
    }


    void Start()
    {
        Time.timeScale = 1;

        RectTransform lrt = onBeatTestingLine.GetComponent<RectTransform>();
        lrt.anchoredPosition = new Vector2(lrt.anchoredPosition.x, (onBeatHeight * screenHeight) - (screenHeight / 2));

        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        StartCoroutine(ProgressBar());
    }

    
    void Update()
    {
        ContinousSpawning();
        timeElapsed += Time.deltaTime;
        elapsedTimeText.SetText("Time Elapsed: " + timeElapsed.ToString("F2"));


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
            Time.timeScale = 0;
            cAudioDelay.PauseAudio();
            loseScreen.SetActive(true);
            print("You lose!");
        }
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene("SimpleRhythm");
    }


    IEnumerator ProgressBar()
    {
        RectTransform backgroundCanvasRt = backgroundCanvas.GetComponent<RectTransform>();
        float bgScreenWidth = backgroundCanvasRt.sizeDelta.x;

        RectTransform prt = progressBar.GetComponent<RectTransform>();

        float elapsedTime = 0;
        while (elapsedTime < songLength)
        {
            elapsedTime += Time.deltaTime;
            prt.sizeDelta = new Vector2(Mathf.Lerp(0, bgScreenWidth, elapsedTime/songLength), prt.sizeDelta.y);
            yield return null;
        }

        prt.sizeDelta = new Vector2(bgScreenWidth, prt.sizeDelta.y);
    }
}