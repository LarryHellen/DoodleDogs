using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerDataRefactored
{
    [SerializeField] public int currentChapter;
    [SerializeField] public List<bool> chaptersUnlocked;

    [SerializeField] public int currentCutscene;
    [SerializeField] public List<bool> tutorials;
}