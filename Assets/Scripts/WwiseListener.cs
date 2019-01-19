using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseListener : MonoBehaviour {

    public static float[] spectrum = new float[9];

    private int type;

	void Start () 
	{
		
	}
	
	void Update () 
	{
        //Get the values from Wwise
        type = 1;
        AkSoundEngine.GetRTPCValue("Fband1", gameObject, 0, out spectrum[0], ref type);
        AkSoundEngine.GetRTPCValue("Fband2", gameObject, 0, out spectrum[1], ref type);
        AkSoundEngine.GetRTPCValue("Fband3", gameObject, 0, out spectrum[2], ref type);
        AkSoundEngine.GetRTPCValue("Fband4", gameObject, 0, out spectrum[3], ref type);
        AkSoundEngine.GetRTPCValue("Fband5", gameObject, 0, out spectrum[4], ref type);
        AkSoundEngine.GetRTPCValue("Fband6", gameObject, 0, out spectrum[5], ref type);
        AkSoundEngine.GetRTPCValue("Fband7", gameObject, 0, out spectrum[6], ref type);
        AkSoundEngine.GetRTPCValue("Fband8", gameObject, 0, out spectrum[7], ref type);
        AkSoundEngine.GetRTPCValue("Mkick", gameObject, 0, out spectrum[8], ref type);

        //Normalizes the value to a value between 0 and 1
        for (int i = 0; i < spectrum.Length; i++)
        {
            spectrum[i] += 48F;
            spectrum[i] /= 48F;
        }
    }
}
