using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfIveBeenClicked : MonoBehaviour
{

    public int type = 0;
    public bool HasChanged = true;
    
    
    public SpriteRenderer currentTileSprite;
    public Sprite x;
    public Sprite o;


    void OnMouseDown()
    {

        if (TicTacToeRunner.turnCounter % 2 == 0)
        {
            type = 1;
            TicTacToeRunner.turnCounter++;
            HasChanged = false;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (HasChanged == false)
        {

            if (type == 1)
            {
                currentTileSprite.sprite = x;
            }
            else if (type == 2)
            {
                currentTileSprite.sprite = o;  
            }

            HasChanged = true;
        }
    }
}
