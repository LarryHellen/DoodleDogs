using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLivesCounter : MonoBehaviour
{
    [Header("Manual Variables")]
    [SerializeField] GameObject lifeXPrefab;

    
    private ContinousNoteSpawning nSS;
    private List<GameObject> livesList = new List<GameObject>();
    private int lives;


    void Start()
    {
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();
        lives = nSS.missesAvaible;

        RectTransform lifeXPrefabRectTransform = lifeXPrefab.GetComponent<RectTransform>();
        float lifeXPrefabWidth = lifeXPrefabRectTransform.sizeDelta.x;


        for (int i = 0; i < lives; i++)
        {
            GameObject lifeX = Instantiate(lifeXPrefab, new Vector3(0,0,0), lifeXPrefab.transform.rotation, gameObject.transform);

            RectTransform lifeXRt = lifeX.GetComponent<RectTransform>();
            lifeXRt.anchoredPosition = new Vector2(lifeXPrefabWidth * i,0);

            livesList.Add(lifeX);
        } 
    }


    public void RemoveLifeFromDisplay()
    {
        if (lives - nSS.missesAvaible < lives)
        {
            Destroy(livesList[lives - nSS.missesAvaible]); //Instead of doing this, later on, make these notes jump down off the screen
        }
    }
}
