using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereVisualizer : MonoBehaviour {

    public float scale;
    public float maxSpectrum;

    private float[] spectrum;
    private int spectrumRange;
    private float median;
    
    
	void Start ()
    {
        spectrumRange = AudioSpectrumListener.spectrum.Length;
        spectrum = new float[spectrumRange];
	}
	
	void Update ()
    {
        spectrum = AudioSpectrumListener.spectrum;

        median = 0;
        for(int i = 0; i < maxSpectrum; i++)
        {
            median += spectrum[i];
        }
        median = median / maxSpectrum;
        median *= scale;

        transform.localScale = new Vector3(median, median, median);

	}
}
