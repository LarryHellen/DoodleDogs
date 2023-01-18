using UnityEngine;
using System.Collections;

public class HoldNoteScript : MonoBehaviour
{
    public int lengthOfHoldNote;
    public int holdTimeConstant; //Need to test to find a good number
    public float scalingConstant; //Probably just the size of normal notes
    public int holdTimeRequired;

    RectTransform rt;

    void Start()
    {
        rt = this.GetComponent<RectTransform>();

        var scale = rt.sizeDelta;
        scale.y = 10f;
        rt.sizeDelta = scale;

    }

    public void ScalingTime()
    {

        //IMPLEMENT THE SCRIPT BELOW USING THE FUNCTIONALITY OF THE CODE WITHIN THE START FUNCTION


        /*
         * 
        Vector3 scaledSize = rt.localScale;

        print(scaledSize);

        scaledSize[1] = (float)lengthOfHoldNote * scalingConstant;

        print(scaledSize);

        rt.localScale = scaledSize;

        this.GetComponent<MainNoteScript>().HoldTimeNeeded = holdTimeConstant * lengthOfHoldNote;

        holdTimeRequired = holdTimeConstant * lengthOfHoldNote;

        */
    }
}
