using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeRunner : MonoBehaviour
{
    public GameObject tile;

    private List<List<GameObject>> board = new List<List<GameObject>>();
    public static int turnCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            List<GameObject> row = new List<GameObject>();

            for (int j = 0; j < 3; j++)
            {
                GameObject Tile = Instantiate(tile, new Vector3(i-1, j, 0), Quaternion.identity);
                row.Add(Tile);
            }

            board.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (turnCounter % 2 == 1)
        {
            //board[1][2].GetComponent<IfIveBeenClicked>().type = "2"; ERROR HERE
            turnCounter++;
        }

        //Win Detection Here
    }
}
