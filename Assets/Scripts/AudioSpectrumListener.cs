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

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        spectrum = new float[(int)spectrumRange];
        staticSpectrumRange = spectrumRange;
    }

    void Start ()
    {
        
	}
	
	void Update ()
    {
        source.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
	}
}
