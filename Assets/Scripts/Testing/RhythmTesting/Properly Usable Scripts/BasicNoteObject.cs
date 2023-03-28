using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BasicNoteObject : MonoBehaviour
{
    [Header("Manual Variables")]
    public float notePercentLengthOfScreen;
    private float distanceToMoveIn1Interval;
    private bool isHoldNote = false;


    // OPACITY //
    public float initialOpacity;
    public float endOpacity;
    private Image image;

    private NoteSpawningSystem nSS;
    private HoldNoteObject hNO;
    private RectTransform rt;

    
    // HOLD NOTE //
    private float startTime, endTime;
    private List<Coroutine> coroutines = new List<Coroutine>();
    

    void Start()
    {
        image = GetComponent<Image>();
        rt = GetComponent<RectTransform>();

        nSS = GameObject.Find("NoteSpawnManager").GetComponent<NoteSpawningSystem>();
        hNO = GetComponent<HoldNoteObject>();

        if (hNO != null)
        {
            isHoldNote = true;
        }


        //Set all variables for proper size (change height size only if HoldNoteObject script not attached)
        //Remember to set proper widths


        //Width Setting
        var sizeDelta = rt.sizeDelta;
        sizeDelta.x = (nSS.screenWidth - (nSS.horizontalSpaceBetweenNotes * (nSS.columns - 1))) / nSS.columns;


        //Height Setting
        if (!isHoldNote)
        {
            sizeDelta.y = nSS.screenHeight * notePercentLengthOfScreen;
        }

        rt.sizeDelta = sizeDelta;

        //NOTE - MOVEMENT

        SetDistanceToMoveIn1Interval();

        //Start Note Movement
        StartCoroutine(LerpMovement());

        //END OF "NOTE - MOVEMENT"
    }


    void Update()
    {
        NoteDestruction();
    }


    void SetDistanceToMoveIn1Interval()
    {
        //DistanceToMoveIn1Interval = Distance needed to move every interval to make it to the On Beat Line on time (Bottom Edge of Note)
        if (isHoldNote)
        {
            distanceToMoveIn1Interval = ((rt.localPosition.y - ((nSS.screenHeightPercentForNoteToLandOnBeat * nSS.screenHeight) - (nSS.screenHeight / 2))) - (nSS.screenHeight * hNO.holdNoteLength * hNO.holdNoteLengthConstant / 2)) / nSS.timeToOnBeatLocation; //timeToOnBeatLocation is in units of time of size "IntervalLength"
        }
        else
        {
            distanceToMoveIn1Interval = ((rt.localPosition.y - ((nSS.screenHeightPercentForNoteToLandOnBeat * nSS.screenHeight) - (nSS.screenHeight / 2))) - (nSS.screenHeight * notePercentLengthOfScreen / 2)) / nSS.timeToOnBeatLocation; //timeToOnBeatLocation is in units of time of size "IntervalLength"
        }
    }


    void NoteDestruction()
    {
        if (isHoldNote)
        {
            if (rt.localPosition.y <= -nSS.screenHeight / 2 - (nSS.screenHeight * hNO.holdNoteLength * hNO.holdNoteLengthConstant / 2))
            {
                nSS.playerCombo = 0;
                Destroy(gameObject);
            }
        }
        else
        {
            if (rt.localPosition.y <= -nSS.screenHeight / 2 - (nSS.screenHeight * notePercentLengthOfScreen / 2))
            {
                nSS.playerCombo = 0;
                Destroy(gameObject);
            }
        }
    }


    public void OnPointerDownOnNote()
    {
        print("Pointer Down On Note");

        if (isHoldNote)
        {
            startTime = Time.time;
            coroutines.Add(StartCoroutine(HoldNoteHideOverTime()));
        }
        else
        {
            nSS.playerCombo += 1;
            Destroy(gameObject);
        }
    }


    public void OnPointerUpOnNote()
    {
        print("Pointer Up On Note");

        if (isHoldNote) //Reset Note Opacity and Fading Coroutines + Check if Hold Note should be destroyed
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, initialOpacity);

            foreach (Coroutine coroutine in coroutines){ StopCoroutine(coroutine); }
            coroutines.Clear();

            endTime = Time.time;

            hNO.timeBeenTapped = endTime - startTime;

            print("Time Been Tapped: " + hNO.timeBeenTapped);

            if (hNO.timeBeenTapped > hNO.holdNoteHoldTime - nSS.holdNoteHoldTimeTolerance*hNO.holdNoteHoldTime) //If timeBeenTapped is within tolerance of holdNoteHoldTime (holdNoteHoldTimeTolerance is a percentage of the holdNoteHoldTime)
            {
                nSS.playerCombo += 1;
                Destroy(gameObject);
            }

            hNO.timeBeenTapped = 0;
        }
    }


    IEnumerator HoldNoteHideOverTime()
    {
        float timeElapsed = 0;
        float lerpValue = 0;

        while (timeElapsed < hNO.holdNoteHoldTime)
        {
            lerpValue = Mathf.Lerp(initialOpacity, endOpacity, timeElapsed / hNO.holdNoteHoldTime);
            timeElapsed += Time.deltaTime;

            image.color = new Color(image.color.r, image.color.g, image.color.b, lerpValue);

            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, endOpacity);
    }


    IEnumerator LerpMovement()
    {
        while (true)
        {
            float startPos = rt.localPosition.y;
            float endPos = rt.localPosition.y - distanceToMoveIn1Interval;
            float timeElapsed = 0;
            float lerpValue = 0;

            while (timeElapsed < nSS.intervalLength)
            {
                lerpValue = Mathf.Lerp(startPos, endPos, timeElapsed / nSS.intervalLength);
                timeElapsed += Time.deltaTime;

                rt.localPosition = new Vector3(rt.localPosition.x, lerpValue, rt.localPosition.z);

                yield return null;
            }

            rt.localPosition = new Vector3(rt.localPosition.x, endPos, rt.localPosition.z);
        }
    } 
}
