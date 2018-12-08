using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereVisualizer : MonoBehaviour {

    public float scaleBase;
    public float scaleMultiplier;
    public float maxSpectrum;

    //Lighting
    public Light baseLight;
    public float lightBaseRange;
    public Light halo;
    public float haloBaseRange;

    private float[] spectrum;
    private int spectrumRange;
    private float median;

    //Smooth Stuff
    private float velocity;
    public float smoothTime;
    
    
	void Start ()
    {
        spectrumRange = AudioSpectrumListener.spectrum.Length;
        spectrum = new float[spectrumRange];      
    }
	
	void Update ()
    {
        spectrum = AudioSpectrumListener.spectrum;

        median = 0;

        median = Mathf.SmoothDamp(median, AudioSpectrumListener.frequencyBand[0], ref velocity, smoothTime);
        
        median *= scaleMultiplier;
        median += scaleBase;
        

        transform.localScale = new Vector3(median, median, median);

        baseLight.range = median + lightBaseRange;
        halo.range = median + haloBaseRange;

	}

    
}

