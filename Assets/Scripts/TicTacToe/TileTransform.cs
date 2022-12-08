using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileTransform : MonoBehaviour
{
    public RectTransform myRectTransform;


    public void setRectO (){
        Vector3 previousPos = transform.position;
        myRectTransform.offsetMin = new Vector2(191,664.5f); //Left, Top
        myRectTransform.offsetMax = new Vector2(-191,-664.5f); //Right, Bottom
        transform.position = previousPos;
        //left = 191
        //right = 191
        //top = 664.5
        //bottom = 664.5
    }

    public void setRectX (){
        Vector3 previousPos = transform.position;
        myRectTransform.offsetMin = new Vector2(156,539); //Left, Top
        myRectTransform.offsetMax = new Vector2(-156,-539); //Right, Bottom
        transform.position = previousPos;
        //left = 156
        //right = 156
        //top = 539
        //bottom = 539
    }

}
