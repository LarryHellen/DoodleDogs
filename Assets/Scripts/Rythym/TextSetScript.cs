using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetScript : MonoBehaviour
{
    public static int Score = 0;
    public TextMeshProUGUI DaScore;
    public int notesNeededForAWin;

    void FixedUpdate()
    {
        DaScore.SetText(Score.ToString());

        if (Score > notesNeededForAWin)
        {
            RhythmGameWinCondition();
        }
    }


    public void RhythmGameWinCondition()
    {
        //Win code here
        print("you win");
    }
}
