using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnObjectDetection : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clickity Clackity");
    }
}
