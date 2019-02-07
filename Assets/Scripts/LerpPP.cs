using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.PostProcessing;

public class LerpPP : MonoBehaviour {

    [Tooltip("Empty profile used as a medium to display values. Necessary effects must be enabled")]
    [SerializeField]
    private PostProcessingProfile lerpProfile;
    [Tooltip("The profile that will be displayed as soon as the scene starts")]
    [SerializeField]
    private PostProcessingProfile startProfile;
    [Space]
    [Tooltip("The profiles that will be lerped to, at the corresponding timestamp")]
    [SerializeField]
    private PostProcessingProfile[] profiles;
    [Tooltip("The times at which a new profile will be lerped to")]
    [SerializeField]
    private float[] timestamps;
    [Tooltip("The durations of the lerp, corresponding to the timestamps")]
    [SerializeField]
    private float[] lerpDurations;
    

    private int counter;

    //private float startTime;
    private Stopwatch timer = new Stopwatch();
    

    void Start () 
	{
        timer.Start();
        //startTime = Time.time;

        //Kind bad way to set the lerpProfile to the startProfile
        StartCoroutine(LerpToProfile(startProfile, 0.0F));
    }
	
	void Update () 
	{
        //if (counter < timestamps.Length && Time.time >= startTime + timestamps[counter])
        //{
        //    StartCoroutine(LerpToProfile(profiles[counter], lerpDurations[counter]));

        //    counter++;
        //}    

        if (counter < timestamps.Length && timer.ElapsedMilliseconds >= (long)(timestamps[counter] * 1000))
        {
            StartCoroutine(LerpToProfile(profiles[counter], lerpDurations[counter]));

            counter++;
        }
    }

    /// <summary>
    /// Lerps all the values from the current profile to the next one, using the dedicated LerpProfile as a medium so as to not overwrite existing profiles
    /// Profiles do not actually get switched, only values
    /// </summary>
    /// <param name="nextProfile">The Profile that is being lerped to</param>
    /// <param name="duration">The duration of the Lerp</param>
    /// <returns></returns>
    private IEnumerator LerpToProfile(PostProcessingProfile nextProfile, float duration)
    {
        //In the following method "C" stands for "current" and "N" stands for "Next", as in "current profile" and "next profile"

        float t = 0.0F;
        float lerpStart = Time.realtimeSinceStartup;

        #region Data
        /////////
        //Bloom//
        /////////
        BloomModel.Settings bloomSettingsC = lerpProfile.bloom.settings;
        BloomModel.Settings bloomSettingsN = nextProfile.bloom.settings;
        BloomModel.Settings tempBloomSettings = new BloomModel.Settings();

        //Current
        float bloomIntensityC = bloomSettingsC.bloom.intensity;
        float thresholdC = bloomSettingsC.bloom.threshold;
        float softKneeC = bloomSettingsC.bloom.softKnee;
        float radiusC = bloomSettingsC.bloom.radius;
        //Next
        float bloomIntensityN = bloomSettingsN.bloom.intensity;
        float thresholdN = bloomSettingsN.bloom.threshold;
        float softKneeN = bloomSettingsN.bloom.softKnee;
        float radiusN = bloomSettingsN.bloom.radius;

        /////////////////
        //Color Grading//
        /////////////////
        ColorGradingModel.Settings cgSettingsC = lerpProfile.colorGrading.settings;
        ColorGradingModel.Settings cgSettingsN = nextProfile.colorGrading.settings;
        ColorGradingModel.Settings tempcgSettings = new ColorGradingModel.Settings();

        ////Tone-Mapping 
        //Current
        float blackInC = cgSettingsC.tonemapping.neutralBlackIn;
        float whiteInC = cgSettingsC.tonemapping.neutralWhiteIn;
        float blackOutC = cgSettingsC.tonemapping.neutralBlackOut;
        float whiteOutC = cgSettingsC.tonemapping.neutralWhiteOut;
        float whiteLevelC = cgSettingsC.tonemapping.neutralWhiteLevel;
        float whiteClipC = cgSettingsC.tonemapping.neutralWhiteClip;
        //Next
        float blackInN = cgSettingsN.tonemapping.neutralBlackIn;
        float whiteInN = cgSettingsN.tonemapping.neutralWhiteIn;
        float blackOutN = cgSettingsN.tonemapping.neutralBlackOut;
        float whiteOutN = cgSettingsN.tonemapping.neutralWhiteOut;
        float whiteLevelN = cgSettingsN.tonemapping.neutralWhiteLevel;
        float whiteClipN = cgSettingsN.tonemapping.neutralWhiteClip;

        ////Basic
        //Current
        float postExposureC = cgSettingsC.basic.postExposure;
        float temperatureC = cgSettingsC.basic.temperature;
        float tintC = cgSettingsC.basic.tint;
        float hueShiftC = cgSettingsC.basic.hueShift;
        float saturationC = cgSettingsC.basic.saturation;
        float contrastC = cgSettingsC.basic.contrast;
        //Next
        float postExposureN = cgSettingsN.basic.postExposure;
        float temperatureN = cgSettingsN.basic.temperature;
        float tintN = cgSettingsN.basic.tint;
        float hueShiftN = cgSettingsN.basic.hueShift;
        float saturationN = cgSettingsN.basic.saturation;
        float contrastN = cgSettingsN.basic.contrast;

        ////Color Wheels        
        //Current
        float slopeRC = cgSettingsC.colorWheels.log.slope.r;
        float slopeGC = cgSettingsC.colorWheels.log.slope.g;
        float slopeBC = cgSettingsC.colorWheels.log.slope.b;
        float slopeAC = cgSettingsC.colorWheels.log.slope.a;
        float powerRC = cgSettingsC.colorWheels.log.power.r;
        float powerGC = cgSettingsC.colorWheels.log.power.g;
        float powerBC = cgSettingsC.colorWheels.log.power.b;
        float powerAC = cgSettingsC.colorWheels.log.power.a;
        float offsetRC = cgSettingsC.colorWheels.log.offset.r;
        float offsetGC = cgSettingsC.colorWheels.log.offset.g;
        float offsetBC = cgSettingsC.colorWheels.log.offset.b;
        float offsetAC = cgSettingsC.colorWheels.log.offset.a;
        //Next
        float slopeRN = cgSettingsN.colorWheels.log.slope.r;
        float slopeGN = cgSettingsN.colorWheels.log.slope.g;
        float slopeBN = cgSettingsN.colorWheels.log.slope.b;
        float slopeAN = cgSettingsN.colorWheels.log.slope.a;
        float powerRN = cgSettingsN.colorWheels.log.power.r;
        float powerGN = cgSettingsN.colorWheels.log.power.g;
        float powerBN = cgSettingsN.colorWheels.log.power.b;
        float powerAN = cgSettingsN.colorWheels.log.power.a;
        float offsetRN = cgSettingsN.colorWheels.log.offset.r;
        float offsetGN = cgSettingsN.colorWheels.log.offset.g;
        float offsetBN = cgSettingsN.colorWheels.log.offset.b;
        float offsetAN = cgSettingsN.colorWheels.log.offset.a;


        //Non-Lerp
        tempBloomSettings.bloom.antiFlicker = bloomSettingsN.bloom.antiFlicker;

        tempcgSettings.tonemapping.tonemapper = cgSettingsN.tonemapping.tonemapper;
        tempcgSettings.curves = cgSettingsN.curves;
        tempcgSettings.channelMixer = cgSettingsC.channelMixer;

        #endregion Data

        #region Lerp
        //Lerp
        while (t <= 1F)
        {
            t = (Time.time - lerpStart) / duration;

            ////Bloom
            tempBloomSettings.bloom.intensity = Mathf.Lerp(bloomIntensityC, bloomIntensityN, t);
            tempBloomSettings.bloom.threshold = Mathf.Lerp(thresholdC, thresholdN, t);
            tempBloomSettings.bloom.softKnee = Mathf.Lerp(softKneeC, softKneeN, t);
            tempBloomSettings.bloom.radius = Mathf.Lerp(radiusC, radiusN, t);

            lerpProfile.bloom.settings = tempBloomSettings;

            ////Color Grading
            //Tone-Mapping
            tempcgSettings.tonemapping.neutralBlackIn = Mathf.Lerp(blackInC, blackInN, t);
            tempcgSettings.tonemapping.neutralWhiteIn = Mathf.Lerp(whiteInC, whiteInN, t);
            tempcgSettings.tonemapping.neutralBlackOut = Mathf.Lerp(blackOutC, blackOutN, t);
            tempcgSettings.tonemapping.neutralWhiteOut = Mathf.Lerp(whiteOutC, whiteOutN, t);
            tempcgSettings.tonemapping.neutralWhiteLevel = Mathf.Lerp(whiteLevelC, whiteLevelN, t);
            tempcgSettings.tonemapping.neutralWhiteClip = Mathf.Lerp(whiteClipC, whiteClipN, t);
            //Basic
            tempcgSettings.basic.postExposure = Mathf.Lerp(postExposureC, postExposureN, t);
            tempcgSettings.basic.temperature = Mathf.Lerp(temperatureC, temperatureN, t);
            tempcgSettings.basic.tint = Mathf.Lerp(tintC, tintN, t);
            tempcgSettings.basic.hueShift = Mathf.Lerp(hueShiftC, hueShiftN, t);
            tempcgSettings.basic.saturation = Mathf.Lerp(saturationC, saturationN, t);
            tempcgSettings.basic.contrast = Mathf.Lerp(contrastC, contrastN, t);
            //Color Wheels
            tempcgSettings.colorWheels.log.slope.r = Mathf.Lerp(slopeRC, slopeRN, t);
            tempcgSettings.colorWheels.log.slope.g = Mathf.Lerp(slopeGC, slopeGN, t);
            tempcgSettings.colorWheels.log.slope.b = Mathf.Lerp(slopeBC, slopeBN, t);
            tempcgSettings.colorWheels.log.slope.a = Mathf.Lerp(slopeAC, slopeAN, t);
            tempcgSettings.colorWheels.log.power.r = Mathf.Lerp(powerRC, powerRN, t);
            tempcgSettings.colorWheels.log.power.g = Mathf.Lerp(powerGC, powerGN, t);
            tempcgSettings.colorWheels.log.power.b = Mathf.Lerp(powerBC, powerBN, t);
            tempcgSettings.colorWheels.log.power.a = Mathf.Lerp(powerAC, powerAN, t);
            tempcgSettings.colorWheels.log.offset.r = Mathf.Lerp(offsetRC, offsetRN, t);
            tempcgSettings.colorWheels.log.offset.g = Mathf.Lerp(offsetGC, offsetGN, t);
            tempcgSettings.colorWheels.log.offset.b = Mathf.Lerp(offsetBC, offsetBN, t);
            tempcgSettings.colorWheels.log.offset.a = Mathf.Lerp(offsetAC, offsetAN, t);

            lerpProfile.colorGrading.settings = tempcgSettings;
            
            yield return null;
        }
        #endregion Lerp
    }
}
