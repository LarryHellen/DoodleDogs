using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainNoteScript : MonoBehaviour
{

    public float BPM;

    void OnMouseDown()
    {
        Destroy(gameObject);
        TextSetScript.Score++;
    }

    void Start()
    {
        BPM = BPM / 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "Parent")
        {
            transform.position = transform.position - new Vector3(0, BPM, 0);
        }
        if (transform.position[1] < -6)
        {
            TextSetScript.Score = 0;
            Destroy(gameObject);
        }
       
    }
}
