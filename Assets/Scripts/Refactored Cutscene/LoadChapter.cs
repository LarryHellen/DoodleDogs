using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadChapter : MonoBehaviour
{
    private JsonDataManipulation jsonDataManipulation = new JsonDataManipulation();
    public int selectedChapter;


    public void LoadChapterWithJsonSetting()
    {
        //Set json data for specific chapter

        //ITS BREAKING HERE BC UR NOT WORKING OFF A LOAD BY JSON VERSION OF THE CLASS
        jsonDataManipulation.LoadByJSON();

        int lastChapter = jsonDataManipulation.currentChapter;
        jsonDataManipulation.currentChapter = selectedChapter;

        if (lastChapter == selectedChapter)
        {
            print("Current cutscene: " + jsonDataManipulation.currentCutscene);
            print("Current chapter: " + jsonDataManipulation.currentChapter);
            print("Selected chapter: " + selectedChapter);
            SceneManager.LoadScene("RefactoredCutscenes");
            jsonDataManipulation.SaveByJSON();
            return;
        }
        
        jsonDataManipulation.currentCutscene = -1;
        jsonDataManipulation.SaveByJSON();
        SceneManager.LoadScene("RefactoredCutscenes");
    }
}