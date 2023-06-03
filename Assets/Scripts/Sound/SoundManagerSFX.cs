using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerSFX : MonoBehaviour
{
    [HideInInspector] public AudioSource AudioSource;
    public Slider volumeSliderMusic;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Volume (SFX)"))
        {
            PlayerPrefs.SetFloat("Volume (SFX)", 1f);
        }
        else
        {
            LoadMusic();
        }
    }

    void Update()
    {
        Save();
    }

    public void ChangeVolume()
    {
        AudioSource.volume = volumeSliderMusic.value;
        Save();
    }

    private void LoadMusic()
    {
        volumeSliderMusic.value = PlayerPrefs.GetFloat("Volume (SFX)");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Volume (SFX)", volumeSliderMusic.value);
    }
}
