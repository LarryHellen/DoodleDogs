using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GoToNewScene : MonoBehaviour
{
    public string scene = "";
    public bool reset;
    public int cutsceneNum;

    public void GoToScene()
    {
        LoadByJSON();
        if(reset){
            cutsceneNum = 0;
        }
        SaveByJSON();
        SceneManager.LoadScene(scene);
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
}
