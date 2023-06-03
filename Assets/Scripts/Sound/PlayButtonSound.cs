using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySoundFromString);
    }


    public void PlaySoundFromString()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSFX");
    }
}