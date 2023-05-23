using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class JsonDataManipulation
{
    [SerializeField] public int currentChapter;
    [SerializeField] public List<bool> chaptersUnlocked;
    [SerializeField] public int currentCutscene;
    [SerializeField] public List<bool> tutorials;


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


    public void LoadDefaultValuesFromPlayerDataRefactored()
    {
        chaptersUnlocked = new List<bool>();
        for (int i = 0; i < 5; i++) { chaptersUnlocked.Add(false); }
        

        tutorials = new List<bool>();
        for (int i = 0; i < 10; i++) { tutorials.Add(false); }

        
    }
}