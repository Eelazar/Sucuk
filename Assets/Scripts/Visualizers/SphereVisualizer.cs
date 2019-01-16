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
    private float kick;
    private float mid;
    private float hi;
    private float hi2;

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
        //spectrum = AudioSpectrumListener.spectrum;

        //I'm generating RTCPValues in WWise based on the peak volume of certain tracks. 
        //It's returning values from -48 to 0 currently because it's acting as a peak meter, which is probably not ideal.
        //There are currently four values generated: Kick, Low, Mid and Hi, the .GetRTCPValue function returns them as a float in the "out" section.
        int type = 1;
        AkSoundEngine.GetRTPCValue("Mkick", gameObject, 0, out kick, ref type);
        AkSoundEngine.GetRTPCValue("Fband1", gameObject, 0, out mid, ref type);
        AkSoundEngine.GetRTPCValue("Fband6", gameObject, 0, out hi, ref type);
        AkSoundEngine.GetRTPCValue("Fband7", gameObject, 0, out hi2, ref type);
        // median = Mathf.SmoothDamp(median, AudioSpectrumListener.frequencyBand[0], ref velocity, smoothTime);
        median = (kick+mid/2) * scaleMultiplier;
  
        //median += scaleBase;
        transform.localScale = new Vector3(median, median, median);

        baseLight.range = median + lightBaseRange;
        halo.range = median + haloBaseRange;

	}

    
}

