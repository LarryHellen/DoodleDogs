using UnityEngine;
using System.Collections;

public class GameTileScript : MonoBehaviour
{
    public int xCoord;
    public int yCoord;



    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (this.GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                print(xCoord);
                print(yCoord);
            }
        }
    }
}
