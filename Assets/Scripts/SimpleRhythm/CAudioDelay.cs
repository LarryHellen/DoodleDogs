using UnityEngine;
using System.Collections;


public class CAudioDelay : MonoBehaviour
{
    [Header("Manual Variables")]
    private int audioChoice;
    public GameObject[] audioObjects;

    [Space(25)]


    [Header("Automatic Variables")]
    public AudioSource[] audioArray;
    private ContinousNoteSpawning nSS;
    private float timeToDelay = 0;


    void Start()
    {
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();
        timeToDelay = nSS.timeToOnBeatHeight;

        audioChoice = nSS.audioChoice;

        audioArray = audioObjects[audioChoice].GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioArray)
        {
            audioSource.PlayDelayed(timeToDelay);
        }

        
    }


    public void PauseAudio()
    {
        foreach (AudioSource audioSource in audioArray)
        {
            audioSource.Pause();
        }
    }
}