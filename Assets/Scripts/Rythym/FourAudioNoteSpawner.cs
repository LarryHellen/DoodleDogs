using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]

public class FourAudioNoteSpawner : MonoBehaviour
{

    //Make 4 vars for 4 audios
    //Make 4 vars for 4 audio spectrum datas

    public float BPM;
    private float interval;
    private float period = 0;
    private bool running = true;
    public int percentHigherThanCloseAvg;

    void Start()
    {
        //Set 4 vars to 4 audios
    }


    void Update()
    {
        if (running == true)
        {
            List<int> patternONotes = GetSpectrumAudioSource();

            if (period > interval)
            {
                NoteSpawningScript.FullNoteList.Add(patternONotes); //patternONotes looks like {0, 0, 0, 0}
                period = 0;
            }
            period += UnityEngine.Time.deltaTime;

        }
    }


    List<int> GetSpectrumAudioSource()
    {
        //_audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman); (Audio Source 1-4.GetSpectrumData)


        List<int> thisTimeAround = new List<int>() { 0, 0, 0, 0 };

        //Make helper function to do max intensity to avg checking and return true or false
        //if (helperFunction(spectrumdataforthisaudio) == true){ thisTimeAround[one of them] = 1; }


        return thisTimeAround;
    }
}