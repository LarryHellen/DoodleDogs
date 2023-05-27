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
    private float timeToDelay = .0000001f;


    void Start()
    {
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();
        timeToDelay = nSS.timeToOnBeatHeight;

        audioChoice = nSS.audioChoice;

        audioArray = audioObjects[audioChoice].GetComponents<AudioSource>();

        StartCoroutine(DelayAudio());
    }


    public void PauseAudio()
    {
        foreach (AudioSource audioSource in audioArray)
        {
            audioSource.Pause();
        }
    }


    IEnumerator DelayAudio()
    {
        while (CRhythmTriggers.pauseGame)
        {
            yield return null;
        }

        foreach (AudioSource audioSource in audioArray)
        {
            audioSource.Play();
        }
    }
}