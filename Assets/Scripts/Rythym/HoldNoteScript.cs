using UnityEngine;
using System.Collections;

public class HoldNoteScript : MonoBehaviour
{
    public int lengthOfHoldNote;
    public int holdTimeConstant; //Need to test to find a good number
    public float scalingConstant; //Probably just the size of normal notes
    public int holdTimeRequired;
    private Vector3 scale;
    private RectTransform rt;

    void Start()
    {
        

        
        
        //scale.y = 10;
        //rt.localScale = scale;
        

        //scale = rt.localScale;
    }

    public void ScalingTime()
    {

        //IMPLEMENT THE SCRIPT BELOW USING THE FUNCTIONALITY OF THE CODE WITHIN THE START FUNCTION




        rt = this.GetComponent<RectTransform>();
        var scale = rt.localScale;
        scale.y = (float) lengthOfHoldNote * scalingConstant;
        rt.localScale = scale;


        this.GetComponent<MainNoteScript>().HoldTimeNeeded = holdTimeConstant * lengthOfHoldNote;

        holdTimeRequired = holdTimeConstant * lengthOfHoldNote;

    }
}
