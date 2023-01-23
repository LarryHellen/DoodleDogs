using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TutorialMaker : MonoBehaviour
{

    public GameObject[] panelList;
    public int panelIndex = 0;
    private List<GameObject> allCurrentPanels;


    void Start()
    {
        panelList = GameObject.FindGameObjectsWithTag("Tutorial Panel"); //Put all panels with the tag "Tutorial Panel" into a list
        foreach (GameObject panel in panelList) { panel.SetActive(false); } //Hide every panel from the screen

        //NextPanel();
    }


    void Update()
    {

    }


    List<GameObject> HighlightPanelNegativeSpace() //Using a panelIndex, create panels in all spaces around the panel at the panelIndex in the panelList
    {
        //List<GameObject> NegativeSpacePanels;




        //Algorithm to find and create all panels in negative space




        //panelList[panelIndex].SetActive(false); <- Make the panel the negative space is being built off of hidden
        //return NegativeSpacePanels;

        return null;
    }


    void CreatePanel() //Do this before creating the HighlightPanelNegativeSpace function
    {
        //Required parameters

        //size-x, size-y, x-pos, y-pos, color, opacity


        /*
         GameObject panel = new GameObject("Panel");
         panel.AddComponent<CanvasRenderer>();
         i.color = Color.white;
         panel.transform.SetParent(newCanvas.transform, false);
        */
    }


    private void OnMouseDown()
    {
        //NextPanel();
    }


    void NextPanel()
    {
        //panelIndex++;
        //ClearList(allCurrentPanels);
        //allCurrentPanels = HighlightPanelNegativeSpace();
    }


    void ClearList(List<GameObject> aList) //Function to remove all GameObjects from a list by deleting them
    {
        foreach (GameObject thing in aList) { Destroy(thing); }
    }

}
