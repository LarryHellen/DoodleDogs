using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsRedo : MonoBehaviour
{
    public Slider slideBar;

    public List<GameObject> creditsText = new List<GameObject>();
    private List<RectTransform> creditsTextRt = new List<RectTransform>();
    private List<Vector2> creditsTextStartPos = new List<Vector2>();


    void Start()
    {   
        foreach (GameObject creditsText in creditsText)
        {
            creditsTextRt.Add(creditsText.GetComponent<RectTransform>());
        }

        foreach (RectTransform creditsTextRt in creditsTextRt)
        {
            creditsTextStartPos.Add(creditsTextRt.anchoredPosition);
        }
    }


    void Update()
    {
        for (int i = 0; i < creditsText.Count; i++)
        {
            creditsTextRt[i].anchoredPosition = new Vector2(creditsTextStartPos[i].x, creditsTextStartPos[i].y + slideBar.value * 1450);
        }
    }
}
