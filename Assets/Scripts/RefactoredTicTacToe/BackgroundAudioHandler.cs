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
        //Commenting this out to make this game's music act like the rest of the games'
        //this.GetComponent<AudioSource>().Pause();
    }


    public void UnPauseAudio()
    {
        //Commenting this out to make this game's music act like the rest of the games'
        //this.GetComponent<AudioSource>().UnPause();
    }
}