using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManagement : MonoBehaviour
{
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    public Slider slide;

    public void SliderValueChanged()
    {
        if (slide.value >= 0.1)
        {
            star2.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        }
        else
        {
            star2.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
        }
        if (slide.value >= 0.366)
        {
            star3.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        }
        else
        {
            star3.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
        }
        if (slide.value >= 0.633)
        {
            star4.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        }
        else
        {
            star4.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
        }
        if (slide.value >= 0.9)
        {
            star5.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        }
        else
        {
            star5.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
        }
    }
}
