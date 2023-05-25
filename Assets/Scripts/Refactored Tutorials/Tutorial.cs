using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Tutorial
{
    private GameObject basicTutorial;
    private GameObject advancedTutorial;

    private List<GameObject> basicTutorialSlides;
    private List<GameObject> advancedTutorialSlides;
    private List<List<GameObject>> bothTutorialsSlides;

    private int advancedTutorialIndex;
    private int tutorialIndex = -1;


    public Tutorial(GameObject basicTutorial, GameObject advancedTutorial, bool advanced)
    {
        this.basicTutorial = basicTutorial;
        this.advancedTutorial = advancedTutorial;

        if (advanced) { advancedTutorialIndex = 1; }
        else { advancedTutorialIndex = 0; }

        SetSlides();

        if (advanced)
        {
            basicTutorial.SetActive(false);
            advancedTutorial.SetActive(true);
        }
        else
        {
            basicTutorial.SetActive(true);
            advancedTutorial.SetActive(false);
        }

        StepForward();
    }


    public void StepForward()
    {
        if (tutorialIndex > -1){ bothTutorialsSlides[advancedTutorialIndex][tutorialIndex].SetActive(false); }
        else { Time.timeScale = 0; } //ANYTHING THAT SHOULD HAPPEN BEFORE THE TUTORIAL STARTS SHOULD GO HERE
        
        tutorialIndex++;

        if (tutorialIndex < bothTutorialsSlides[advancedTutorialIndex].Count){ bothTutorialsSlides[advancedTutorialIndex][tutorialIndex].SetActive(true); } 
        else { Time.timeScale = 1; } //ANYTHING THAT SHOULD HAPPEN AFTER THE TUTORIAL ENDS SHOULD GO HERE
    }


    private void SetSlides()
    {
        basicTutorialSlides = new List<GameObject>();
        advancedTutorialSlides = new List<GameObject>();

        for (int i = 0; i < basicTutorial.transform.childCount; i++)
        {
            basicTutorialSlides.Add(basicTutorial.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < advancedTutorial.transform.childCount; i++)
        {
            advancedTutorialSlides.Add(advancedTutorial.transform.GetChild(i).gameObject);
        }

        bothTutorialsSlides = new List<List<GameObject>>();
        bothTutorialsSlides.Add(basicTutorialSlides);
        bothTutorialsSlides.Add(advancedTutorialSlides);
    }
}
