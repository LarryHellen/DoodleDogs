using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class NoteLayerOuter : MonoBehaviour
{
    AudioSource _audioSource;

    public float[] _samples = new float[64];



    public int necessaryPercentHigherChannel1; //2500
    public int necessaryPercentHigherChannel2;
    public int necessaryPercentHigherChannel3;
    public int necessaryPercentHigherChannel4;

    private List<int> necessarryPercentHigherList = new List<int>();


    public bool necessaryPercentHigherChannel1Bool;
    public bool necessaryPercentHigherChannel2Bool;
    public bool necessaryPercentHigherChannel3Bool;
    public bool necessaryPercentHigherChannel4Bool;

    private List<bool> necessarryPercentHigherBoolList = new List<bool>();



    public float BPM;
    private float interval;
    private float period = 0;


    private int[] thisTimeAround = new int[4];

    public static List<List<int>> fullNoteList = new List<List<int>>();



    private bool running = true;


    // Start is called before the first frame update
    void Start()
    {
        interval = 60 / BPM;

        necessarryPercentHigherList = new List<int>() { necessaryPercentHigherChannel1, necessaryPercentHigherChannel2, necessaryPercentHigherChannel3, necessaryPercentHigherChannel4 };


        necessarryPercentHigherBoolList = new List<bool>() { necessaryPercentHigherChannel1Bool, necessaryPercentHigherChannel2Bool, necessaryPercentHigherChannel3Bool, necessaryPercentHigherChannel4Bool };

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running == true)
        {
            if (period > interval)
            {
                fullNoteList.Add(new List<int>(thisTimeAround));
                thisTimeAround = new int[4];
                period = 0;
            }
            period += UnityEngine.Time.deltaTime;

            List<int> patternONotes = GetSpectrumAudioSource();

            for (int i = 0; i < 4; i++)
            {
                if (thisTimeAround[i] == 0 && patternONotes[i] == 1)
                {
                    thisTimeAround[i] = patternONotes[i];
                }
            }
        }

        //Seperate the frequencies into 4 sections and avg them out to see if a sound should be played in that lane
    }

    public void Pause()
    {

        string wholeLine = "{";

        for (int i = 0; i < fullNoteList.Count(); i++)
        {

            wholeLine += "new List<int>() {";

            for (int j = 0; j < 4; j++)
            {
                wholeLine += fullNoteList[i][j];
                if (j != 3)
                {
                    wholeLine += ",";
                }
            }

            wholeLine += "},\n";
        }

        wholeLine += "}";
        print(wholeLine);

        running = false;
    }


    void ShowAll(List<float> theList)
    {
        foreach (float num in theList)
        {
            print(num);
        }

        print("------------------");
    }

    List<int> SeperateSingleChannelInto4Sections(int channel)
    {
        var notePattern = new List<int>();

        var tmp = new List<float>();

        for (int j = (_samples.Count() / 4) * (channel - 1); j < (_samples.Count() / 4) * channel; j++)
        {
            tmp.Add(_samples[j]);
        }

        for (int i = 1; i < 5; i++)
        {
            var anotherTmp = new List<float>();

            for (int j = (tmp.Count() / 4) * (i - 1); j < (tmp.Count() / 4) * i; j++)
            {
                anotherTmp.Add(tmp[j]);
            }


            float avg = anotherTmp.Average();

            foreach (float num in anotherTmp)
            {
                if (num > avg * ((100 + necessarryPercentHigherList[channel - 1]) / 100))
                {
                    //print("Category " + channel + " had a frequency (" + num + ") in section " + i + " of itself with an intensity " + necessarryPercentHigherList[channel - 1] + "% higher than the avg of " + avg);
                    notePattern.Add(1);
                    break;
                }
            }
            notePattern.Add(0);
        }

        return notePattern;
    }

    List<int> GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        //Debug.Log(_samples.Max());

        for (int i = 1; i < 5; i++)
        {
            if(necessarryPercentHigherBoolList[i-1] == true)
            {
                return SeperateSingleChannelInto4Sections(i);
            }
        }

        return new List<int>() {0, 0, 0, 0};
    }
}
