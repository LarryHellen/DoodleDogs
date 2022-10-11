using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    public int Seconds;
    public int DEGREES_TO_ROTATE = 360;
    private float DegreesRotated = 0;
    private float DeltaTimeVsTotalTime;

    void Start()
    {
        //Nothing lol
    }

    void Update()
    {

        //Rotate 360 degrees in "Seconds" seconds

        DeltaTimeVsTotalTime = Time.deltaTime / Seconds;

        DegreesRotated += DEGREES_TO_ROTATE * DeltaTimeVsTotalTime;

        if (DegreesRotated <= DEGREES_TO_ROTATE)
        {
            this.transform.Rotate(0.0f, 0.0f, DEGREES_TO_ROTATE * DeltaTimeVsTotalTime, Space.Self);
        }
        

    }
}
