using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainNoteScript : MonoBehaviour
{


    public float BPM;
    public TextMeshPro DaScore;

    private int PlayerScore = 0;

    void OnMouseDown()
    {
        PlayerScore++;
        Destroy(gameObject);
    }


    void Start()
    {
        BPM = BPM / 1000;
    }

    // Update is called once per frame
    void Update()
    {

        DaScore.SetText(PlayerScore.ToString());

        if (gameObject.tag != "Parent")
        {
            transform.position = transform.position - new Vector3(0, BPM, 0);
        }
        if (transform.position[1] < -6)
        {
            PlayerScore = 0;
            Destroy(gameObject);
        }
       
    }
}
