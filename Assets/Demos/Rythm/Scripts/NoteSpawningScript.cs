using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawningScript : MonoBehaviour
{

    public List<bool> NoteList1;
    public List<bool> NoteList2;
    public List<bool> NoteList3;
    public List<bool> NoteList4;

    public GameObject Note;
    public float TimeBetweenNotes;

    private List<List<bool>> FullNoteList = new List<List<bool>>();
    private int SecondsPast = 0;
    private float period = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        FullNoteList.Add(NoteList1);
        FullNoteList.Add(NoteList2);
        FullNoteList.Add(NoteList3);
        FullNoteList.Add(NoteList4);
    }

    // Update is called once per frame
    void Update()
    {
        if (period > TimeBetweenNotes)
        {
            for (int i = 0; i < 4; i++)
            {
                if (FullNoteList[i][SecondsPast] == true)
                {
                    Instantiate(Note, new Vector3((2 * i)-3, 5, 0), Quaternion.identity);
                }
            }
            SecondsPast++;
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }
}