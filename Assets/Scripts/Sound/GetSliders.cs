using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class GetSliders : MonoBehaviour
{
    public Slider volumeSliderMusic;
    public Slider volumeSliderSFX;

    private AudioManager audioManager;


    private void OnEnable() //This script is just so that in the rare case that the usual getting of sliders doesn't work, that there is this
    {
        audioManager = FindObjectsOfType<AudioManager>()[0];

        audioManager.volumeSliderMusic = volumeSliderMusic;
        audioManager.volumeSliderSFX = volumeSliderSFX;
    }
}