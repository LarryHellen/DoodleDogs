using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]

public class FourAudioNoteSpawner : MonoBehaviour
{
    List<AudioSource> audioSourceList = new List<AudioSource>();

    List<float[]> sampleList = new List<float[]>();

    //Make 4 vars for 4 audios
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;

    //Make 4 vars for 4 audio spectrum datas
    public int sampleSize = 64;

    public float[] samples1 = new float[64];
    public float[] samples2 = new float[64];
    public float[] samples3 = new float[64];
    public float[] samples4 = new float[64];

    public float BPM;
    private float interval;
    private float period = 0;
    private bool running = true;
    public int percentHigherThanCloseAvg;

    void Start()
    {
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
    }


    void Update()
    {
        if (running)
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


    List<int> GetSpectrumAudioSource()
    {
        List<int> thisTimeAround = new List<int>() { 0, 0, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            audioSourceList[i].GetSpectrumData(sampleList[i], 0, FFTWindow.Blackman);

            if (IsMaxIntensityGreaterThanAvg(sampleList[i]))
            {
                thisTimeAround[i] = 1;
            }
        }

        return thisTimeAround;
    }

    bool IsMaxIntensityGreaterThanAvg(float[] samples)
    {
        float maxFreq = samples.Max();
        float avg = FindAvg(samples);

        if (maxFreq > avg * ((100 + percentHigherThanCloseAvg) / 100))
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