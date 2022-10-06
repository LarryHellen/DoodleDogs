using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawningScript : MonoBehaviour
{

    private static int SongListLength; //Song Length in Quarters of a Second
    public int RealSongListLength;

    //Tap Note Lists Stuff

    public List<bool> TapNoteList1 = new List<bool>();
    public List<bool> TapNoteList2 = new List<bool>();
    public List<bool> TapNoteList3 = new List<bool>();
    public List<bool> TapNoteList4 = new List<bool>();

    public string TapEasyProgramNoteListOne; // First Number is starting position of notes, Second Number is interval of notes, Third Number is end position of notes | Write "None" If you don't want to use it
    public string TapEasyProgramNoteListTwo;
    public string TapEasyProgramNoteListThree;
    public string TapEasyProgramNoteListFour;


    //Hold Note Lists Stuff

    public List<bool> HoldNoteList1 = new List<bool>();
    public List<bool> HoldNoteList2 = new List<bool>();
    public List<bool> HoldNoteList3 = new List<bool>();
    public List<bool> HoldNoteList4 = new List<bool>();

    public string HoldEasyProgramNoteListOne;
    public string HoldEasyProgramNoteListTwo;
    public string HoldEasyProgramNoteListThree;
    public string HoldEasyProgramNoteListFour;

    //Other Stuff

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




    void Start()
    {

        TapNoteList1 = ReadEasyProgramNoteList(TapEasyProgramNoteListOne);
        TapNoteList2 = ReadEasyProgramNoteList(TapEasyProgramNoteListTwo);
        TapNoteList3 = ReadEasyProgramNoteList(TapEasyProgramNoteListThree);
        TapNoteList4 = ReadEasyProgramNoteList(TapEasyProgramNoteListFour);

        HoldNoteList1 = ReadEasyProgramNoteList(HoldEasyProgramNoteListOne);
        HoldNoteList2 = ReadEasyProgramNoteList(HoldEasyProgramNoteListTwo);
        HoldNoteList3 = ReadEasyProgramNoteList(HoldEasyProgramNoteListThree);
        HoldNoteList4 = ReadEasyProgramNoteList(HoldEasyProgramNoteListFour);



        FullNoteList.Add(TapNoteList1);
        FullNoteList.Add(TapNoteList2);
        FullNoteList.Add(TapNoteList3);
        FullNoteList.Add(TapNoteList4);

        FullNoteList.Add(HoldNoteList1);
        FullNoteList.Add(HoldNoteList2);
        FullNoteList.Add(HoldNoteList3);
        FullNoteList.Add(HoldNoteList4);
    }

    void Update()
    {
        if (period > TimeBetweenNotes)
        {
            for (int i = 0; i < 8; i++)
            {
                if (FullNoteList[i][SecondsPast] == true)
                {

                     //Hold Notes Will Appear Offscreen needs fix

                    if (i <= 3) //TAP NOTE
                    {
                        GameObject ANote = (GameObject)Instantiate(Note, new Vector3((1f * i) - 1.5f, 8, 0), Quaternion.identity);
                        ANote.GetComponent<MainNoteScript>().NOTE_TYPE = "TAP";
                    }
                    else //HOLD NOTE
                    {
                        GameObject ANote = (GameObject)Instantiate(Note, new Vector3(((1f * i)%4) - 1.5f, 8, 0), Quaternion.identity);
                        ANote.GetComponent<MainNoteScript>().NOTE_TYPE = "HOLD";
                        ANote.transform.localScale = new Vector3(1f, 3f, 0.1f);
                        var cubeRenderer = ANote.GetComponent<Renderer>();
                        cubeRenderer.material.SetColor("_Color", Color.blue);
                    }
                    
                }
            }
            SecondsPast++;
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }
}