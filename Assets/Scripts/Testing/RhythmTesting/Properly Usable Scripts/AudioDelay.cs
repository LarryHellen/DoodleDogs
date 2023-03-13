using UnityEngine;
using System.Collections;


public class AudioDelay : MonoBehaviour
{
    //Init TimeToOnBeatLocation (Get from main spanwer script)
    //Init AudioSource


    public AudioSource audioSource;

    private NoteSpawningSystem noteSpawningSystem;


    void Start()
    {
        noteSpawningSystem = GameObject.Find("NoteSpawnManager").GetComponent<NoteSpawningSystem>();

        //Delay the start of the audible audio by the time it takes for a note to scroll into position and the max hold note length
        audioSource.PlayDelayed(noteSpawningSystem.timeToOnBeatLocation + noteSpawningSystem.maxHoldNoteLength*noteSpawningSystem.intervalLength); 
    }
}