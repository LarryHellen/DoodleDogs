using UnityEngine;
using UnityEngine.UI;

public class PlaySoundInCutscenes : MonoBehaviour
{
    public string soundName;


    void OnEnable()
    {
        PlaySoundFromString();
    }


    private void PlaySoundFromString()
    {
        FindObjectOfType<AudioManager>().Play(soundName);
    }
}