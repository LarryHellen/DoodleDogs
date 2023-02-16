using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class OtherNoteLayerTesting : MonoBehaviour
{
    AudioSource _audioSource;

    public int sampleSize = 64;

    private float[] samples = new float[64];

    public float BPM;
    private float interval;
    private float period = 0;


    private int[] thisTimeAround = new int[4];

    public static List<List<int>> fullNoteList = new List<List<int>>();

    private bool running = true;

    public int percentHigherThanCloseAvg;

    private double maximumFreq;

    public List<int> allDaKeys;

    // Start is called before the first frame update
    void Start()
    {
        samples = new float[sampleSize];

        interval = 60 / BPM;

        maximumFreq = sampleSize;

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running == true)
        {

            List<int> patternONotes = GetSpectrumAudioSource();

            if (period > interval)
            {
                NoteSpawningScript.FullNoteList.Add(patternONotes);
                period = 0;
            }
            period += UnityEngine.Time.deltaTime;

        }
    }

    public void Pause()
    {

        string wholeLine = "{";

        for (int i = 0; i < fullNoteList.Count(); i++)
        {

            wholeLine += "new List<int>() {";

            for (int j = 0; j < 4; j++)
            {
                wholeLine += fullNoteList[i][j];
                if (j != 3)
                {
                    wholeLine += ",";
                }
            }

            wholeLine += "},\n";
        }

        wholeLine += "}";
        print(wholeLine);

        running = false;
    }


    void ShowAll(List<int> theList)
    {
        foreach (int num in theList)
        {
            print(num);
        }

        print("------------------");
    }



    List<int> GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);


        List<int> thisTimeAround = new List<int>() { 0, 0, 0, 0 };


        Dictionary<int, float> freqNumAndIntensity = new Dictionary<int, float>();

        freqNumAndIntensity = new Dictionary<int, float>();

        for (int i = 0; i < samples.Count(); i++)
        {
            freqNumAndIntensity[i] = samples[i];
        }


        var sortedDict = freqNumAndIntensity.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value); //Sorts Dict Values from Least to Greatest

        //print(String.Join(", ", sortedDict));

        allDaKeys = sortedDict.Keys.ToList();

        //USE MAXIMUM FREQ AND LOOK AT 4 SECTIONS WITHIN THAT
        //MAXIMUM FREQ SHOULD PRETTY MUCH BE LIKE SELECTION AREA
        List<List<int>> LANES = new List<List<int>>()
        {
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        };



        for (int i = 0; i < maximumFreq; i++)
        {
            int index = (int)Math.Floor(allDaKeys[allDaKeys.Count - i - 1] / (maximumFreq / 4));
            LANES[index].Add(allDaKeys[allDaKeys.Count - i - 1]);
        }

        for (int i = 0; i < 4; i++)
        {
            List<float> actualFreqs = new List<float>();
            foreach (int num in LANES[i])
            {
                actualFreqs.Add(freqNumAndIntensity[num]);
            }

            double avg = Queryable.Average(actualFreqs.AsQueryable());
            float maxFreq = actualFreqs.Max(x => x);

            if (maxFreq > avg * ((100 + percentHigherThanCloseAvg) / 100))
            {
                thisTimeAround[i] = 1;
            }
        }

        return thisTimeAround;
    }
}