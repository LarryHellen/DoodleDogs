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
    private float timeElapsedSinceLastInterval;
    private float distanceToMoveIn1Interval;
    private float timeBeenTapped;
    private bool isHoldNote = false;


    // OPACITY //
    public float initialOpacity;
    public float endOpacity;
    private Image image;

    private NoteSpawningSystem nSS;
    private HoldNoteObject holdNoteObject;
    private RectTransform rt;


    void Start()
    {
        image = GetComponent<Image>();
        rt = GetComponent<RectTransform>();

        nSS = GameObject.Find("NoteSpawnManager").GetComponent<NoteSpawningSystem>();
        holdNoteObject = GetComponent<HoldNoteObject>();

        if (holdNoteObject != null)
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


        //DistanceToMoveIn1Interval = (CurrentHeight - ScreenHeight*ScreenHeightPercentForNoteToLandOnBeat)/TimeToOnBeatLocation
        distanceToMoveIn1Interval = (rt.localPosition.y - ((nSS.screenHeightPercentForNoteToLandOnBeat * nSS.screenHeight) - (nSS.screenHeight / 2))) / nSS.timeToOnBeatLocation; //timeToOnBeatLocation is in units of time of size "IntervalLength"


        //+ (nSS.screenHeight * notePercentLengthOfScreen/2))
        //Something like this for note to get to the top of the line of time





        //print(distanceToMoveIn1Interval);

        //Start Note Movement
        StartCoroutine(LerpMovement());
        //print(rt.localPosition);

        //END OF "NOTE - MOVEMENT"
    }

    void Update()
    {
        //NOTE - WINNING AND LOSING


        //if HoldNoteObject script is attached and object being clicked, increase a TimeBeenTapped variable by deltaTime
        //if TimeBeenTapped is greater than this objects HoldTime, [Destroy this object and give the player score] <- Seperate function?

        //if HoldNoteObject script is attached and object not being clicked
        //Set TimeBeenTapped to 0


        //else if (HoldNoteObject script not attached and object being clicked), [Destroy this object and give the player score] <- Seperate function?


        //if object has passed ScreenHeightForNoteHide, [Destroy this object and set the player's score to 0]


        //END OF "NOTE - WINNING AND LOSING"
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