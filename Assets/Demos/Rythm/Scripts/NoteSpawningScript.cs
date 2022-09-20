using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawningScript : MonoBehaviour
{
    public List<bool> NoteList1;
    public List<bool> NoteList2;
    public List<bool> NoteList3;
    public List<bool> NoteList4;

    public string EasyProgramNoteListOne; // First Number is starting position of notes, Second Number is interval of notes, Third Number is end position of notes | Write "None" If you don't want to use it
    public string EasyProgramNoteListTwo;
    public string EasyProgramNoteListThree;
    public string EasyProgramNoteListFour;

    public int SongListLength;

    public GameObject Note;
    public float TimeBetweenNotes;

    private List<List<bool>> FullNoteList = new List<List<bool>>();
    private int SecondsPast = 0;
    private float period = 0;

    List<bool> ReadEasyProgramNoteList(string EasyProgramNoteList)
    {
        List<bool> NoteList = new List<bool>();

        if (EasyProgramNoteList != "None")
        {
            for (int i = 0; i < SongListLength; i++)
            {
                NoteList.Add(false);
            }

            string[] NoteCode = EasyProgramNoteList.Split(" ");

            for (int i = int.Parse(NoteCode[0]); i < int.Parse(NoteCode[2]); i += int.Parse(NoteCode[1]))
            {
                NoteList[i] = true;
            }
        }
        return NoteList;
    }

    void Start()
    {
        NoteList1 = ReadEasyProgramNoteList(EasyProgramNoteListOne);
        NoteList2 = ReadEasyProgramNoteList(EasyProgramNoteListTwo);
        NoteList3 = ReadEasyProgramNoteList(EasyProgramNoteListThree);
        NoteList4 = ReadEasyProgramNoteList(EasyProgramNoteListFour);



        FullNoteList.Add(NoteList1);
        FullNoteList.Add(NoteList2);
        FullNoteList.Add(NoteList3);
        FullNoteList.Add(NoteList4);
    }

    void Update()
    {
        if (period > TimeBetweenNotes)
        {
            for (int i = 0; i < 4; i++)
            {
                if (FullNoteList[i][SecondsPast] == true)
                {
                   Instantiate(Note, new Vector3((2 * i) - 3, 6, 0), Quaternion.identity);
                }
            }
            SecondsPast++;
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }
}
