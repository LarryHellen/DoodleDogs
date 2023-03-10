using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MatchCounter : MonoBehaviour
{
    public int currentMatches;
    public int neededMatches;
    public string matchType;
    public TextMeshProUGUI textField;
    public bool full;
    public GameObject iconObject;
    public Sprite[] iconSprites;


    // Start is called before the first frame update
    public void Start()
    {
        Create();
    }
    public void Create()
    {
        full = false;
        currentMatches = 0;
        UpdateText();
    }

    public void UpdateText(){
        textField.text = "" + currentMatches + "/" + neededMatches;
    }

    public void IncreaseMatches(int numberToAdd){
        currentMatches += numberToAdd;
        if(currentMatches >= neededMatches){
            currentMatches = neededMatches;
            full = true;
        } else if (currentMatches < 0){
            currentMatches = 0;
        }
        UpdateText();
    }

    public int GetMatches(){
        return currentMatches;
    }

    public void Reset()
    {
        Create();
    }

}
