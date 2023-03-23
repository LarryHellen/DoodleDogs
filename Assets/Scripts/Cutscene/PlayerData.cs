using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class PlayerData
{
    public int sceneNumber;
    public bool chapter2Unlocked;
    public bool chapter3Unlocked;
    public bool chapter4Unlocked;
}
