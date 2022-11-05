using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class OtherNoteLayerTesting : MonoBehaviour
{
    AudioSource _audioSource;

    public float[] samples = new float[64];

    public float BPM;
    private float interval;
    private float period = 0;


    private int[] thisTimeAround = new int[4];

    public static List<List<int>> fullNoteList = new List<List<int>>();

    private bool running = true;

    public int avgRangeAroundIntenseFrequencies;

    public int percentHigherThanCloseAvg;

    // Start is called before the first frame update
    void Start()
    {
        interval = 60 / BPM;

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
                fullNoteList.Add(patternONotes);
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


    void ShowAll(List<float> theList)
    {
        foreach (float num in theList)
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

        List<int> allDaKeys = sortedDict.Keys.ToList();
        List<int> top4Intesities = new List<int>() { allDaKeys[allDaKeys.Count - 1], allDaKeys[allDaKeys.Count - 2], allDaKeys[allDaKeys.Count - 3], allDaKeys[allDaKeys.Count - 4] }; //Keys of the Top 4 intensities


        int theIndex = 0;

        foreach (int elementPosition in top4Intesities)
        {
            float total = 0;

            for (int j = 0; j < avgRangeAroundIntenseFrequencies; j++)
            {
                try
                {
                    total += samples[elementPosition + j + 1];
                }
                catch
                {
                    //Just skip it lol
                }
                
            }

            for (int j = 0; j < avgRangeAroundIntenseFrequencies; j++)
            {
                try
                {
                    total += samples[elementPosition - j - 1];
                }
                catch
                {
                    //Skipping it again
                }
            }

            float avg = total / (avgRangeAroundIntenseFrequencies*2);

            if (samples[elementPosition] > avg * ((100 + percentHigherThanCloseAvg) / 100))
            {
                thisTimeAround[theIndex] = 1;
            }


            theIndex++;
        }

        //avg intensities close to it to see if its an anomaly if so put a 1 in that lane

        return thisTimeAround;
    }
}
