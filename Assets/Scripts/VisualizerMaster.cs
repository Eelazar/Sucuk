using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class VisualizerMaster : MonoBehaviour {

    #region Editor Variables

    [Header("Post Processing Multipliers")]
    [Range(0F, 2F)]
    [SerializeField]
    private float bloomIntensity;

    public PostProcessingProfile profile;
    public float smoothTime;

    #endregion Editor Variables
    
    //////////////////////////////////////////////////////////

    #region Private Variables

    private float[] spectrum = new float[9];

    private BloomModel.Settings bloomSettings;
    private float velocity;

    #endregion Private Variables

    //////////////////////////////////////////////////////////

    void Start () 
	{
        bloomSettings = profile.bloom.settings;
    }
	
	void Update () 
	{
        spectrum = WwiseListener.spectrum;

        bloomSettings.bloom.intensity = Mathf.SmoothDamp(bloomSettings.bloom.intensity, spectrum[8] * bloomIntensity, ref velocity, smoothTime);
        profile.bloom.settings = bloomSettings;
    }
}
