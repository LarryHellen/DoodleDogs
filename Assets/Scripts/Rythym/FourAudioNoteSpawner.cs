using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]

public class FourAudioNoteSpawner : MonoBehaviour
{
    public GameObject Note;

    public GameObject HoldNote;

    public Canvas NoteCanvas;

    public List<AudioSource> audioSourceList;

    List<float[]> sampleList = new List<float[]>();

    List<int> percentHigherList = new List<int>();

    //Make 4 vars for 4 audios
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;

    //Make 4 vars for 4 audio spectrum datas
    public int sampleSize = 64;


    public float BPM;

    public float timeBetweenNotes;


    private float interval;
    private float period = 0;
    private bool running = true;
    public int intervalsPast = 0;

    public int percentHigherThanCloseAvg1;
    public int percentHigherThanCloseAvg2;
    public int percentHigherThanCloseAvg3;
    public int percentHigherThanCloseAvg4;

    public float minimumMaxFreq;


    private float[] samples1 = new float[64];
    private float[] samples2 = new float[64];
    private float[] samples3 = new float[64];
    private float[] samples4 = new float[64];

    public bool advanced;

    public TextSetScript tss;


    public static List<int> patternONotes = new List<int>();



    public static List<List<int>> FullNoteList = new List<List<int>>();

    public float distanceBetween;

    public float noteOffset;

    public float spawnHeight;


    public bool prioritizeLane1;
    public bool prioritizeLane2;
    public bool prioritizeLane3;
    public bool prioritizeLane4;

    private List<bool> lanePrioritization = new List<bool>();


    public int maxHoldNoteLength;

    private List<GameObject> allNotes;

    public GameObject loseScreen;
    public GameObject winScreen;




    void DeleteAllGameObjectsInList(List<GameObject> listOfGameObjects)
    {
        foreach(GameObject specificGameObject in listOfGameObjects)
        {
            Destroy(specificGameObject);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("RythymDemo");
    }
    
    public void Setup()
    {
        print("SETTING UP RIGHT NOW");
        try
        {
            DeleteAllGameObjectsInList(allNotes);
        }
        catch
        {
            //Do Nothing
        }

        Time.timeScale = 1;



        allNotes = new List<GameObject>();

        



        winScreen.SetActive(false);

        loseScreen.SetActive(false);



        //interval = 60 / BPM;

        interval = timeBetweenNotes;

        samples1 = new float[sampleSize];
        samples2 = new float[sampleSize];
        samples3 = new float[sampleSize];
        samples4 = new float[sampleSize];

        audioSourceList = new List<AudioSource>()
        {
            audioSource1,
            audioSource2,
            audioSource3,
            audioSource4
        };

        sampleList = new List<float[]>()
        {
            samples1,
            samples2,
            samples3,
            samples4
        };


        percentHigherList = new List<int>()
        {
            percentHigherThanCloseAvg1,
            percentHigherThanCloseAvg2,
            percentHigherThanCloseAvg3,
            percentHigherThanCloseAvg4
        };

        lanePrioritization.Add(prioritizeLane1);
        lanePrioritization.Add(prioritizeLane2);
        lanePrioritization.Add(prioritizeLane3);
        lanePrioritization.Add(prioritizeLane4);

        FullNoteList.Add(new List<int>() { 1, 0, 0, 0 });

        tss = FindObjectOfType<TextSetScript>();



        period = 0;
        running = true;
        intervalsPast = 0;
    }

    void Start()
    {
        Time.timeScale = 1;
        LoadByJSON();

        Setup();
    }


    void Update()
    {
        if (running)
        {
            //print("Gameplay");
            

            if (period > interval)
            {
                patternONotes = GetSpectrumAudioSource();
                //print(patternONotes[0] + " " + patternONotes[1] + " " + patternONotes[2] + " " + patternONotes[3]);
                FullNoteList.Add(patternONotes);
                //print("FULL NOTE STUFF" + FullNoteList[intervalsPast][0] + " " + FullNoteList[intervalsPast][1] + " " + FullNoteList[intervalsPast][2] + " " + FullNoteList[intervalsPast][3]);


                //print("Gameplay");


                for (int i = 0; i < 4; i++)
                {
                    if (FullNoteList[intervalsPast][0] + FullNoteList[intervalsPast][1] + FullNoteList[intervalsPast][2] + FullNoteList[intervalsPast][3] >= 3)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (lanePrioritization[j] == true && FullNoteList[intervalsPast][j] == 1)
                            {
                                FullNoteList[intervalsPast][j] = 1;
                            }
                            else
                            {
                                FullNoteList[intervalsPast][j] = 0;
                            }
                        }
                    }


                    if (intervalsPast - maxHoldNoteLength < 0)
                    {
                        print("Saved from getting an exception because of delayed note spawning");

                        //Just don't do the below thing lol
                    }
                    else if (FullNoteList[intervalsPast - maxHoldNoteLength][i] == 1)
                    {
                        int lengthOfHoldNote = 0;


                        for (int j = 0; j < maxHoldNoteLength; j++)
                        {
                            if (FullNoteList[intervalsPast - maxHoldNoteLength + j + 1][i] == 1)
                            {
                                lengthOfHoldNote++;
                            }
                            else
                            {
                                break;
                            }
                        }


                        Vector3 tempPos = new Vector3(distanceBetween * i - noteOffset, spawnHeight, 0);

                        if (lengthOfHoldNote > 1 && advanced == true)
                        {
                            print("Hold Note Spawn");

                            for (int j = 0; j < maxHoldNoteLength; j++)
                            {
                                FullNoteList[intervalsPast - maxHoldNoteLength + j + 1][i] = 0;
                            }

                            

                            GameObject ANote = Instantiate(HoldNote, tempPos, Quaternion.identity); //CHANGE THE GAMEOBJECT TO A HOLD NOTE GAMEOBJECT? Possibly stretch a gameobject depending on lengthofHoldNote then rest of normal note code



                            ANote.GetComponent<HoldNoteScript>().lengthOfHoldNote = lengthOfHoldNote;
                            print("Hold Note Length : " + lengthOfHoldNote);
                            ANote.GetComponent<HoldNoteScript>().ScalingTime();



                            ANote.transform.SetParent(NoteCanvas.transform, false);


                            //ANote.transform.position = tempPos;


                            //ANote.transform.SetParent(AllTheNotes.transform, false);


                            
                            //ANote.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
                            ANote.GetComponent<MainNoteScript>().NOTE_TYPE = "HOLD";
                            allNotes.Add(ANote);
                        }
                        else
                        {
                            GameObject ANote = Instantiate(Note, tempPos, Quaternion.identity);


                            ANote.transform.SetParent(NoteCanvas.transform, false);


                            //ANote.transform.position = tempPos;


                            //ANote.transform.SetParent(AllTheNotes.transform, false);


                            ANote.GetComponent<MainNoteScript>().NOTE_TYPE = "TAP";
                            allNotes.Add(ANote);
                        }


                    }
                }

                period = 0;
                intervalsPast++;

                if (intervalsPast * interval > 50f)
                {
                    tss.RhythmGameWinCondition();
                    print("song ended");
                }
            }
            period += Time.deltaTime;
        }
    }



    List<int> GetSpectrumAudioSource()
    {
        List<int> thisTimeAround = new List<int>() { 0, 0, 0, 0 };

        for (int i = 0; i < 4; i++)
        {

            audioSourceList[i].GetSpectrumData(sampleList[i], 0, FFTWindow.Blackman);


            if (IsMaxIntensityGreaterThanAvg(sampleList[i], i))
            {
                //print(i);
                thisTimeAround[i] = 1;
            }
        }

        //print(thisTimeAround[0] + " " + thisTimeAround[1] + " " + thisTimeAround[2] + " " + thisTimeAround[3]);
        return thisTimeAround;
    }

    bool IsMaxIntensityGreaterThanAvg(float[] samples, int lane)
    {
        float maxFreq = samples.Max();
        float avg = FindAvg(samples);

        if (maxFreq > avg * ((100 + percentHigherList[lane]) / 100) && maxFreq > minimumMaxFreq)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float FindAvg(float[] samples)
    {
        float avg;
        float total = 0f;

        foreach (float num in samples)
        {
            total += num;
        }

        avg = total / samples.Count();

        return avg;
    }

    private void LoadFromPlayerData(PlayerData tempData)
    {
        int cutsceneNum = tempData.sceneNumber;
        if (cutsceneNum == 11)
        {
            advanced = true;
        }
    }

    //This was originally private, I made it public so I could access it in other scripts. Was there a reason for making it private?
    //A: Mostly if we wanted another LoadByJSON script for a different save class. Ask me about it later FIXME: Delete if you understand
    public void LoadByJSON()
    {
        if(!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"))){
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"));
        }
        if (File.Exists(Application.persistentDataPath+"/Saves/JSONData.text"))
        {
            //LOAD THE GAME
            StreamReader sr = new StreamReader(Application.persistentDataPath+"/Saves/JSONData.text");

            string JsonString = sr.ReadToEnd();

            sr.Close();

            //Convert JSON to the Object(PlayerData)
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(JsonString);

            LoadFromPlayerData(playerData);

        }
        else
        {
            Debug.Log("NOT FOUND FILE");
        }
    }
}