using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewScene : MonoBehaviour
{
    public string scene = "";
    public bool reset;

    public void GoToScene()
    {
        if(reset){
            PlayerPrefs.SetInt("played",0);
        }
        SceneManager.LoadScene(scene);
    }
}
