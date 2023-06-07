using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class TutorialHandler : MonoBehaviour 
{
    [HideInInspector]
    public GameObject basicTutorial;
    [HideInInspector]
    public GameObject advancedTutorial;
    public bool advanced;
    private bool tutorialEnabled;
    private Tutorial tutorial;

    private JsonDataManipulation jsonDataManipulation = new JsonDataManipulation();
    private List<bool> tutorials;
    private List<bool> tutorialsDone;


    public void StepForward()
    {
        tutorial.StepForward();
    }


    public void RegisterTutorial()
    {
        jsonDataManipulation.LoadByJSON();
        tutorials = jsonDataManipulation.tutorials;
        tutorialsDone = jsonDataManipulation.tutorialsDone;


        if (tutorials[2*jsonDataManipulation.currentChapter] == false && tutorials[2 * jsonDataManipulation.currentChapter + 1] == false)
        {
            tutorials[2 * jsonDataManipulation.currentChapter] = true;
            tutorialsDone[2 * jsonDataManipulation.currentChapter] = true;
        }
        else if (tutorials[2 * jsonDataManipulation.currentChapter] == true && tutorials[2 * jsonDataManipulation.currentChapter + 1] == false)
        {
            tutorials[2 * jsonDataManipulation.currentChapter + 1] = true;
            tutorialsDone[2 * jsonDataManipulation.currentChapter + 1] = true;
        }
        else if (tutorials[2 * jsonDataManipulation.currentChapter] == true && tutorials[2 * jsonDataManipulation.currentChapter + 1] == true)
        {
            tutorials[2 * jsonDataManipulation.currentChapter + 1] = false;
        }


        jsonDataManipulation.tutorialsDone = tutorialsDone;
        jsonDataManipulation.tutorials = tutorials;
        jsonDataManipulation.SaveByJSON();
    }


    public List<bool> RegisterAdvanced()
    {
        jsonDataManipulation.LoadByJSON();
        tutorials = jsonDataManipulation.tutorials;
        tutorialsDone = jsonDataManipulation.tutorialsDone;


        if (jsonDataManipulation.currentChapter == 3)
        {
            advanced = true;
        }
        else if (tutorials[2*jsonDataManipulation.currentChapter] == false && tutorials[2 * jsonDataManipulation.currentChapter + 1] == false)
        {
            advanced = false;
        }
        else if (tutorials[2 * jsonDataManipulation.currentChapter] == true && tutorials[2 * jsonDataManipulation.currentChapter + 1] == false)
        {
            advanced = true; 
        }
        else if (tutorials[2 * jsonDataManipulation.currentChapter] == true && tutorials[2 * jsonDataManipulation.currentChapter + 1] == true)
        {
            advanced = false;
        }

        if (advanced)
        {
            if (tutorialsDone[2 * jsonDataManipulation.currentChapter + 1] == false)
            {
                //tutorial = new Tutorial(basicTutorial, advancedTutorial, advanced);
                if (jsonDataManipulation.currentChapter != 3)
                {
                    print("Activated tutorial");
                    tutorialEnabled = true;
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
            else {
                Time.timeScale = 1;
            }
        }
        else
        {
            if (tutorialsDone[2 * jsonDataManipulation.currentChapter] == false)
            {
                //tutorial = new Tutorial(basicTutorial, advancedTutorial, advanced);
                if (jsonDataManipulation.currentChapter != 3)
                {
                    print("Activated tutorial");
                    tutorialEnabled = true;
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
            else {
                Time.timeScale = 1;
            }
        }


        jsonDataManipulation.tutorialsDone = tutorialsDone;
        jsonDataManipulation.tutorials = tutorials;
        jsonDataManipulation.SaveByJSON();


        return new List<bool>() { advanced, tutorialEnabled };
    }
}