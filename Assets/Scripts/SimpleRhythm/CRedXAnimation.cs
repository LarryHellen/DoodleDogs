using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRedXAnimation : MonoBehaviour
{
    private Rigidbody rb;
    private RectTransform rt;
    [SerializeField] float minPowerValue;
    [SerializeField] float maxPowerValue;
    private ContinousNoteSpawning nSS;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rt = GetComponent<RectTransform>();
        nSS = GameObject.Find("GameManager").GetComponent<ContinousNoteSpawning>();


        rb.AddForce(new Vector3(0, Random.Range(minPowerValue, maxPowerValue), 0), ForceMode.Impulse);
    }


    void Update()
    {
        XDestruction();
    }


    void XDestruction()
    {
        if (rt.anchoredPosition.y <= -nSS.screenHeight / 2 - (nSS.screenHeight * rt.sizeDelta.y / 2))
        {
            Destroy(gameObject);
        }
    }
}
