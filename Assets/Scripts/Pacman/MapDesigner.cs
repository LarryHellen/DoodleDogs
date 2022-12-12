using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDesigner : MonoBehaviour
{
    public GameObject tile;
    public GameObject renderButton;
    public GameObject mapDesigningGameObject;

    public float tileSize;

    public int mapWidth;
    public int mapHeight;

    public float spawnHeightOffset;
    public float spawnWidthOffset;

    public bool mapEditor;

    public List<List<int>> mapList = new List<List<int>>();


    void Start()
    {

        if (mapEditor == false)
        {
            renderButton.SetActive(false);
            tile.SetActive(false);
            mapDesigningGameObject.SetActive(false);
        }

        if (mapEditor != false)
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
                    GameObject currentTile = Instantiate(tile, new Vector2(i * tileSize - spawnWidthOffset, j * tileSize - spawnHeightOffset), Quaternion.identity);

                    currentTile.GetComponent<TileScript>().xCoord = i;
                    currentTile.GetComponent<TileScript>().yCoord = j;

                    currentTile.transform.localScale = new Vector2(tileSize, tileSize);
                }
            }
        }
        
    }

    public void RenderMap()
    {
        mapList.Reverse();

        string totalString = "mapList = new List<List<int>>()\n{\n";


        foreach (List<int> lineOfNums in mapList)
        {
            
            string lineToBePrinted = "";

            foreach(int num in lineOfNums)
            {
                lineToBePrinted += num + ", ";
            }

            totalString += "    new List<int>(){" + lineToBePrinted + "}, \n";
        }

        totalString += "};";

        print(totalString);

        mapList.Reverse();
    }

    void Update()
    {

    }
}

