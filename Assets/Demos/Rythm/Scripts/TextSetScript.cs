using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetScript : MonoBehaviour
{
    public static int Score = 0;
    public TextMeshProUGUI DaScore;

    void FixedUpdate()
    {
        DaScore.SetText(Score.ToString());
    }
}
