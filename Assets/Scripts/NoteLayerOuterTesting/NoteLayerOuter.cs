using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class NoteLayerOuter : MonoBehaviour
{
    AudioSource _audioSource;

    public float[] _samples = new float[64];

    public HashSet<int> theSet = new HashSet<int>();

    public List<int> theList = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();

        //Seperate the frequencies into 4 sections and avg them out to see if a sound should be played in that lane
    }


    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        //Debug.Log(_samples.Max());

        
        for(int i = 0; i < _samples.Count(); i++)
        {
            if (_samples[i] > .1f)
            {
                theSet.Add(i);
            }
        }

        theList = new List<int>(theSet);
        
    }
}
