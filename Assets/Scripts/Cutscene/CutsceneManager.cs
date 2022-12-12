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
    public GameObject thirdChapter;
    public string tileMatching;
    public string ticTacToe;
    public string rythym;

    private List<GameObject> cutsceneList = new List<GameObject>();
    public int sceneNumber;

    public GameObject firstGameplayScene;
    public GameObject secondGameplayScene;
    public GameObject thirdGameplayScene;
    public GameObject fourthGameplayScene;
    public GameObject fifthGameplayScene;
    public GameObject sixthGameplayScene;


    private void ChangeScene()
    {
        //FindObjectOfType<AudioManager>().Play("FlipPage");
        cutsceneList[sceneNumber - 1].SetActive(false);
        cutsceneList[sceneNumber].SetActive(true);
    }

    private void ChangeSceneBack()
    {
        //FindObjectOfType<AudioManager>().Play("FlipPage");
        cutsceneList[sceneNumber + 1].SetActive(false);
        cutsceneList[sceneNumber].SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!settingsMenu.activeSelf)
        {
            Handheld.Vibrate();
            sceneNumber++;

            if (sceneNumber == cutsceneList.IndexOf(firstGameplayScene) - 1 && cutscenes == firstChapter)
            {
                PlayerPrefs.SetInt("played", 1);
                SceneManager.LoadScene(tileMatching);

                Debug.Log("got here tile");
            }
            else if (sceneNumber == cutsceneList.IndexOf(secondGameplayScene) - 1 && cutscenes == firstChapter)
            {
                PlayerPrefs.SetInt("played", 2);
                SceneManager.LoadScene(tileMatching);
            }
            else if (sceneNumber == cutsceneList.IndexOf(thirdGameplayScene) - 1 && cutscenes == secondChapter)
            {
                PlayerPrefs.SetInt("played", 4);
                SceneManager.LoadScene(ticTacToe);

                Debug.Log("got here tictac");
            }
            else if (sceneNumber == cutsceneList.IndexOf(fourthGameplayScene) - 1 && cutscenes == secondChapter)
            {
                PlayerPrefs.SetInt("played", 5);
                SceneManager.LoadScene(ticTacToe);
            } else if (sceneNumber == cutsceneList.IndexOf(fifthGameplayScene) - 1 && cutscenes == thirdChapter){
                PlayerPrefs.SetInt("played", 7);
                SceneManager.LoadScene(rythym);

                Debug.Log("got here rythym");
            } else if (sceneNumber == cutsceneList.IndexOf(sixthGameplayScene) - 1 && cutscenes == thirdChapter){
                PlayerPrefs.SetInt("played", 8);
                SceneManager.LoadScene(rythym);
            }
            else if (sceneNumber < cutscenes.transform.childCount)
            {
                ChangeScene();
            }
            else if(PlayerPrefs.GetInt("played") == 2){
                cutscenes.SetActive(false);
                PlayerPrefs.SetInt("played",3);
                cutscenes = secondChapter;
                Setup();
            } else if (PlayerPrefs.GetInt("played") == 5){
                cutscenes.SetActive(false);
                PlayerPrefs.SetInt("played",6);
                cutscenes = thirdChapter;
                Setup();
            }
            {
                //SceneManager.LoadScene("Chapter2");
            }
        }

    }

    void Start()
    {
        if(PlayerPrefs.HasKey("played")){
            if(PlayerPrefs.GetInt("played") < 3){
                cutscenes = firstChapter;
            } else if(PlayerPrefs.GetInt("played") > 2 && PlayerPrefs.GetInt("played") < 6){
                cutscenes = secondChapter;
            } else{
                cutscenes = thirdChapter;
            }
        } else {
            cutscenes = firstChapter;
        }

        Setup();

    }

    void Setup(){
        sceneNumber = 0;
        cutscenes.SetActive(true);

        cutsceneList.Clear();

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
            } else if (PlayerPrefs.GetInt("played") == 7){
                start = cutsceneList.IndexOf(fifthGameplayScene) + 1;
            } else if (PlayerPrefs.GetInt("played") == 8){
                start = cutsceneList.IndexOf(fifthGameplayScene) + 1;
            }
        }

        for (int i = 0; i < cutscenes.transform.childCount; i++)
        {
            cutsceneList[i].SetActive(false);
        }

        sceneNumber = start;
        cutsceneList[start].SetActive(true);
    }

    public void backButton()
    {
        if (sceneNumber > 0 && !settingsMenu.activeSelf) 
        {
            if(sceneNumber - 1 != cutsceneList.IndexOf(firstGameplayScene) && sceneNumber - 1 != cutsceneList.IndexOf(secondGameplayScene) && cutscenes == firstChapter)
            {
                sceneNumber--;
                ChangeSceneBack();
            }
            else if (sceneNumber - 1 != cutsceneList.IndexOf(thirdGameplayScene) && sceneNumber - 1 != cutsceneList.IndexOf(fourthGameplayScene) && cutscenes == secondChapter)
            {
                sceneNumber--;
                ChangeSceneBack();
            }
            else if (sceneNumber - 1 != cutsceneList.IndexOf(fifthGameplayScene) && sceneNumber - 1 != cutsceneList.IndexOf(sixthGameplayScene) && cutscenes == secondChapter)
            {
                sceneNumber--;
                ChangeSceneBack();
            }
        }
       }
}