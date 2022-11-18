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

    void OnMouseDown()
    {
        
        if (TicTacToeRunner.runGame && ActivateSettings.gameRunning)
        {
            if (type == 0)
            {
                if (TicTacToeRunner.turnCounter % 2 == 0)
                {
                    type = 1;
                    TicTacToeRunner.turnCounter++;
                    HasChanged = false;
                    FindObjectOfType<AudioManager>().Play("PlaceBall");
                    //Debug.Log(TicTacToeRunner.turnCounter);
                    StartCoroutine(pauseGame(.5f));

                }
                else if (TicTacToeRunner.turnCounter % 2 == 1 && TicTacToeRunner.twoPlayer == true)
                {
                    type = 2;
                    TicTacToeRunner.turnCounter++;
                    TicTacToeRunner.currentlyRotating = true;
                    HasChanged = false;
                    //Debug.Log(TicTacToeRunner.turnCounter);
                }
            }
        }
    }

    void Start()
    {
        //tttr = FindObjectOfType<TicTacToeRunner>();
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

            

        if (type == 0)
        {
            currentTileSprite.sprite = square;
        }

        if (HasChanged == false)
        {

            if (type == 1)
            {
                currentTileSprite.sprite = x;
            }
            else if (type == 2)
            {
                currentTileSprite.sprite = o;
            }

            HasChanged = true;
        }
    }

    private IEnumerator pauseGame(float secondsToPause){
        TicTacToeRunner.runGame = false;
        yield return new WaitForSeconds(secondsToPause);
        TicTacToeRunner.runGame = true;
    }
}
