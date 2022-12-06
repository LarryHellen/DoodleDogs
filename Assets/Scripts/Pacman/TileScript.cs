using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public int xCoord;
    public int yCoord;

    public GameObject mapDesigner;

    void PlaceTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (this.GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                
                if (mapDesigner.GetComponent<MapDesigner>().mapList[yCoord][xCoord] = 1) //tmp1.GetComponent<IfIveBeenClicked>().type.
                {
                    mapDesigner.GetComponent<MapDesigner>().mapList[yCoord][xCoord] = 0;
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    mapDesigner.GetComponent<MapDesigner>().mapList[yCoord][xCoord] = 1;
                    spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
                }
                
            }
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        PlaceTile();
    }
}
