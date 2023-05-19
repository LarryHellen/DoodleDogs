using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TempToCutsceneScene : MonoBehaviour
{
    public void ToCutsceneScene()
    {
        SceneManager.LoadScene("RefactoredCutscenes");
    }
}
