using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSerial : MonoBehaviour
{
    [SerializeField] private PlayerData _PlayerData = new PlayerData();

    public void SaveIntoJson()
    {
        string playerData = JsonUtility.ToJson(_PlayerData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", playerData);
    }
}
