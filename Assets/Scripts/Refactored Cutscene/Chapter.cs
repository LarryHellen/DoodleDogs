using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Chapter
{
    public List<GameObject> cutscenes = new List<GameObject>();
    public int currentCutscene = -1;


    public void SetCutsceneList(GameObject chapterGameObject)
    {
        for (int i = 0; i < chapterGameObject.transform.childCount; i++)
        {
            cutscenes.Add(chapterGameObject.transform.GetChild(i).gameObject);
        }
    }


    public void StepForward()
    {
        currentCutscene++;

        if (currentCutscene != 0) {cutscenes[currentCutscene - 1].SetActive(false);}

        if (currentCutscene >= cutscenes.Count && CutsceneSystem.instance.currentChapter != CutsceneSystem.instance.chapterGameObjects.Count - 1)
        {
            CutsceneSystem.instance.NextChapter();
            return;
        }
        else if (currentCutscene >= cutscenes.Count && CutsceneSystem.instance.currentChapter == CutsceneSystem.instance.chapterGameObjects.Count - 1)
        {
            currentCutscene--;
        }
        else if (cutscenes[currentCutscene].CompareTag("Game"))
        {
            SceneManager.LoadScene(cutscenes[currentCutscene].name);
            return;
        }
        
        ButtonHideCheck();

        cutscenes[currentCutscene].SetActive(true);
    }


    public void StepBackward()
    {
        currentCutscene--;

        if (currentCutscene < cutscenes.Count - 1) {cutscenes[currentCutscene + 1].SetActive(false);}

        if (currentCutscene < 0 && CutsceneSystem.instance.currentChapter != 0)
        {
            CutsceneSystem.instance.PreviousChapter();
            return;
        }
        else if (currentCutscene < 0 && CutsceneSystem.instance.currentChapter == 0)
        {
            currentCutscene++;
        }

        ButtonHideCheck();

        cutscenes[currentCutscene].SetActive(true);
    }


    private void ButtonHideCheck()
    {
        if (cutscenes[currentCutscene].CompareTag("SpecialForwards"))
        {
            CutsceneSystem.instance.ForwardButton.SetActive(false);
        }
        else if (cutscenes[currentCutscene].CompareTag("NoBackwards"))
        {
            CutsceneSystem.instance.BackButton.SetActive(false);
        }
        else if (cutscenes[currentCutscene].CompareTag("SpecialForwardsANDNoBackwards"))
        {
            CutsceneSystem.instance.ForwardButton.SetActive(false);
            CutsceneSystem.instance.BackButton.SetActive(false);
        }
        else 
        {
            CutsceneSystem.instance.ForwardButton.SetActive(true);
            CutsceneSystem.instance.BackButton.SetActive(true);
        }
    }
}