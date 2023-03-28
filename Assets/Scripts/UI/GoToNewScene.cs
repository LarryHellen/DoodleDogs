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
    private int cutsceneNum;
    public bool inCutscenes;
    //public CutsceneManager cm;

    private bool chapter2Unlocked;
    private bool chapter3Unlocked;
    private bool chapter4Unlocked;

    public void GoToScene()
    {
        if(!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"))){
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"));
        }
        if (File.Exists(Application.persistentDataPath+"/Saves/JSONData.text")){
            LoadByJSON();
        }
        if(reset){
            cutsceneNum = 0;
        }
        if (inCutscenes || reset)
        {
            SaveByJSON();
        }
        SceneManager.LoadScene(scene);
    }


    public PlayerData createPlayerDataObject()
    {
        PlayerData data = new PlayerData();

        data.sceneNumber = cutsceneNum;
        data.chapter2Unlocked = this.chapter2Unlocked;
        data.chapter3Unlocked = this.chapter3Unlocked;
        data.chapter4Unlocked = this.chapter4Unlocked;


        return data;
    }

    private void LoadFromPlayerData(PlayerData tempData)
    {
        cutsceneNum = tempData.sceneNumber;
        this.chapter2Unlocked = tempData.chapter2Unlocked;
        this.chapter3Unlocked = tempData.chapter3Unlocked;
        this.chapter4Unlocked = tempData.chapter4Unlocked;
    }

    //This was originally private, I made it public so I could access it in other scripts. Was there a reason for making it private?
    //MARKER Object(Save Type) -> Json(String)
    public void SaveByJSON()
    {
        PlayerData playerData = createPlayerDataObject();

        string JsonString = JsonUtility.ToJson(playerData, true); //Convert PLAYERDATA Object into JSON();

        if(!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"))){
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"));
        }
        StreamWriter sw = new StreamWriter(Application.persistentDataPath+"/Saves/JSONData.text");
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("==============SAVED================");
    }

    //This was originally private, I made it public so I could access it in other scripts. Was there a reason for making it private?
    //A: Mostly if we wanted another LoadByJSON script for a different save class. Ask me about it later FIXME: Delete if you understand
    public void LoadByJSON()
    {
        if(!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"))){
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"));
        }
        if (File.Exists(Application.persistentDataPath+"/Saves/JSONData.text"))
        {
            //LOAD THE GAME
            StreamReader sr = new StreamReader(Application.persistentDataPath+"/Saves/JSONData.text");

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
