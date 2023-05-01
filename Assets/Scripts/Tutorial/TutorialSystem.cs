using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public List<GameObject> allScenes;
    protected int currentIndex;
    protected bool advanced;
    public int normalLastIndex;


    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    public void Setup(){
        allScenes[0].SetActive(true);
    }

    public void NextTutorial(){
        allScenes[currentIndex].SetActive(false);
        currentIndex++;
        if(currentIndex == normalLastIndex && advanced == false){
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

    public void Next(){

    }

    public void EndTutorial(){

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
