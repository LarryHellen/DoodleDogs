using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]

public class FourAudioNoteSpawner : MonoBehaviour
{
    public GameObject Note;

    List<AudioSource> audioSourceList = new List<AudioSource>();

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
    private float interval;
    private float period = 0;
    private bool running = true;
    private int intervalsPast = 0;

    public int percentHigherThanCloseAvg1;
    public int percentHigherThanCloseAvg2;
    public int percentHigherThanCloseAvg3;
    public int percentHigherThanCloseAvg4;

    public float minimumMaxFreq;


    private float[] samples1 = new float[64];
    private float[] samples2 = new float[64];
    private float[] samples3 = new float[64];
    private float[] samples4 = new float[64];



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



    void Start()
    {
        interval = 60 / BPM;

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
    }


    void Update()
    {
        if (running)
        {
            

            if (period > interval)
            {
                patternONotes = GetSpectrumAudioSource();
                //print(patternONotes[0] + " " + patternONotes[1] + " " + patternONotes[2] + " " + patternONotes[3]);
                FullNoteList.Add(patternONotes);
                //print("FULL NOTE STUFF" + FullNoteList[intervalsPast][0] + " " + FullNoteList[intervalsPast][1] + " " + FullNoteList[intervalsPast][2] + " " + FullNoteList[intervalsPast][3]);




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

                    if (FullNoteList[intervalsPast][i] == 1)
                    {
                        GameObject ANote = Instantiate(Note, new Vector3(distanceBetween * i - noteOffset, spawnHeight, 0), Quaternion.identity);
                        ANote.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
                        ANote.GetComponent<MainNoteScript>().NOTE_TYPE = "TAP";
                    }
                }

                period = 0;
                intervalsPast++;
            }
            period += UnityEngine.Time.deltaTime;
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
}