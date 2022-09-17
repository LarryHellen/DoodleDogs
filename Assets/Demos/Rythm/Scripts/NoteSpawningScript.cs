using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawningScript : MonoBehaviour
{
    public List<bool> NoteList1 = new List<bool>();
    public List<bool> NoteList2 = new List<bool>();
    public List<bool> NoteList3 = new List<bool>();
    public List<bool> NoteList4 = new List<bool>();

    public string EasyProgramNoteListOne; // First Number is starting position of notes, Second Number is interval of notes, Third Number is end position of notes | Write "None" If you don't want to use it
    public string EasyProgramNoteListTwo;
    public int SongListLength;

    public GameObject Note;
    public float TimeBetweenNotes;

    private List<List<bool>> FullNoteList = new List<List<bool>>();
    private int SecondsPast = 0;
    private float period = 0;


    void Start()
    {

        //Start of First Note Easy Program

        if (EasyProgramNoteListOne != "None")
        {
            for (int i = 0; i < SongListLength; i++)
            {
                NoteList1.Add(false);
            }

            string[] NoteCode1 = EasyProgramNoteListOne.Split(" ");

            for (int i = int.Parse(NoteCode1[0]); i < int.Parse(NoteCode1[2]); i += int.Parse(NoteCode1[1]))
            {
                NoteList1[i] = true;
            }

            //End of First Note Easy Program


            //Start of Second Note Easy Program

            if (EasyProgramNoteListTwo != "None")
            {
                for (int i = 0; i < SongListLength; i++)
                {
                    NoteList2.Add(false);
                }

                string[] NoteCode2 = EasyProgramNoteListTwo.Split(" ");

                for (int i = int.Parse(NoteCode2[0]); i < int.Parse(NoteCode2[2]); i += int.Parse(NoteCode2[1]))
                {
                    NoteList2[i] = true;
                }
            }

            //End of Second Note Easy Program


            FullNoteList.Add(NoteList1);
            FullNoteList.Add(NoteList2);
            FullNoteList.Add(NoteList3);
            FullNoteList.Add(NoteList4);
        }
    }

        void Update()
        {
            if (period > TimeBetweenNotes)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (FullNoteList[i][SecondsPast] == true)
                    {
                        Instantiate(Note, new Vector3((2 * i) - 3, 5, 0), Quaternion.identity);
                    }
                }
                SecondsPast++;
                period = 0;
            }
            period += UnityEngine.Time.deltaTime;
        }
}