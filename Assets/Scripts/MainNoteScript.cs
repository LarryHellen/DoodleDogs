using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainNoteScript : MonoBehaviour
{

    public float BPM;
    public string NOTE_TYPE;

    public int HoldTimeNeeded;

    private int HoldTime = 0;

    private bool BeingClicked;

    void OnMouseDown()
    {
        BeingClicked = true;

        if (NOTE_TYPE == "TAP") { 
            Destroy(gameObject);
            TextSetScript.Score++;
        }

    }

    void OnMouseUp()
    {
        BeingClicked = false;

        HoldTime = 0;
    }

    void Start()
    {
        BPM = BPM / 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (NOTE_TYPE == "HOLD" && BeingClicked == true)
            {
                HoldTime++;
            }
        }

        transform.position = transform.position - new Vector3(0, BPM, 0);

        if (transform.position[1] < -5.5)
        {
            TextSetScript.Score = 0;
            Destroy(gameObject);
        }
        else if (HoldTimeNeeded <= HoldTime)
        {
            Destroy(gameObject);
            TextSetScript.Score++;
        }
    }
}
