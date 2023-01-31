using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetScript : MonoBehaviour
{
    public static int Score = 0;
    public TextMeshProUGUI DaScore;

    public bool Invicibility;
    public GameObject LoseScreen;
    public GameObject WinScreen;
    private FourAudioNoteSpawner fans;

    private void Start()
    {
        fans = FindObjectOfType<FourAudioNoteSpawner>();
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    void FixedUpdate()
    {
        DaScore.SetText(Score.ToString());
    }




    public void RhythmGameWinCondition()
    {
        //Win code here
        Time.timeScale = 0;
        print("you win");
        WinScreen.SetActive(true);
    }


    public void RhythmGameLoseCondition()
    {
        if (!Invicibility)
        {
            //Lose code here
            Time.timeScale = 0;
            print("you lose");
            LoseScreen.SetActive(true);
        }
    }
}
