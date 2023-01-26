using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TutorialMaker : MonoBehaviour
{

    public GameObject[] panelList;
    public int panelIndex = 0;
    private List<GameObject> allCurrentPanels;
    public Canvas canvas;
    private float canvasHeight;
    private float canvasWidth;
    public GameObject defaultPanel;


    void Start()
    {
        panelList = GameObject.FindGameObjectsWithTag("Tutorial Panel"); //Put all panels with the tag "Tutorial Panel" into a list
        foreach (GameObject panel in panelList) { panel.SetActive(false); } //Hide every panel from the screen



        canvas = FindObjectOfType<Canvas>(); //Get the canvas GameObject
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height; //Get the height of the canvas
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width; //Get the width of the canvas

        print(canvasWidth);
        print(canvasHeight);

        //Testing Stuff Here
        CreatePanel(500f, 500f, -100f, -100f, Color.white, 1f);


        allCurrentPanels = HighlightPanelNegativeSpace();
    }


    void Update()
    {

    }


    List<GameObject> HighlightPanelNegativeSpace() //Using a panelIndex, create panels in all spaces around the panel at the panelIndex in the panelList
    {
        List<GameObject> NegativeSpacePanels = new List<GameObject>();



        RectTransform rt = panelList[panelIndex].GetComponent<RectTransform>();




        //Algorithm to find and create all panels in negative space



        //3 step plan to fill in the negative space of the important panel with other panels



        //Go from the bottom of the screen to the bottom egde of the panel (from panelList[panelIndex]) and create a panel in that space spanning the full width of the screen





        //Start from the left and right sides of the important panel and continue to the screens edge with that height (create panels to fill that space)


        //Start from the top of the important panel, move to the edge of the screen on the left and create a panel spanning the full width of the screen and the full height remaining





        //panelList[panelIndex].SetActive(false); <- Make the panel the negative space is being built off of hidden
        //return NegativeSpacePanels;

        return NegativeSpacePanels;
    }


    GameObject CreatePanel(float xPos, float yPos, float xSize, float ySize, Color color, float opacity)
    {
        //Required parameters
        //x-pos, y-pos, size-x, size-y, color, opacity


        GameObject panel = Instantiate(defaultPanel, new Vector2(0, 0), Quaternion.identity); //Creating new panel GameObject
        panel.transform.SetParent(canvas.transform, true);


        Image panelImage = panel.GetComponent<Image>(); //Getting the panel's Image component
        Color tempPanelImageColor = panelImage.color; //Temporary Image Color variable
        tempPanelImageColor = color; //Setting Color
        tempPanelImageColor.a = opacity; //Setting Opacity
        panelImage.color = tempPanelImageColor; //Setting Color + Opacity of the panel
            

        RectTransform panelRectTransform = panel.GetComponent<RectTransform>(); //Getting the RectTransform of the panel

        panelRectTransform.anchoredPosition = new Vector2(xPos, yPos); //Setting the position of the panel (this is based off the center of the panel)
        panelRectTransform.sizeDelta = new Vector2(xSize, ySize); //Setting the size of the panel (this is relative to the size of the canvas, xSize = 100 is 100 units larger than the canvas on the x-axis)


        //SET ALL 4 MARGINS OF THE PANEL TO 4 CERTAIN AMOUNTS

        return panel;
    }


    private void OnMouseDown()
    {
        NextPanel();
    }


    void NextPanel()
    {
        panelIndex++;
        ClearList(allCurrentPanels);
        allCurrentPanels = HighlightPanelNegativeSpace();
    }


    void ClearList(List<GameObject> aList) //Function to remove all GameObjects from a list by deleting them
    {
        foreach (GameObject thing in aList) { Destroy(thing); }
    }

}
