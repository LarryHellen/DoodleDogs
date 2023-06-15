using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public bool isSong;

    public bool isRhythmSong;

    public AudioClip clip;

    [Range(0f, 10f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public AudioSource source;
}
