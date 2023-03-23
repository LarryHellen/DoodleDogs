using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainNoteScript : MonoBehaviour
{
    public float BPM;
    public string NOTE_TYPE;

    public int HoldTimeNeeded;

    private int HoldTime = 0;

    private bool BeingClicked;

    private float Timer, LastTime = 0;

    public float noteGoneAtThisValue;

    public float noteOpacityBeGone;

    public Image spriteRenderer;


    public float initialOpacity;

    public float endOpacity;

    private float currentOpacity;

    //private bool becomeVisible = false;

    public TextSetScript tss;


    void OnMouseDown()
    {
        BeingClicked = true;


        if (NOTE_TYPE == "TAP") { 
            Destroy(gameObject);
            tss.Score++;
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

        currentOpacity = initialOpacity;

        spriteRenderer.color = new Color(1f, 1f, 1f, currentOpacity);

        tss = FindObjectOfType<TextSetScript>();
    }


    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (NOTE_TYPE == "HOLD" && BeingClicked == true)
            {
                HoldTime++;
            }
        }

        if (Timer - LastTime > 0.001)
        {
            transform.position = transform.position - new Vector3(0, BPM, 0);
            LastTime = Timer;
            
        }

        if (transform.position[1] < noteOpacityBeGone)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, endOpacity);
            //becomeVisible = true;
        }


        if (transform.position[1] < noteGoneAtThisValue)
        {
                tss.Score = 0;
                tss.RhythmGameLoseCondition();
                Destroy(gameObject);
        }
        else if (HoldTime >= HoldTimeNeeded && NOTE_TYPE == "HOLD")
        {
                Destroy(gameObject);
                tss.Score++;
        }

    }
}
