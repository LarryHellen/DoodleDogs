using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenuMusicBackground");
    }
}