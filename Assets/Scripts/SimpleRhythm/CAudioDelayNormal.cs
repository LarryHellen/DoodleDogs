using UnityEngine;
using System.Collections;


public class CAudioDelayNormal : MonoBehaviour
{
    private int audioChoice;
    private ContinousNoteSpawning nSS;

    void Start()
    {
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();

        audioChoice = nSS.audioChoice;

        StartCoroutine(DelayAudio());
    }


    IEnumerator DelayAudio()  //TO DO - MAKE THE RHYTHM AUDIO USE THE AUDIO MANAGER SO THAT THE VOLUME SLIDERS WORK FOR IT - Cai
    {
        while (CRhythmTriggers.pauseGame)
        {
            yield return null;
        }

        PlayMusic();
    }


    private void PlayMusic()
    {
        //Play Music
        if (audioChoice == 0)
        {
            //All tracks to be audibly played
            FindObjectOfType<AudioManager>().Play("rbTrack1");
            FindObjectOfType<AudioManager>().Play("rbTrack2");
            FindObjectOfType<AudioManager>().Play("rbTrack3");
            FindObjectOfType<AudioManager>().Play("rbTrack4");
            FindObjectOfType<AudioManager>().Play("rbTrackMain");
        }
        else if (audioChoice == 1)
        {
            //All tracks to be audibly played
            FindObjectOfType<AudioManager>().Play("raTrack1");
            FindObjectOfType<AudioManager>().Play("raTrack2");
            FindObjectOfType<AudioManager>().Play("raTrack3");
            FindObjectOfType<AudioManager>().Play("raTrack4");
            FindObjectOfType<AudioManager>().Play("raTrackMain");
        }
    }
}