using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Tile 
{
    public GameObject tileGameObject;
    public RectTransform tileRectTransform;
    public int type = -1;
    List<Sprite> tileSprites;
    Image image;


    public Tile(List<Sprite> tileSprites, GameObject tileGameObject)
    {
        this.tileSprites = tileSprites;
        this.tileGameObject = tileGameObject;
        this.tileRectTransform = tileGameObject.GetComponent<RectTransform>();
        this.image = tileGameObject.GetComponent<Image>();  
    }


    public void SetSprite(int turnCounter)
    {
        Debug.Log("SetSprite");
        type = turnCounter % 2;
        image.sprite = tileSprites[type];
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
    }
}