using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject settingsMenu;
    private GameObject cutscenes;
    public GameObject firstChapter;
    public GameObject secondChapter;
    public string tileMatching;
    public string ticTacToe;

    private List<GameObject> cutsceneList = new List<GameObject>();
    private int sceneNumber = 0;

    public GameObject firstGameplayScene;
    public GameObject secondGameplayScene;
    public GameObject thirdGameplayScene;
    public GameObject fourthGameplayScene;


    private void ChangeScene()
    {
        FindObjectOfType<AudioManager>().Play("FlipPage");
        cutsceneList[sceneNumber - 1].SetActive(false);
        cutsceneList[sceneNumber].SetActive(true);
    }


    private void OnMouseDown()
    {
        if (!settingsMenu.activeSelf)
        {
            sceneNumber++;
            if (sceneNumber == cutsceneList.IndexOf(firstGameplayScene) && cutscenes == firstChapter)
            {
                PlayerPrefs.SetInt("played", 1);
                SceneManager.LoadScene(tileMatching);
            }
            else if (sceneNumber == cutsceneList.IndexOf(secondGameplayScene) && cutscenes == firstChapter)
            {
                PlayerPrefs.SetInt("played", 2);
                SceneManager.LoadScene(tileMatching);
            }
            else if (sceneNumber == cutsceneList.IndexOf(thirdGameplayScene) && cutscenes == secondChapter)
            {
                PlayerPrefs.SetInt("played", 4);
                SceneManager.LoadScene(ticTacToe);
            }
            else if (sceneNumber == cutsceneList.IndexOf(fourthGameplayScene) && cutscenes == secondChapter)
            {
                PlayerPrefs.SetInt("played", 5);
                SceneManager.LoadScene(ticTacToe);
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

    }

    void Start()
    {
        if(PlayerPrefs.HasKey("played")){
            if(PlayerPrefs.GetInt("played") > 2){
                cutscenes = secondChapter;
            } else {
                cutscenes = firstChapter;
            }
        } else {
            cutscenes = firstChapter;
        }

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
            } else if (PlayerPrefs.GetInt("played") == 4){
                start = cutsceneList.IndexOf(thirdGameplayScene) + 1;
            } else if (PlayerPrefs.GetInt("played") == 5){
                start = cutsceneList.IndexOf(fourthGameplayScene) + 1;
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