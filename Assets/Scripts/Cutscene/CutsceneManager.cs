using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    public GameObject cutscenes;
    public string nextMinigame;

    private List<GameObject> cutsceneList = new List<GameObject>();
    private int sceneNumber = 0;


    private void ChangeScene()
    {
        FindObjectOfType<AudioManager>().Play("FlipPage");
        cutsceneList[sceneNumber - 1].SetActive(false);
        cutsceneList[sceneNumber].SetActive(true);
    }


    private void OnMouseDown()
    {
        
        sceneNumber++;
        if (sceneNumber < cutscenes.transform.childCount)
        {
            ChangeScene();
        }
        else
        {
            SceneManager.LoadScene(nextMinigame);
        }

    }

    void Start()
    {
        foreach (Transform child in cutscenes.transform)
        {
            cutsceneList.Add(child.gameObject);
        }

        for (int i = 1; i < cutscenes.transform.childCount; i++)
        {
            cutsceneList[i].SetActive(false);
        }

    }
}