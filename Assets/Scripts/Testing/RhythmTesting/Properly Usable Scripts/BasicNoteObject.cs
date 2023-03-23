using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasicNoteObject : MonoBehaviour
{
    //Init ScreenWidth ("Screen.width") (Get from main spanwer script)
    //Init ScreenHeight (Get from main spanwer script)


    //Init Columns (Get from main spanwer script) 
    //Init NotePercentLengthOfScreen
    //Init IntervalLength (Get from main spanwer script)
    //Init HorizontalSpaceBetweenNotes (Get from main spanwer script)
    //Init TimeElapsedSinceLastInterval
    //Init ScreenHeightPercentForNoteHide (Get from main spanwer script)
    //Init ScreenHeightPercentForNoteToLandOnBeat (Get from main spanwer script)
    //Init TimeToOnBeatLocation (Get from main spanwer script)
    //Init DistanceToMoveIn1Interval
    //Init TimeBeenTapped


    // OPACITY //
    //Init ScreenHeightPercentForNoteShow (Get from main spanwer script)
    //Init InitialOpacity
    //Init EndOpacity
    //Init SpriteRenderer


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


    void OnMouseDown() //NOT WORKING
    {
        print("o");

        if (isHoldNote)
        {
            hNO.timeBeenTapped += Time.deltaTime;

            if (hNO.timeBeenTapped > hNO.holdNoteHoldTime)
            {
                nSS.playerCombo += 1;
                Destroy(gameObject);
            }
        }
        else
        {
            nSS.playerCombo += 1;
            Destroy(gameObject);
        }
    }


    void OnMouseUp() //NOT WORKING
    {
        if (isHoldNote)
        {
            hNO.timeBeenTapped = 0;
        }
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