using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class VisualizerMaster : MonoBehaviour
{

    #region Editor Variables

    [Header("Post Processing")]
    [SerializeField]
    private PostProcessingProfile profile;
    [SerializeField]
    private float bloomIntensity;
    [SerializeField]
    private float smoothBloomM;
    [SerializeField]
    private float[] bloomTimestamps;

    [Header("Materials")]
    [SerializeField]
    private Material holoMaterial;
    [SerializeField]
    private float holoRimPowerM;
    [SerializeField]
    private float smoothRimPowermM;
    [SerializeField]
    private float[] rimPowerTimestamps;

    [Header("Lights")]
    [SerializeField]
    private Light[] lights;
    [SerializeField]
    private float[] spotAnglesM;
    [SerializeField]
    private float[] minAngles;
    [SerializeField]
    private float[] maxAngles;
    [SerializeField]
    private float[][] spotAnglesTimestamps;
    [Space]
    [SerializeField]
    private float[] intensityM;
    [SerializeField]
    private float[] minIntensity;
    [SerializeField]
    private float[] maxIntensity;
    [SerializeField]
    private float[][] intensityTimestamps;
    [SerializeField]
    private float smoothSpotAnglesM;
    [SerializeField]
    private float smoothIntensityM;

    #endregion Editor Variables

    //////////////////////////////////////////////////////////

    #region Private Variables

    private float[] spectrum = new float[9];

    private BloomModel.Settings bloomSettings;

    private float velocity;

    private bool bloomSwitch;
    private bool rimPowerSwitch;
    private bool spotAnglesSwitch;
    private bool intensitySwitch;

    private int bloomCounter;
    private int rimPowerCounter;
    private int[] spotAnglesCounters;
    private int[] intensityCounters;

    private float startTime;

    #endregion Private Variables

    //////////////////////////////////////////////////////////

    void Start()
    {
        startTime = Time.time;

        bloomSettings = profile.bloom.settings;

        spotAnglesCounters = new int[lights.Length];
        intensityCounters = new int[lights.Length];
    }

    void Update()
    {
        spectrum = WwiseListener.spectrum;

        //Post-processing
        if (Time.time >= startTime + bloomTimestamps[bloomCounter])
        {
            bloomSwitch = !bloomSwitch;
            bloomCounter++;
        }
        //Materials
        if (Time.time >= startTime + rimPowerTimestamps[rimPowerCounter])
        {
            rimPowerSwitch = !rimPowerSwitch;
            rimPowerCounter++;
        }
        //Lights
        for (int i = 0; i < lights.Length; i++)
        {
            if (Time.time >= startTime + spotAnglesTimestamps[i][spotAnglesCounters[i]])
            {
                spotAnglesSwitch = !spotAnglesSwitch;
                spotAnglesCounters[i]++;
            }
            if (Time.time >= startTime + intensityTimestamps[i][intensityCounters[i]])
            {
                intensitySwitch = !intensitySwitch;
                intensityCounters[i]++;
            }
        }

        //Post-processing
        if (bloomSwitch)
        {
            bloomSettings.bloom.intensity = Mathf.SmoothDamp(bloomSettings.bloom.intensity, spectrum[8] * bloomIntensity, ref velocity, smoothBloomM);
            profile.bloom.settings = bloomSettings;
        }

        //Materials
        if (rimPowerSwitch)
        {
            holoMaterial.SetFloat("_RimPower", Mathf.SmoothDamp(holoMaterial.GetFloat("_RimPower"), 1.3F + (spectrum[8] * holoRimPowerM), ref velocity, smoothRimPowermM));
        }

        //Lights
        if (spotAnglesSwitch)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                float targetValueAngle = Mathf.Clamp(spectrum[8] * spotAnglesM[i], minAngles[i], maxAngles[i]);
                lights[i].spotAngle = Mathf.SmoothDamp(lights[i].spotAngle, targetValueAngle, ref velocity, smoothSpotAnglesM);
            }
        }
        if (intensitySwitch)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                float targetValueIntensity = Mathf.Clamp(spectrum[8] * intensityM[i], minIntensity[i], maxIntensity[i]);
                lights[i].intensity = Mathf.SmoothDamp(lights[i].intensity, targetValueIntensity, ref velocity, smoothIntensityM);
            }
        }
        


    }
}
