using UnityEngine;
using System.Collections;


public class AudioDelay : MonoBehaviour
{
    [Header("Manual Variables")]
    public AudioSource audioSource;
    //private NoteSpawningSystem nSS;
    private ContinousNSS nSS;
    private float timeToDelay = 0;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nSS = GameObject.Find("NoteSpawnManager").GetComponent<ContinousNSS>();


        timeToDelay = (nSS.timeToOnBeatLocation + nSS.spawnDelay); // * nSS.intervalLength;


        //Delay the start of the audible audio by the time it takes for a note to scroll into position and the max hold note length
        audioSource.PlayDelayed(timeToDelay);


        //float intervalsTillMusicStart = nSS.timeToOnBeatLocation + nSS.maxHoldNoteLength;

        //print("Music will start in " + intervalsTillMusicStart + " intervals.");

        //StartCoroutine(WhenDoesMusicStart());
    }

    IEnumerator WhenDoesMusicStart()
    {
        yield return new WaitForSeconds(timeToDelay);

        print("Music Started Now");
    }
}