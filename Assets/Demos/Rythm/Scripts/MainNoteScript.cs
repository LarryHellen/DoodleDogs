using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNoteScript : MonoBehaviour
{

    public float BPM;
    // Start is called before the first frame update
    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        BPM = BPM / 1000;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - new Vector3(0, BPM, 0);
        if (transform.position[1] < -6)
        {
            Destroy(gameObject);
        }
    }
}
