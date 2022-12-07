using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDesigner : MonoBehaviour
{
    public GameObject tile;
    public float tileSize;

    public int mapWidth;
    public int mapHeight;

    public float spawnHeightOffset;
    public float spawnWidthOffset;

    public static List<List<int>> mapList;


    void Start()
    {
        GameObject sizingTile = Instantiate(tile, new Vector2(0, 0), Quaternion.identity);
        sizingTile.transform.localScale = new Vector2(tileSize, tileSize);

        Collider2D tileCollider = sizingTile.GetComponent<Collider2D>();
        Vector2 actualTileSize = tileCollider.bounds.size;

        float actualTileHeight = actualTileSize[0];
        float actualTileWidth = actualTileSize[1];


        for (int i = 0; i < mapHeight; i++)
        {
            List<int> tempList = new List<int>();

            for (int j = 0; j < mapWidth; j++)
            {
                tempList.Add(0);
            }

            mapList.Add(tempList);
        }


        //Multiply distances by actualTileHeight and actualTileWidth for proper position scaling with tile size


        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                GameObject currentTile = Instantiate(tile, new Vector2(0, 0), Quaternion.identity);

                currentTile.GetComponent<TileScript>().xCoord = i;
                currentTile.GetComponent<TileScript>().yCoord = j;

                currentTile.transform.localScale = new Vector2(tileSize, tileSize);
            }
        }
    }


    void Update()
    {

    }




    /*
    
    public GameObject cube;
    public Vector3 sizeChange; // This has to be set for (x,y,z) in the editor
    //public float cubeSize = 0.2f;
    void OnMouseDown()
    {
        cube.transform.localScale = cube.transform.localScale - sizeChange;    
    }

    */



}

