using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public GameObject currentTutorials;
    private Board board;
    public string sceneType;
    public List<GameObject> allScenes;
    private int currentIndex;


    // Start is called before the first frame update
    void Start()
    {
        if(sceneType == "board"){
            board = FindObjectOfType<Board>();
        }
        allScenes = new List<GameObject>();
        foreach(Transform child in currentTutorials.transform){
            allScenes.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        allScenes[0].SetActive(true);
    }

    public void nextTutorial(){
        allScenes[currentIndex].SetActive(false);
        currentIndex++;
        if(currentIndex == allScenes.Count){
            if(sceneType == "board"){
                board.tutorialEnabled = false;
            }
        } else{
            allScenes[currentIndex].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
