using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNoteScript : MonoBehaviour
{

    private float BPM = 1;
    public GameObject Note;
    // Start is called before the first frame update
    void OnMouseDown()
    {
        transform.position = transform.position + new Vector3(0, 2, 0);
        Instantiate(Note);
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
