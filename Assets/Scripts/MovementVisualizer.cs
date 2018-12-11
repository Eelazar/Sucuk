using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVisualizer : MonoBehaviour {

    public Vector3 startPosition;
    public Vector3 endPosition;

    public double bpm;

    public float beatsPerCycle;

    public AnimationCurve animCurve;


    private double cycleDuration;
    private double currentTime;
    private double nextTime;

    //Lerp
    private float lerpT;
    private bool start = false;

	void Start () 
	{
        cycleDuration = (60 / bpm) * beatsPerCycle;

        currentTime = AudioSettings.dspTime;
        nextTime = currentTime + cycleDuration;
    }
	
	void Update () 
	{
        if (AudioSettings.dspTime >= nextTime)
        {
            if (start)
            {
                StopAllCoroutines();
                StartCoroutine(Cycle(endPosition, startPosition));
            }
            else if (!start)
            {
                StopAllCoroutines();
                StartCoroutine(Cycle(startPosition, endPosition));
            }
        }

    }

    private IEnumerator Cycle(Vector3 sPosition, Vector3 ePosition)
    {
        currentTime = nextTime;
        nextTime = currentTime + cycleDuration;
        start = !start;
        lerpT = 0;

        while (lerpT < 1)
        {
            lerpT = (float)((AudioSettings.dspTime - currentTime) / cycleDuration);
            transform.position = Vector3.Lerp(sPosition, ePosition, animCurve.Evaluate(lerpT));
            yield return null;
        }
    }
}
