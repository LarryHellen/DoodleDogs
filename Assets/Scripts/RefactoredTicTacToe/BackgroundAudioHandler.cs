using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundAudioHandler : MonoBehaviour 
{
    void Awake()
    {
       if (FindObjectsOfType<BackgroundAudioHandler>().Length > 1)
       {
           Destroy(this.gameObject);
       }
       else
       {
           DontDestroyOnLoad(this.gameObject);
       }
    }


    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
 
         string sceneName = currentScene.name;

        if (sceneName != "RefactoredTicTacToe")
        {
            Destroy(this.gameObject);
        }
    }


    public void PauseAudio()
    {
        this.GetComponent<AudioSource>().Pause();
    }


    public void UnPauseAudio()
    {
        this.GetComponent<AudioSource>().UnPause();
    }
}