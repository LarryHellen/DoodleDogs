using UnityEngine;
using System.Collections;


public class CAudioDelay : MonoBehaviour
{
    [Header("Manual Variables")]
    public AudioSource[] audioSources;
    private ContinousNoteSpawning nSS;
    private float timeToDelay = 0;


    void Start()
    {
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();

        timeToDelay = nSS.timeToOnBeatHeight;

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.PlayDelayed(timeToDelay);
        }

        
    }


    public void PauseAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }
}