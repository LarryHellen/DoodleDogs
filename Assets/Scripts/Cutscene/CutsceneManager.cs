using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class CutsceneManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject backButtonDisable;
    public GameObject cutscene1Num7, cutscene1End;
    public GameObject cutscenes;
    public GameObject firstChapter;
    public GameObject secondChapter;
    public GameObject thirdChapter;
    public GameObject fourthChapter;
    public string tileMatching;
    public string ticTacToe;
    public string ticTacToe2;
    public string rythym;

    private List<GameObject> cutsceneList = new List<GameObject>();
    public int sceneNumber;

    public GameObject firstGameplayScene;
    public GameObject secondGameplayScene;
    public GameObject thirdGameplayScene;
    public GameObject fourthGameplayScene;
    public GameObject fifthGameplayScene;
    public GameObject sixthGameplayScene;
    public GameObject seventhGameplayScene;
    public GameObject eighthGameplayScene;

    public GameObject chapter2Enable;
    public GameObject chapter3Enable;

    public int cutsceneNum;


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

    public void buttonForwardYes()
    {
        Handheld.Vibrate();
        sceneNumber++;
        if (sceneNumber == 22)
        {
            backButtonDisable.SetActive(true);
        }
        ChangeScene();

    }

    private void OnMouseDown()
    {
        if (!settingsMenu.activeSelf && !cutscene1Num7.activeSelf && !cutscene1End.activeSelf)
        {
            Handheld.Vibrate();
            sceneNumber++;

            if (sceneNumber == cutsceneList.IndexOf(firstGameplayScene) + 1 && cutscenes == firstChapter)
            {
                cutsceneNum = 1;
                sceneLoader(tileMatching);
                //SceneManager.LoadScene(tileMatching);

                //Debug.Log("got here tile");
            }
            else if (sceneNumber == cutsceneList.IndexOf(secondGameplayScene) + 1 && cutscenes == firstChapter)
            {
                cutsceneNum = 2;
                sceneLoader(tileMatching);
            }
            else if (sceneNumber == cutsceneList.IndexOf(thirdGameplayScene) + 1 && cutscenes == secondChapter)
            {
                cutsceneNum = 4;
                sceneLoader(ticTacToe);

                //Debug.Log("got here tictac");
            }
            else if (sceneNumber == cutsceneList.IndexOf(fourthGameplayScene) + 1 && cutscenes == secondChapter)
            {
                cutsceneNum = 5;
                sceneLoader(ticTacToe);
            }
            else if (sceneNumber == cutsceneList.IndexOf(fifthGameplayScene) + 1 && cutscenes == thirdChapter)
            {
                cutsceneNum = 7;
                sceneLoader(rythym);

                //Debug.Log("got here rythym");
            }
            else if (sceneNumber == cutsceneList.IndexOf(sixthGameplayScene) + 1 && cutscenes == thirdChapter)
            {
                cutsceneNum = 8;
                sceneLoader(rythym);
            }
            else if (sceneNumber == cutsceneList.IndexOf(seventhGameplayScene) + 1 && cutscenes == fourthChapter)
            {
                cutsceneNum = 10;
                sceneLoader(rythym);

                //Debug.Log("got here rythym");
            }
            else if (sceneNumber == cutsceneList.IndexOf(eighthGameplayScene) + 1 && cutscenes == fourthChapter)
            {
                cutsceneNum = 11;
                sceneLoader(rythym);
            }
            else if (sceneNumber < cutscenes.transform.childCount)
            {
                ChangeScene();
            }
            else if (cutsceneNum == 2)
            {
                cutscenes.SetActive(false);
                cutsceneNum = 3;
                cutscenes = secondChapter;
                Setup();
            }
            else if (cutsceneNum == 5)
            {
                SceneManager.LoadScene("Chapter2");
                cutscenes.SetActive(false);
                cutsceneNum = 6;
                cutscenes = thirdChapter;
                Setup();
            }
            else if (cutsceneNum == 8)
            {
                cutscenes.SetActive(false);
                cutsceneNum = 9;
                cutscenes = fourthChapter;
                Setup();
            }
            if (sceneNumber == 21)
            {
                backButtonDisable.SetActive(false);
            }
            {
                //SceneManager.LoadScene("Chapter2");
            }
        }

    }

    void Start()
    {
        //cutsceneNum = 0;
        LoadByJSON();

        if (cutsceneNum < 3)
        {
            cutscenes = firstChapter;
        }
        else if (cutsceneNum > 2 && cutsceneNum < 6)
        {
            cutscenes = secondChapter;
        }
        else if (cutsceneNum > 5 && cutsceneNum < 9)
        {
            cutscenes = thirdChapter;
        }

        Setup();

    }

    void Setup()
    {
        sceneNumber = 0;
        cutscenes.SetActive(true);

        cutsceneList.Clear();

        foreach (Transform child in cutscenes.transform)
        {
            cutsceneList.Add(child.gameObject);
        }

        int start = 0;

        if (cutsceneNum == 1)
        {
            start = cutsceneList.IndexOf(firstGameplayScene) + 1;
        }
        else if (cutsceneNum == 2)
        {
            start = cutsceneList.IndexOf(secondGameplayScene) + 1;
        }
        else if (cutsceneNum == 4)
        {
            start = cutsceneList.IndexOf(thirdGameplayScene) + 1;
        }
        else if (cutsceneNum == 5)
        {
            start = cutsceneList.IndexOf(fourthGameplayScene) + 1;
        }
        else if (cutsceneNum == 7)
        {
            start = cutsceneList.IndexOf(fifthGameplayScene) + 1;
        }
        else if (cutsceneNum == 8)
        {
            start = cutsceneList.IndexOf(sixthGameplayScene) + 1;
        }
        else if (cutsceneNum == 10)
        {
            start = cutsceneList.IndexOf(seventhGameplayScene) + 1;
        }
        else if (cutsceneNum == 11)
        {
            start = cutsceneList.IndexOf(eighthGameplayScene) + 1;
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
        if (sceneNumber > 0 && !settingsMenu.activeSelf && !cutscene1End.activeSelf)
        {
            if (sceneNumber - 1 != cutsceneList.IndexOf(firstGameplayScene) && sceneNumber - 1 != cutsceneList.IndexOf(secondGameplayScene) && cutscenes == firstChapter)
            {
                sceneNumber--;
                ChangeSceneBack();
            }
            else if (sceneNumber - 1 != cutsceneList.IndexOf(thirdGameplayScene) && sceneNumber - 1 != cutsceneList.IndexOf(fourthGameplayScene) && cutscenes == secondChapter)
            {
                sceneNumber--;
                ChangeSceneBack();
            }
            else if (sceneNumber - 1 != cutsceneList.IndexOf(fifthGameplayScene) && sceneNumber - 1 != cutsceneList.IndexOf(sixthGameplayScene) && cutscenes == thirdChapter)
            {
                sceneNumber--;
                ChangeSceneBack();
            }
            else if (sceneNumber - 1 != cutsceneList.IndexOf(seventhGameplayScene) && sceneNumber - 1 != cutsceneList.IndexOf(eighthGameplayScene) && cutscenes == fourthChapter)
            {
                sceneNumber--;
                ChangeSceneBack();
            }
        }
    }
    public PlayerData createPlayerDataObject()
    {
        PlayerData data = new PlayerData();

        data.sceneNumber = cutsceneNum;

        return data;
    }

    private void LoadFromPlayerData(PlayerData tempData)
    {

        cutsceneNum = tempData.sceneNumber;
    }

    //This was originally private, I made it public so I could access it in other scripts. Was there a reason for making it private?
    //MARKER Object(Save Type) -> Json(String)
    public void SaveByJSON()
    {
        PlayerData playerData = createPlayerDataObject();

        string JsonString = JsonUtility.ToJson(playerData, true); //Convert PLAYERDATA Object into JSON();

        StreamWriter sw = new StreamWriter(Application.dataPath + "/JSONData.text");
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("==============SAVED================");
    }

    //This was originally private, I made it public so I could access it in other scripts. Was there a reason for making it private?
    //A: Mostly if we wanted another LoadByJSON script for a different save class. Ask me about it later FIXME: Delete if you understand
    public void LoadByJSON()
    {
        if (File.Exists(Application.dataPath + "/JSONData.text"))
        {
            //LOAD THE GAME
            StreamReader sr = new StreamReader(Application.dataPath + "/JSONData.text");

            string JsonString = sr.ReadToEnd();

            sr.Close();

            //Convert JSON to the Object(PlayerData)
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(JsonString);

            LoadFromPlayerData(playerData);

        }
        else
        {
            Debug.Log("NOT FOUND FILE");
        }
    }

    public void sceneLoader(string name)
    {
        SaveByJSON();
        SceneManager.LoadScene(name);
    }
}