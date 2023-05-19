using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class JsonDataManipulation
{
    public int currentChapter;
    public List<bool> chaptersUnlocked;
    public int currentCutscene;
    public List<List<bool>> tutorials = new List<List<bool>>();


    private PlayerDataRefactored CreatePlayerDataRefactoredObject()
    {
        PlayerDataRefactored data = new PlayerDataRefactored();

        data.currentChapter = currentChapter;
        data.chaptersUnlocked = chaptersUnlocked;
        data.currentCutscene = currentCutscene;
        data.tutorials = tutorials;

        return data;
    }

    private void LoadFromPlayerDataRefactored(PlayerDataRefactored tempData)
    {
        currentChapter = tempData.currentChapter;
        chaptersUnlocked = tempData.chaptersUnlocked;
        currentCutscene = tempData.currentCutscene;
        tutorials = tempData.tutorials;

        if (tempData.chaptersUnlocked.Count == 0)
        {
            for (int i = 0; i < 5; i++) {chaptersUnlocked.Add(false);}
        }


        if (tempData.tutorials.Count == 0)
        {
            for (int i = 0; i < 5; i++) { tutorials.Add(new List<bool>() { false, false }); }
        }
    }


    public void SaveByJSON()
    {
        PlayerDataRefactored playerDataRefactored = CreatePlayerDataRefactoredObject();

        string JsonString = JsonUtility.ToJson(playerDataRefactored, true); //Convert PLAYERDATAREFACTORED Object into JSON();

        if(!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"))){
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath+"/Saves/"));
        }
        StreamWriter sw = new StreamWriter(Application.persistentDataPath+"/Saves/JSONData.text");
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("==============SAVED================");
    }


    public void LoadByJSON(List<GameObject> chapterGameObjects=null)
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
            PlayerDataRefactored data = JsonUtility.FromJson<PlayerDataRefactored>(JsonString);

            LoadFromPlayerDataRefactored(data);
        }
        else
        {
            Debug.Log("NOT FOUND FILE");
        }
    }
}