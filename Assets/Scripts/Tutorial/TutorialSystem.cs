using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public List<GameObject> allScenes;
    protected int currentIndex;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(){
        
    }

    public void NextTutorial(){
        allScenes[currentIndex].SetActive(false);
        currentIndex++;
        if(currentIndex == allScenes.Count){
            EndTutorial();
        } else{
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
