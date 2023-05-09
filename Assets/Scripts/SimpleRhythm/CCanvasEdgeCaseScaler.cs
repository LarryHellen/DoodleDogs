using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CCanvasEdgeCaseScaler : MonoBehaviour
{
    private Canvas canvas;
    public Canvas otherCanvas;
    private RectTransform canvasRt;
    private ContinousNoteSpawning nSS;


    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasRt = otherCanvas.GetComponent<RectTransform>();
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();


        float canvasWidth = canvasRt.sizeDelta.x;
        float canvasHeight = canvasRt.sizeDelta.y;


        if (canvasHeight == 1920 && canvasWidth == 1080)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1080, 1920);
            canvas.scaleFactor = .83f;
            nSS.notePrefab.GetComponent<CBasicNoteObject>().heightRemovalConst = 500;
        }
        else if (canvasWidth * 2 > canvasHeight)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
    }
}
