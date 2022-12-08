using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRunner : MonoBehaviour
{
    private List<List<int>> mapList;

    public Vector2 spawnPosition;

    public GameObject wallTile;

    public float tileSize;
    public float spawnHeightOffset;
    public float spawnWidthOffset;

    public GameObject player;

    void Start()
    {
        mapList = new List<List<int>>()
        {
            new List<int>(){1, 1, 1, 0, },
            new List<int>(){1, 0, 1, 0, },
            new List<int>(){0, 0, 0, 0, },
            new List<int>(){0, 1, 0, 1, },
        };


        mapList.Reverse();

        for (int i = 0; i < mapList.Count; i++)
        {
            for (int j = 0; j < mapList[0].Count; j++)
            {
                if (mapList[j][i] == 1)
                {
                    GameObject currentTile = Instantiate(wallTile, new Vector2(i * tileSize - spawnWidthOffset, j * tileSize - spawnHeightOffset), Quaternion.identity);

                    currentTile.GetComponent<GameTileScript>().xCoord = i;
                    currentTile.GetComponent<GameTileScript>().yCoord = j;

                    currentTile.transform.localScale = new Vector2(tileSize, tileSize);
                }
            }

        }


        //SPAWN PLAYER WITH SCALED POSITION (MULTIPLY BY TILE SIZE)
        mapList[(int) spawnPosition[0]][(int) spawnPosition[1]] = 2;
        GameObject currentPlayer = Instantiate(player, new Vector2(spawnPosition[0] * tileSize - spawnWidthOffset, spawnPosition[1] * tileSize - spawnHeightOffset), Quaternion.identity);

    }


    void Update()
    {
        //On certain input, lerp to a connecting tile's position (find this position by getting the tile's coordinates and multiplying by tile size)

        //0,0 for tile coordinates starts in the bottom left corner
    }
}
