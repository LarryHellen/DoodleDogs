using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerMusic : MonoBehaviour
{
    public AudioSource AudioSource;
    [SerializeField] Slider volumeSliderMusic;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Volume (Music)"))
        {
            PlayerPrefs.SetFloat("Volume (Music)", 1f);
        }
        else
        {
            LoadMusic();
        }

    }

    public void ChangeVolume()
    {
        AudioSource.volume = volumeSliderMusic.value;
        Save();
    }

    private void LoadMusic()
    {
        volumeSliderMusic.value = PlayerPrefs.GetFloat("Volume (Music)");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Volume (Music)", volumeSliderMusic.value);
    }

}
