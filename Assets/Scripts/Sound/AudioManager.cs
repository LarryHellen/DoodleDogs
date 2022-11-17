using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] Slider volumeSliderMusic;
    [SerializeField] Slider volumeSliderSFX;

    void Start(){
        
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

        

        //Play("AfghanMusicBackground");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
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
}
