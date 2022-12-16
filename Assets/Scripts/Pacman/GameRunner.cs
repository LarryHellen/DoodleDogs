using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameRunner : MonoBehaviour
{
    private List<List<int>> mapList;

    public Vector2 spawnPosition;

    public GameObject wallTile;

    public float tileSize;
    public float spawnHeightOffset;
    public float spawnWidthOffset;

    public GameObject playerPrefab;

    private Vector2 movementList;

    public Vector2 playerPos;

    private Vector2 possiblePos;

    private Vector2 posToBe;

    private float percentageDistance;

    public float timeBetweenTiles;

    public float coroutineTimeBetween;

    public float waitTime;

    private float timeFraction;

    private GameObject currentPlayer;

    private float timeElapsed;

    void PrintOutMapList()
    {
        foreach (List<int> listThing in mapList)
        {
            string aString = "";
            foreach (int num in listThing)
            {
                aString += num + " ";
            }
            print(aString);
        }
    }

    void Start()
    {
        playerPos = spawnPosition;

        mapList = new List<List<int>>()
        {
            new List<int>(){1, 1, 1, 1, },
            new List<int>(){0, 0, 0, 1, },
            new List<int>(){0, 0, 0, 1, },
            new List<int>(){0, 1, 0, 1, },
        };


        for (int i = 0; i < mapList.Count; i++)
        {
            for (int j = 0; j < mapList[0].Count; j++)
            {
                if (mapList[i][j] == 1)
                {
                    GameObject currentTile = Instantiate(wallTile, new Vector2(i * tileSize - spawnWidthOffset, j * tileSize - spawnHeightOffset), Quaternion.identity);

                    currentTile.GetComponent<GameTileScript>().xCoord = i;
                    currentTile.GetComponent<GameTileScript>().yCoord = j;

                    currentTile.transform.localScale = new Vector2(tileSize, tileSize);
                }
            }

        }


        PrintOutMapList();

        //SPAWN PLAYER WITH SCALED POSITION (MULTIPLY BY TILE SIZE)
        currentPlayer = Instantiate(playerPrefab, new Vector2(spawnPosition[0] * tileSize - spawnWidthOffset, spawnPosition[1] * tileSize - spawnHeightOffset), Quaternion.identity);
        
    }


    void Update()
    {
        //On certain input, lerp to a connecting tile's position (find this position by getting the tile's coordinates and multiplying by tile size)

        //0,0 for tile coordinates starts in the bottom left corner


        StartCoroutine(MovementCode());


        if (!(CheckCollisions()))
        {
            //waitABit();

            //print("No collision this way\n\n\n\n\n\n\n\n\n\n\n");
            

            posToBe = playerPos + movementList;


            StartCoroutine(LerpingCode());
        }
        else
        {
            //print("Collision over here");
        }
        //StartCoroutine(TestingCoroutine());
    }


    public IEnumerator MovementCode()
    {

        if (Input.GetKey(KeyCode.W))
        {
            //print("You pressed the W key");
            movementList = new Vector2(0f, 1f);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            //print("You pressed the A key");
            movementList = new Vector2(-1f, 0f);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            //print("You pressed the S key");
            movementList = new Vector2(0f, -1f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            //print("You pressed the D key");
            movementList = new Vector2(1f, 0f);

        }

        yield return new WaitForSeconds(coroutineTimeBetween);
    }

    public IEnumerator LerpingCode()
    {
        percentageDistance = 0;


        //print(percentageDistance);


        while (timeElapsed < timeBetweenTiles)
        {
            currentPlayer.transform.position = Vector2.Lerp(currentPlayer.transform.position, new Vector2((posToBe[0] * tileSize - spawnWidthOffset), (posToBe[1] * tileSize - spawnHeightOffset)), timeElapsed / timeBetweenTiles);
            timeElapsed += Time.deltaTime;
            //print("This thing");
        }

        //print("this other thing");
        currentPlayer.transform.position = new Vector2((posToBe[0] * tileSize - spawnWidthOffset), (posToBe[1] * tileSize - spawnHeightOffset));

        timeElapsed = 0;

        yield return new WaitForSeconds(waitTime);
        /*
        while (percentageDistance < 1f)
        {
            print("yo");
            print(percentageDistance);


            timeFraction = Time.deltaTime / timeBetweenTiles;

            percentageDistance += timeFraction;



            currentPlayer.transform.position = Vector2.Lerp(currentPlayer.transform.position, new Vector2((posToBe[0] * tileSize - spawnWidthOffset), (posToBe[1] * tileSize - spawnHeightOffset)), timeFraction);
            //currentPlayer.transform.position = new Vector2((posToBe[0] * tileSize - spawnWidthOffset), (posToBe[1] * tileSize - spawnHeightOffset));
        }
        */

        playerPos = posToBe;


        yield return null;
    }

    public IEnumerator waitABit()
    {
        yield return new WaitForSeconds(waitTime);
    }


    bool CheckCollisions()
    {
        possiblePos = playerPos + movementList;
        try
        {
            //print(movementList);
            //print(playerPos);
            //print(possiblePos);

            if (mapList[(int)possiblePos[0]][(int)possiblePos[1]] == 1)
            {
                //print("    Wall Collision");
                return true;
                
            }
            else
            {
                //print("    No Collision");
                return false;

            }
        }
        catch (Exception e)
        {
            //print(e);
            //print("    Edge Collision");
            return true;

        }
    }
}
