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

    public List<List<int>> mapList = new List<List<int>>();


    void Start()
    {

        for (int i = 0; i < mapWidth; i++)
        {
            List<int> tempList = new List<int>();

            for (int j = 0; j < mapHeight; j++)
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
                GameObject currentTile = Instantiate(tile, new Vector2(i*tileSize - spawnWidthOffset, j*tileSize - spawnHeightOffset), Quaternion.identity);

                currentTile.GetComponent<TileScript>().xCoord = i;
                currentTile.GetComponent<TileScript>().yCoord = j;

                currentTile.transform.localScale = new Vector2(tileSize, tileSize);
            }
        }
    }


    void Update()
    {

    }
}

