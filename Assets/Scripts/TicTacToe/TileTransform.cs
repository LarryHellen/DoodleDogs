using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileTransform : MonoBehaviour
{
    public RectTransform myRectTransform;


    public void setRectO (){
        myRectTransform.offsetMin = new Vector2(191,664.5f);
        myRectTransform.offsetMax = new Vector2(-191,-664.5f);
        //left = 191
        //right = 191
        //top = 664.5
        //bottom = 664.5
    }

    public void setRectX (){
        myRectTransform.offsetMin = new Vector2(156,539);
        myRectTransform.offsetMax = new Vector2(-156,-539);
        //left = 156
        //right = 156
        //top = 539
        //bottom = 539
    }

}
