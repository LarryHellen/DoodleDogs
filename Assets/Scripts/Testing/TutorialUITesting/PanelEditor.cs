using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PanelEditor : MonoBehaviour
{

    public GameObject[] panelList;
    public int panelIndex = 0;
    private List<GameObject> allCurrentPanels;
    public Canvas canvas;
    private float canvasHeight;
    private float canvasWidth;
    public GameObject defaultPanel;
    public float panelOpacity;


    void Start()
    {
        panelList = GameObject.FindGameObjectsWithTag("Tutorial Panel"); //Put all panels with the tag "Tutorial Panel" into a list
        foreach (GameObject panel in panelList) { panel.SetActive(false); } //Hide every panel from the screen



        canvas = FindObjectOfType<Canvas>(); //Get the canvas GameObject
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height; //Get the height of the canvas
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width; //Get the width of the canvas


        allCurrentPanels = HighlightPanelNegativeSpace(panelOpacity);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextPanel();
        }
    }


    List<GameObject> HighlightPanelNegativeSpace(float allPanelsOpacity) //Using a panelIndex, create panels in all spaces around the panel at the panelIndex in the panelList
    {
        List<GameObject> NegativeSpacePanels = new List<GameObject>();


        RectTransform rt = panelList[panelIndex].GetComponent<RectTransform>();



        Vector2 bottomPanelPosition = new Vector2(canvasWidth / 2, rt.anchoredPosition.y - rt.sizeDelta.y/2 - (rt.anchoredPosition.y - rt.sizeDelta.y / 2) / 2);
        Vector2 bottomPanelSize = new Vector2(canvasWidth, rt.anchoredPosition.y - rt.sizeDelta.y/2);

        NegativeSpacePanels.Add(CreatePanel(bottomPanelPosition, bottomPanelSize, Color.white, allPanelsOpacity));



        Vector2 topPanelPosition = new Vector2(canvasWidth / 2, canvasHeight - (canvasHeight - rt.anchoredPosition.y + rt.sizeDelta.y / 2)/2 + rt.sizeDelta.y/2);
        Vector2 topPanelSize = new Vector2(canvasWidth, canvasHeight - (rt.anchoredPosition.y + rt.sizeDelta.y / 2));

        NegativeSpacePanels.Add(CreatePanel(topPanelPosition, topPanelSize, Color.white, allPanelsOpacity));



        RectTransform topRt = NegativeSpacePanels[1].GetComponent<RectTransform>();
        RectTransform bottomRt = NegativeSpacePanels[0].GetComponent<RectTransform>();
        



        
        Vector2 rightPanelSize = new Vector2(canvasWidth - (rt.anchoredPosition.x + rt.sizeDelta.x/2), (topRt.anchoredPosition.y - topRt.sizeDelta.y/2) - (bottomRt.anchoredPosition.y + bottomRt.sizeDelta.y/2));
        Vector2 rightPanelPosition = new Vector2(canvasWidth - rightPanelSize.x/2, rt.anchoredPosition.y);

        NegativeSpacePanels.Add(CreatePanel(rightPanelPosition, rightPanelSize, Color.white, allPanelsOpacity));



        
        Vector2 leftPanelSize = new Vector2(rt.anchoredPosition.x - rt.sizeDelta.x / 2, (topRt.anchoredPosition.y - topRt.sizeDelta.y / 2) - (bottomRt.anchoredPosition.y + bottomRt.sizeDelta.y / 2));
        Vector2 leftPanelPosition = new Vector2(leftPanelSize.x/2, rt.anchoredPosition.y);

        NegativeSpacePanels.Add(CreatePanel(leftPanelPosition, leftPanelSize, Color.white, allPanelsOpacity));



        foreach (GameObject panel in NegativeSpacePanels)
        {
            panel.transform.localScale = new Vector3(1, 1, 1);
        }

        return NegativeSpacePanels;
    }


    GameObject CreatePanel(Vector2 position, Vector2 size, Color color, float opacity)
    {
        GameObject panel = Instantiate(defaultPanel, new Vector2(0, 0), Quaternion.identity); //Creating new panel GameObject
        panel.transform.SetParent(canvas.transform, true);


        Image panelImage = panel.GetComponent<Image>(); //Getting the panel's Image component
        Color tempPanelImageColor = panelImage.color; //Temporary Image Color variable
        tempPanelImageColor = color; //Setting Color
        tempPanelImageColor.a = opacity; //Setting Opacity
        panelImage.color = tempPanelImageColor; //Setting Color + Opacity of the panel
            

        RectTransform panelRectTransform = panel.GetComponent<RectTransform>(); //Getting the RectTransform of the panel

        panelRectTransform.anchoredPosition = position; //Setting the position of the panel (this is based off the center of the panel)
        panelRectTransform.sizeDelta = size; //Setting the size of the panel (this is relative to the size of the canvas, xSize = 100 is 100 units larger than the canvas on the x-axis)

        return panel;
    }


    void NextPanel()
    {
        panelIndex++;
        ClearList(allCurrentPanels);
        allCurrentPanels = HighlightPanelNegativeSpace(panelOpacity);
    }


    void ClearList(List<GameObject> aList) //Function to remove all GameObjects from a list by deleting them
    {
        foreach (GameObject thing in aList) { Destroy(thing); }
    }
}
