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

    public GameObject firstGameplayScene;
    public GameObject secondGameplayScene;


    private void ChangeScene()
    {
        FindObjectOfType<AudioManager>().Play("FlipPage");
        cutsceneList[sceneNumber - 1].SetActive(false);
        cutsceneList[sceneNumber].SetActive(true);
    }


    private void OnMouseDown()
    {
        
        sceneNumber++;
        if(sceneNumber == cutsceneList.IndexOf(firstGameplayScene)){
            PlayerPrefs.SetInt("played",1);
            SceneManager.LoadScene(nextMinigame);
        } else if(sceneNumber == cutsceneList.IndexOf(secondGameplayScene)){
            PlayerPrefs.SetInt("played",2);
            SceneManager.LoadScene(nextMinigame);
        }
        else if (sceneNumber < cutscenes.transform.childCount)
        {
            ChangeScene();
        }
        else
        {
            SceneManager.LoadScene("Chapter2");
        }

    }

    void Start()
    {
        foreach (Transform child in cutscenes.transform)
        {
            cutsceneList.Add(child.gameObject);
        }

        int start = 0;

        if(PlayerPrefs.HasKey("played")){
            if(PlayerPrefs.GetInt("played") == 1){
                start = cutsceneList.IndexOf(firstGameplayScene) + 1;
            } else if (PlayerPrefs.GetInt("played") == 2){
                start = cutsceneList.IndexOf(secondGameplayScene) + 1;
            }
        }

        for (int i = 0; i < cutscenes.transform.childCount; i++)
        {
            cutsceneList[i].SetActive(false);
        }

        sceneNumber = start;
        cutsceneList[start].SetActive(true);

    }
}