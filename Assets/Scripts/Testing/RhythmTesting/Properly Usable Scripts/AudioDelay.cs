using UnityEngine;
using System.Collections;

public class AudioDelay : MonoBehaviour
{
    //Init TimeToOnBeatLocation (Get from main spanwer script)
    //Init AudioSource
    //public AudioMixer silencer;


    void Start()
    {

        //AudioSource.PlayDelayed(TimeToOnBeatLocation); 
    }
}

//This line should actually be geared towards the audiosources in the note spawning script (it should still occur here because it only needs to occur once):
//Set the audio to be silenced completely -> "_MasterMixer.SetFloat("silencePlease", -80f);"