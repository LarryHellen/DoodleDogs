using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DragScroll : MonoBehaviour
{
    //private CutsceneManager CutsceneManager;
    public GameObject background;
    public GameObject settingsMenu;
    public GameObject c2d, c3d;//, c4d;
    public int p;
    public Vector3 startPoint = new Vector3(0, 0, 0);
    public int b = 0, c = 0;
    public  int cutsceneNum;

    private bool chapter2Unlocked;
    private bool chapter3Unlocked;
    private bool chapter4Unlocked;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (!settingsMenu.activeSelf)
        {
            startPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Debug.Log("e");
        }
    }
    private void OnMouseUp()
    {
        startPoint = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (startPoint != new Vector3(0, 0, 0))
        {
            background.transform.position += new Vector3(0, (Input.mousePosition.y - startPoint.y)/2, 0);
            startPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if (background.transform.position.y < b)
        {
            background.transform.position = new Vector3(background.transform.position.x, b, background.transform.position.z);
        }
        if (background.transform.position.y > c)
        {
            background.transform.position = new Vector3(background.transform.position.x, c, background.transform.position.z);
        }
    }
    void Start()
    {
        print(cutsceneNum);
        //cutsceneNum = 0;
        LoadByJSON();
        if (cutsceneNum > 2)
        {
            c2d.SetActive(false);
        }
        if (cutsceneNum > 5)
        {
            c3d.SetActive(false);
        }
        if (Time.deltaTime > 1 || Time.deltaTime < 1.2)
        {
            print(cutsceneNum);
        }

    }

    public PlayerData createPlayerDataObject()
    {
        PlayerData data = new PlayerData();

        data.sceneNumber = cutsceneNum;
        data.chapter2Unlocked = this.chapter2Unlocked;
        data.chapter3Unlocked = this.chapter3Unlocked;
        data.chapter4Unlocked = this.chapter4Unlocked;
        //data.lSC = this.lSC;

        return data;
    }

    private void LoadFromPlayerData(PlayerData tempData)
    {

        cutsceneNum = tempData.sceneNumber;
        this.chapter2Unlocked = tempData.chapter2Unlocked;
        this.chapter3Unlocked = tempData.chapter3Unlocked;
        this.chapter4Unlocked = tempData.chapter4Unlocked;
        //lSC = tempData.lSC;
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
