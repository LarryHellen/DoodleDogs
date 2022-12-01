using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOffsettingScript : MonoBehaviour
{


    public AudioSource audioSource;
    public AudioMixer _MasterMixer;
    public float delay;




    // Start is called before the first frame update
    void Start()
    {
        _MasterMixer.SetFloat("silencePlease", -80f);
        audioSource.PlayDelayed(delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
