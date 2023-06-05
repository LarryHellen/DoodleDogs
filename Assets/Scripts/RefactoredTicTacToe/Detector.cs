using System.Collections;
using UnityEngine;

public class Detector : MonoBehaviour 
{
    GameManager gM;


    void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }


    public void Detect()
    {
        //Debug.Log("Detect");
        if (gM.tiles[gM.boardPositions.IndexOf(this.gameObject)].type < 0 && gM.placingEnabled)
        {
            gM.tiles[gM.boardPositions.IndexOf(this.gameObject)].SetSprite(gM.turnCounter);
            FindObjectOfType<AudioManager>().Play("PlaceBall");
            StartCoroutine(WaitToThink());
        }
    }


    IEnumerator WaitToThink()
    {
        yield return new WaitForSeconds(gM.waitToThinkTime);
        gM.CompleteTurn();
    }
}