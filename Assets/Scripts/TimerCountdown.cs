using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    public int Seconds;
    public int DEGREES_TO_ROTATE = 360;
    private float DegreesRotated = 0;
    private float DeltaTimeVsTotalTime;
    private Board board;
    public bool movingAllowed;

    void Start()
    {
        movingAllowed = true;
        board = FindObjectOfType<Board>();
    }

    public void Reset(){
        movingAllowed = true;
        DegreesRotated = 0;
    }

    void Update()
    {
        if(movingAllowed){
            //Rotate 360 degrees in "Seconds" seconds

            DeltaTimeVsTotalTime = Time.deltaTime / Seconds;

            DegreesRotated += DEGREES_TO_ROTATE * DeltaTimeVsTotalTime;

            if (DegreesRotated <= DEGREES_TO_ROTATE)
            {
                this.transform.Rotate(0.0f, 0.0f, DEGREES_TO_ROTATE * DeltaTimeVsTotalTime, Space.Self);
            } else{
                movingAllowed = false;
                board.endGame();
            }
        }
        

    }
}
