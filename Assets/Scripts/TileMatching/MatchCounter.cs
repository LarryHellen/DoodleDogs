using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MatchCounter : MonoBehaviour
{
    private int currentMatches;
    private int neededMatches;
    public string matchType;
    private TextMeshProUGUI textField;
    public bool full;
    public GameObject iconObject;
    public Sprite[] iconSprites;


    // Start is called before the first frame update
    public void Create(int neededMatches, string matchType)
    {
        full = false;
        currentMatches = 0;
        this.neededMatches = neededMatches;
        this.matchType = matchType;
        textField = gameObject.GetComponent<TextMeshProUGUI>();

        if(matchType == "Bread Dot"){
            iconObject.GetComponent<Image>().sprite = iconSprites[2];
        } else if (matchType == "Butter Dot"){
            iconObject.GetComponent<Image>().sprite = iconSprites[1];
        } else if (matchType == "Cheese Dot"){
            iconObject.GetComponent<Image>().sprite = iconSprites[3];
        } else if (matchType == "Ham Dot"){
            iconObject.GetComponent<Image>().sprite = iconSprites[0];
        }

        UpdateText();
    }

    public void UpdateText(){
        textField.text = "" + currentMatches + "/" + neededMatches;
    }

    public void IncreaseMatches(int numberToAdd){
        currentMatches += numberToAdd;
        if(currentMatches > neededMatches){
            currentMatches = neededMatches;
            full = true;
        }
        UpdateText();
    }

}
