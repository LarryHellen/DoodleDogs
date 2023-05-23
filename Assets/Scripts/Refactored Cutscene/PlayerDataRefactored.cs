using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerDataRefactored
{
    [SerializeField] public int currentChapter = -1;
    [SerializeField] public List<bool> chaptersUnlocked;

    [SerializeField] public int currentCutscene;
    [SerializeField] public List<bool> tutorials;
}