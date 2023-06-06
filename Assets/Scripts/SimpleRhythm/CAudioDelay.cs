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


    void Start()
    {
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();

        audioChoice = nSS.audioChoice;

        audioArray = audioObjects[audioChoice].GetComponents<AudioSource>();

        StartCoroutine(DelayAudio());
    }


    IEnumerator DelayAudio()  //TO DO - MAKE THE RHYTHM AUDIO USE THE AUDIO MANAGER SO THAT THE VOLUME SLIDERS WORK FOR IT - Cai
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

    
    public void PauseAudio()
    {
        foreach (AudioSource audioSource in audioArray)
        {
            audioSource.Pause();
        }
    }
}