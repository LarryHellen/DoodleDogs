using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerDataRefactored
{
    public int currentChapter = -1;
    public List<bool> chaptersUnlocked; //= new List<bool>();

    public int currentCutscene;
    public List<List<bool>> tutorials; //= new List<List<bool>>();
}