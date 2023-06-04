using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class DragScrollRefactored : MonoBehaviour
{
    private JsonDataManipulation jsonDataManipulation = new JsonDataManipulation();
    public GameObject background;
    public GameObject settingsMenu;
    public GameObject c2e, c3e, c2d, c3d, c4e, c4d, c5e, c5d;
    public Sprite c2image, c3image, c4image, c5image;
    public int p;
    public Vector3 startPoint = new Vector3(0, 0, 0);
    public int b = 0, c = 0;


    private List<bool> chaptersUnlocked;

    /*
    private void OnMouseDown()
    {
        if (!settingsMenu.activeSelf)
        {
            startPoint = Input.mousePosition;
            //Debug.Log("e");
        }
    }


    private void OnMouseUp()
    {
        startPoint = new Vector3(0, 0, 0);
    }

    
    void Update()
    {
        if (startPoint != new Vector3(0, 0, 0))
        {
            background.transform.position += new Vector3(0, (Input.mousePosition.y - startPoint.y)/2, 0);
            startPoint = Input.mousePosition;
        }
        if (background.transform.position.y < b)
        {
            background.transform.position = new Vector3(background.transform.position.x, b, background.transform.position.z);
        }
        if (background.transform.position.y > c)
        {
            background.transform.position = new Vector3(background.transform.position.x, c, background.transform.position.z);
        }
    }
    */


    void Start()
    {
        jsonDataManipulation.LoadByJSON();

        if (jsonDataManipulation.chaptersUnlocked.Count == 0 || jsonDataManipulation.tutorials.Count == 0)
        {
            print("Defaulted");
            jsonDataManipulation.LoadDefaultValuesFromPlayerDataRefactored();
        }

        chaptersUnlocked = jsonDataManipulation.chaptersUnlocked;

        print("Current cutscene: " + jsonDataManipulation.currentCutscene);
        print("Current chapter: " + jsonDataManipulation.currentChapter);

        if (chaptersUnlocked[1])
        {
            c2e.GetComponent<Button>().interactable = true;
            c2e.GetComponent<Image>().sprite = c2image;
        }
        if (chaptersUnlocked[2])
        {
            c3e.GetComponent<Button>().interactable = true;
            c3e.GetComponent<Image>().sprite = c3image;
        }
        if (chaptersUnlocked[3])
        {
            c4e.GetComponent<Button>().interactable = true;
            c4e.GetComponent<Image>().sprite = c4image;
        }
        if (chaptersUnlocked[4])
        {
            c5e.GetComponent<Button>().interactable = true;
            c5e.GetComponent<Image>().sprite = c5image;
        }

        jsonDataManipulation.SaveByJSON();
    }
}
