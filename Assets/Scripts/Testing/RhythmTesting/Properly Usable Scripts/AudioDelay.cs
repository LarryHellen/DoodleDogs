using UnityEngine;
using System.Collections;

public class AudioDelay : MonoBehaviour
{
    //Init ExtraAudioDelayInIntervals
    //Init AudioSource
    //Init AudioMixer


    void Start()
    {
        //Set the audio to be silenced completely -> "_MasterMixer.SetFloat("silencePlease", -80f);"

        //AudioSource.PlayDelayed(ExtraAudioDelayInIntervals + MaxHoldNoteLength); 
    }
}
