using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public List<GameObject> allScenes;
    protected int currentIndex;
    protected bool advanced;
    public int normalLastIndex;
    public int testingCurrentIndex;
    public bool testingAdvanced;
    public bool shouldEnd;


    

    public void NextTutorial(){
        allScenes[currentIndex].SetActive(false);
        currentIndex++;
        if(currentIndex == normalLastIndex + 1 && advanced == false){
            print("got here 1");
            EndTutorial();
        } else if(currentIndex == allScenes.Count && advanced == true){
            EndTutorial();
            print("got here 2");
        }
         else{
            allScenes[currentIndex].SetActive(true);
        }
    }

    public void ResetScenes()
    {
        foreach(GameObject scene in allScenes)
        {
            scene.SetActive(false);
        }
        currentIndex = 0;
    }

    public void Next(){
        print("got here 5");
    }

    public void EndTutorial(){
        shouldEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        testingCurrentIndex = currentIndex;
        testingAdvanced = advanced;
    }
}
