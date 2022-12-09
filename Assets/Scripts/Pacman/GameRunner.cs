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

    private Vector2 movementList;

    private Vector2 playerPos;

    private Vector2 possiblePos;

    private Vector2 posToBe;

    private float percentageDistance;

    public float timeBetweenTiles;

    private float timeFraction;


    void Start()
    {
        playerPos = spawnPosition;

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

        

        if (Input.GetKey(KeyCode.W))
        {
            print("You pressed the W key");
            movementList = new Vector2(0f, 1f);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            print("You pressed the A key");
            movementList = new Vector2(-1f, 0f);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            print("You pressed the S key");
            movementList = new Vector2(0f, -1f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("You pressed the D key");
            movementList = new Vector2(1f, 0f);

        }

        if (!(CheckCollisions()))
        {
            print("No collision this way\n\n\n\n\n\n\n\n\n\n\n");

            posToBe = playerPos + movementList;

            percentageDistance = 0;


            timeFraction = Time.deltaTime / timeBetweenTiles;

            percentageDistance += timeFraction;

            print(percentageDistance);

            //while (percentageDistance < 1f)
            //{
            //player.transform.position = Vector2.Lerp(player.transform.position, new Vector2((posToBe[0] * tileSize - spawnWidthOffset), (posToBe[1] * tileSize - spawnHeightOffset)), 1);
            player.transform.position = new Vector2((posToBe[0] * tileSize - spawnWidthOffset), (posToBe[1] * tileSize - spawnHeightOffset));
            //}

            playerPos = posToBe;
        }
        else
        {
            print("Collision over here");
        }
    }

    bool CheckCollisions()
    {
        possiblePos = playerPos + movementList;
        try
        {
            print(movementList);
            print(playerPos);
            print(possiblePos);

            if (mapList[(int)possiblePos[0]][(int)possiblePos[1]] == 1)
            {
                print("    Wall Collision");
                return true;
                
            }
            else
            {
                print("    No Collision");
                return false;

            }
        }
        catch
        {
            print("    Edge Collision");
            return true;

        }
    }
}
