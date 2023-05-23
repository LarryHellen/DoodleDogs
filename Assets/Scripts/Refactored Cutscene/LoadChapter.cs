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
        print(jsonDataManipulation.chaptersUnlocked[0]);
        jsonDataManipulation.currentChapter = selectedChapter;
        jsonDataManipulation.currentCutscene = -1;
        jsonDataManipulation.SaveByJSON();
        SceneManager.LoadScene("RefactoredCutscenes");
    }
}