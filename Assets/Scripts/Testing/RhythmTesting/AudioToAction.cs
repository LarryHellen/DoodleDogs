using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToAction : MonoBehaviour
{

    public AudioSource sound;

    float bigSound = 0f;
    float bigSoundBefore = 0f;

    public float necessarySoundLoudness;

    public float[] sounds = new float[128];

    void Start()
    {
        
    }

    
    void Update()
    {
        sound.GetSpectrumData(sounds, 0, FFTWindow.Rectangular);

        bigSoundBefore = bigSound;
        bigSound = sounds[0] * 100;

        if (bigSound > necessarySoundLoudness && bigSoundBefore <= necessarySoundLoudness)
        {
            print("Beat Detected");
        }
    }
}
