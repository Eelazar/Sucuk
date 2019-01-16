using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class AudioSpectrumListener : MonoBehaviour {
    
    public enum SpectrumRange { Low = 128, Mid = 256, High = 512 }
    public SpectrumRange spectrumRange;
    public static SpectrumRange staticSpectrumRange;

    AudioSource source;
    public static float[] spectrum;
    public static float[] frequencyBand;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        spectrum = new float[(int)spectrumRange];
        staticSpectrumRange = spectrumRange;
        frequencyBand = new float[8];
    }

    void Start ()
    {
        
	}
	
	void Update ()
    {
        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        if(spectrumRange == SpectrumRange.High)
        {
            MakeFrequencyBands();
        }
	}

    void MakeFrequencyBands()
    {
        //Probably only works in 512 samples
        int count = 0;

        for(int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j = 0; j < sampleCount; j++)
            {
                average += spectrum[count] * (count + 1);
                count++;
            }

            average /= count;

            frequencyBand[i] = average * 10;
        }
    }
}
