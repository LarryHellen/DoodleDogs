using UnityEngine;
using System.Collections;

public class HoldNoteScript : MonoBehaviour
{
    public int lengthOfHoldNote;


    void Start()
    {
        Vector2 scaledSize = transform.localScale;
        scaledSize[1] = (float) lengthOfHoldNote;

        this.transform.localScale = scaledSize;
    }
}
