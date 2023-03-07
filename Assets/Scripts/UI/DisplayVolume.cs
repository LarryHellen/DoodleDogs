using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.UI;
using UnityEngine.UI;
using TMPro;
using System;

public class DisplayVolume : MonoBehaviour
{
    public TextMeshProUGUI a, d;
    public Slider b, c;
    // Start is called before the first frame update
    public void Start()
    {
        a.text = "Music Volume: " + Math.Round(b.value * 100) + "%";
        d.text = "Sound Effect Volume: " + Math.Round(c.value * 100) + "%";
    }
    public void valueChanged()
    {
        a.text = "Music Volume: " + Math.Round(b.value * 100) + "%";
        d.text = "Sound Effect Volume: " + Math.Round(c.value * 100) + "%";
    }
}
