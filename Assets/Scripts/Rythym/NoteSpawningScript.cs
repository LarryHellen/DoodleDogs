using System.Collections;
using System.Collections.Generic;
using UnityEngine;






//THIS SCRIPT IS NOT USED IN THE ACTUAL PROGRAM, PLEASE REFER TO FourAudioNoteSpawner FOR THE CODE FOR NOTE SPAWNING





public class NoteSpawningScript : MonoBehaviour
{
    public GameObject Note;
    public float BPM;
    private float timeBetweenNotes;

    public static List<List<int>> FullNoteList = new List<List<int>>();
    private int intervalsPast = 0;
    private float period;
    public float delay;


    void Start()
    {
        period = delay;

        timeBetweenNotes = 60 / BPM;
    }


    void Update()
    {
        if (period > timeBetweenNotes)
        {
            //print("NOTE SPAWNING SCRIPT STUFF " + FourAudioNoteSpawner.patternONotes[0] + FourAudioNoteSpawner.patternONotes[1] + FourAudioNoteSpawner.patternONotes[2] + FourAudioNoteSpawner.patternONotes[3]);
            for (int i = 0; i < 4; i++)
            {
                //print(intervalsPast);
                if (FourAudioNoteSpawner.FullNoteList[intervalsPast][i] == 1)
                {
                    GameObject ANote = Instantiate(Note, new Vector3(i, 8, 0), Quaternion.identity);
                    ANote.GetComponent<MainNoteScript>().NOTE_TYPE = "TAP";
                }
            }
            intervalsPast++;
            period = 0;
        }
        period += Time.fixedDeltaTime;
    }
}












/*
    List<bool> ReadEasyProgramNoteList(string EasyProgramNoteList)
    {
        List<bool> NoteList = new List<bool>();

        if (EasyProgramNoteList != "None")
        {
            for (int i = 0; i < RealSongListLength; i++)
            {
                NoteList.Add(false);
            }



            string[] AllNoteCode = EasyProgramNoteList.Split(",");
            List<List<string>> AllNoteCodeList = new List<List<string>>();

            foreach (string TheString in AllNoteCode)
            {
                string[] ThisProgrammySection = TheString.Split(" ");
                List<string> TheListVersionOfTheProgrammySection = new List<string>(ThisProgrammySection);
                AllNoteCodeList.Add(TheListVersionOfTheProgrammySection);
            }


            foreach (List<string> IntervalSet in AllNoteCodeList)
            {

                //Debug.Log("From : " + IntervalSet[0] + " To : " + IntervalSet[2] + " At an interval of : " + IntervalSet[1]);



                for (int i = int.Parse(IntervalSet[0]); i < int.Parse(IntervalSet[2]); i += int.Parse(IntervalSet[1]))
                {
                    NoteList[i] = true;
                }
            }

        }
        else
        {
            for (int i = 0; i < RealSongListLength; i++)
            {
                NoteList.Add(false);
            }
        }
        return NoteList;
    }



*/