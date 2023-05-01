using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfIveBeenClicked : MonoBehaviour
{
    public ActivateSettings ActivateSettings;

    public int type = 0;
    public bool HasChanged = true;
    public bool rotationBool = false;
    public Vector3 tmpPos;


    public Image currentTileSprite;
    public Sprite x;
    public Sprite o;
    public Sprite square;


    private float totalDistance;
    public float timeToTake;
    private bool smoothMent = false;
    private float timeFraction;
    private float distanceBetween;
    private TicTacToeRunner tttr;

    private Vector2 X_size;
    private Vector2 O_size;
    private TileTransform myTileTransform;
    public bool usingTileTransform;

    void OnMouseDown()
    {
        
        if (tttr.runGame && ActivateSettings.gameRunning && tttr.started == true)
        {
            if (type == 0)
            {
                if (tttr.turnCounter % 2 == 0)
                {
                    type = 1;
                    tttr.turnCounter++;
                    HasChanged = false;
                    FindObjectOfType<AudioManager>().Play("PlaceBall");
                    //Debug.Log(tttr.turnCounter);
                    if(tttr.tutorialEnabled == true){
                        tttr.tutorialObject.placed = true;
                        tttr.tutorialObject.Next();
                    }
                    StartCoroutine(pauseGame(.5f));

                }
                else if (tttr.turnCounter % 2 == 1 && tttr.twoPlayer == true)
                {
                    type = 2;
                    tttr.turnCounter++;
                    tttr.currentlyRotating = true;
                    HasChanged = false;
                    //Debug.Log(tttr.turnCounter);
                }
            }
        }
    }

    void Start()
    {
        myTileTransform = GetComponent<TileTransform>();
        tttr = FindObjectOfType<TicTacToeRunner>();
        ActivateSettings = tttr.activateSettings;
        X_size = x.rect.size;
        O_size = o.rect.size;
    }


    void Update()
    {

        if (rotationBool == true) {
            //Debug.Log("Once.");

            distanceBetween = Vector3.Distance(transform.position, tmpPos);

            totalDistance = 0f;

            rotationBool = false;
            smoothMent = true;
        }

        if (smoothMent == true)
        {
            
            timeFraction = Time.deltaTime / timeToTake;

            // Debug.Log("Time to Take = " + timeToTake + " / Delta Time = " + Time.deltaTime);

            //Debug.Log("Time Fraction = " + timeFraction);

            totalDistance += timeFraction;

            //Debug.Log("Total Distance Traveled = " + totalDistance);

            transform.position = Vector3.Lerp(transform.position, tmpPos, totalDistance);

            if (totalDistance > 1f)
            {
                smoothMent = false;
            }
            
        }

            

        // if (type == 0)
        // {
        //     currentTileSprite.sprite = square;
        // }

        if (HasChanged == false)
        {
            //Get Image Size
            //Change rect transform

            var rectTransform = GetComponent<RectTransform>();

            if (type == 1)
            {
                if(!usingTileTransform){
                    rectTransform.sizeDelta = new Vector2(X_size[0], X_size[1]);
                    //rectTransform.localScale = new Vector3(0.1f, 0.07f, 0.1f);
                } else{
                    myTileTransform.setRectX();
                }


                currentTileSprite.sprite = x;
            }
            else if (type == 2)
            {
                if(!usingTileTransform){
                    rectTransform.sizeDelta = new Vector2(O_size[0], O_size[1]);
                    //rectTransform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                } else{
                    myTileTransform.setRectO(); 
                }

                currentTileSprite.sprite = o;
            }

            HasChanged = true;
        }

        if(type != 0){
            Color tempColor = currentTileSprite.color;
            tempColor.a = 255f;
            currentTileSprite.color = tempColor;
        }
    }

    private IEnumerator pauseGame(float secondsToPause){
        tttr.runGame = false;
        yield return new WaitForSeconds(secondsToPause);
        tttr.runGame = true;
    }

    public void ResetSprite()
    {
        var rectTransform = GetComponent<RectTransform>();

        Color tempColor = currentTileSprite.color;
        tempColor.a = 0f;
        currentTileSprite.color = tempColor;

        currentTileSprite.sprite = square;
        rectTransform.sizeDelta = new Vector2(500, 500);
    }
}
