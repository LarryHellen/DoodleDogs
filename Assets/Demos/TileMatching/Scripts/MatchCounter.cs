using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchCounter : MonoBehaviour
{
    private int currentMatches;
    private int neededMatches;
    public string matchType;
    private TextMeshProUGUI textField;


    // Start is called before the first frame update
    public void Create(int neededMatches, string matchType)
    {
        currentMatches = 0;
        this.neededMatches = neededMatches;
        this.matchType = matchType;
        textField = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void UpdateText(){
        textField.text = matchType + " Matches: " + currentMatches + "/" + neededMatches;
    }

    public void IncreaseMatches(int numberToAdd){
        currentMatches += numberToAdd;
        if(currentMatches > neededMatches){
            currentMatches = neededMatches;
        }
    }

}
