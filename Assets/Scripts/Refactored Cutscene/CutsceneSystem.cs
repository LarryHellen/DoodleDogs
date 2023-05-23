using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CutsceneSystem : MonoBehaviour
{
    private JsonDataManipulation jsonDataManipulation = new JsonDataManipulation();
    public static CutsceneSystem instance;

    public List<GameObject> chapterGameObjects;
    [HideInInspector] public GameObject BackButton;
    [HideInInspector] public GameObject ForwardButton;
    private List<Chapter> chapters = new List<Chapter>();
    [HideInInspector] public int currentChapter = -1;

    private List<bool> chaptersUnlocked;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }


        print("YOOOOOOOOOOOOO");


        jsonDataManipulation.LoadByJSON();
        currentChapter = jsonDataManipulation.currentChapter - 1;
        chaptersUnlocked = jsonDataManipulation.chaptersUnlocked;


        BackButton = GameObject.Find("BackButton");
        ForwardButton = GameObject.Find("ForwardButton");


        for (int i = 0; i < chapterGameObjects.Count; i++)
        {
            Chapter chapter = new Chapter();

            chapter.SetCutsceneList(chapterGameObjects[i]);

            chapters.Add(chapter);
        }

        print(currentChapter);
        print(jsonDataManipulation.currentCutscene);

        chapters[currentChapter + 1].currentCutscene = jsonDataManipulation.currentCutscene;

        NextChapter();
    }


    public void NextChapter()
    {
        currentChapter++;
        if (currentChapter >= chapterGameObjects.Count) {return;}
        StepForward();
        if (currentChapter != 0) {chapterGameObjects[currentChapter - 1].SetActive(false);}
        chapterGameObjects[currentChapter].SetActive(true);


        chaptersUnlocked[currentChapter] = true;

        jsonDataManipulation.currentChapter = currentChapter;
        jsonDataManipulation.chaptersUnlocked = chaptersUnlocked;
        jsonDataManipulation.SaveByJSON();
    }


    public void PreviousChapter()
    {
        currentChapter--;

        if (currentChapter < 0) {return;}

        chapters[currentChapter].currentCutscene = chapters[currentChapter].cutscenes.Count;
        
        StepBackward();
        if (currentChapter != chapterGameObjects.Count-1) {chapterGameObjects[currentChapter + 1].SetActive(false);}
        chapterGameObjects[currentChapter].SetActive(true);
    }


    public void StepForward()
    {
        chapters[currentChapter].StepForward();
    }


    public void StepBackward()
    {
        chapters[currentChapter].StepBackward();
    }
}