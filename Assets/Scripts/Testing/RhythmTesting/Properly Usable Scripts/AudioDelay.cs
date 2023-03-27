using UnityEngine;
using System.Collections;


public class AudioDelay : MonoBehaviour
{
    [Header("Manual Variables")]
    public AudioSource audioSource;
    private NoteSpawningSystem noteSpawningSystem;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        noteSpawningSystem = GameObject.Find("NoteSpawnManager").GetComponent<NoteSpawningSystem>();

        //Delay the start of the audible audio by the time it takes for a note to scroll into position and the max hold note length
        audioSource.PlayDelayed(noteSpawningSystem.timeToOnBeatLocation + noteSpawningSystem.maxHoldNoteLength*noteSpawningSystem.intervalLength); 
    }
}