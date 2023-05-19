using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadChapter : MonoBehaviour
{
    private JsonDataManipulation jsonDataManipulation = new JsonDataManipulation();
    public int selectedChapter;
    public void LoadChapterWithJsonSetting()
    {
        //Set json data for specific chapter
        jsonDataManipulation.currentChapter = selectedChapter;
        jsonDataManipulation.SaveByJSON();
        SceneManager.LoadScene("RefactoredCutscenes");
    }
}