using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    public GameObject slide1, slide2;


    public void advance()
    {
        if (slide1.activeSelf)
        {
            slide1.SetActive(false);
            slide2.SetActive(true);
        }
    }
}
