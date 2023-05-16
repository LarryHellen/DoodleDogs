using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public GameObject creditsText;
    public Slider slideBar;
    // Start is called before the first frame update
    public void valueChanged() {
        creditsText.transform.position = new Vector3(creditsText.transform.position.x, 100 + slideBar.value * 1400, creditsText.transform.position.z);
    }

}
