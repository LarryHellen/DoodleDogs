using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CCanvasEdgeCaseScaler : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform canvasRt;


    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasRt = GetComponent<RectTransform>();

        float canvasWidth = canvasRt.sizeDelta.x;
        float canvasHeight = canvasRt.sizeDelta.y;

        if (canvasWidth*2 > canvasHeight)
        {
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            //canvasScaler.referenceResolution = new Vector2(1080, 1920);
        }
    }
}
