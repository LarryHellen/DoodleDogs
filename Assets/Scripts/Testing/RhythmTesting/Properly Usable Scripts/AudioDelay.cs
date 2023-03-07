using UnityEngine;
using System.Collections;

public class AudioDelay : MonoBehaviour
{
    //Init TimeToOnBeatLocation (Get from main spanwer script)
    //Init AudioSource
    //Init AudioMixer


    void Start()
    {
        //Set the audio to be silenced completely -> "_MasterMixer.SetFloat("silencePlease", -80f);"

        //AudioSource.PlayDelayed(TimeToOnBeatLocation + MaxHoldNoteLength); 
    }
}
