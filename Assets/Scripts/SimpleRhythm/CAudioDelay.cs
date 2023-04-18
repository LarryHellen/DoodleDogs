using UnityEngine;
using System.Collections;


public class CAudioDelay : MonoBehaviour
{
    [Header("Manual Variables")]
    public AudioSource audioSource;
    private ContinousNoteSpawning nSS;
    private float timeToDelay = 0;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();

        timeToDelay = nSS.timeToOnBeatHeight;

        audioSource.PlayDelayed(timeToDelay);
    }


    public void PauseAudio()
    {
        audioSource.Pause();
    }
}