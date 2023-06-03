using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] Slider volumeSliderMusic;
    [SerializeField] Slider volumeSliderSFX;

    private SoundManagerMusic soundManagerMusic;
    private SoundManagerSFX soundManagerSFX;

    private Sound currentlyPlayingSong = null;

    private bool dontDestroyOnLoad = false;


    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (FindObjectsOfType<AudioManager>().Length > 1) 
        { 
            if (!dontDestroyOnLoad)
            {
                print("Destroying duplicate AudioManager");
                Destroy(this.gameObject); 
            }
        }
        else 
        { 
            DontDestroyOnLoad(this.gameObject);
            SetSliders();
            dontDestroyOnLoad = true;
        }
    }   

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s.isSong) //I added this so that songs can loop as opposed to playing once - Cai
        {
            s.source.loop = true;
        }

        if (!(currentlyPlayingSong == s) && s.isSong) 
        {
            if (currentlyPlayingSong != null) { currentlyPlayingSong.source.Stop(); }

            s.source.Play();
            
            currentlyPlayingSong = s;
        }

        if (!s.isSong)
        {
            s.source.Play();
        }
    }

    private void Update()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].isSong)
            {
                sounds[i].source.volume = volumeSliderMusic.value;

            }
            else
            {
                sounds[i].source.volume = volumeSliderSFX.value;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetSliders();
    }

    private void SetSliders()
    {
        GetSliders getSliders = FindObjectOfType<GetSliders>();
        volumeSliderMusic = getSliders.volumeSliderMusic;
        volumeSliderSFX = getSliders.volumeSliderSFX;
    }
}
