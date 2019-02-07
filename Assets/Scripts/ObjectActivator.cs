using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ObjectActivator : MonoBehaviour {

    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private float[] timestamps;
    [SerializeField]
    private GameObject timeMaster;

    private int counter;
    private bool activated;
    private bool activatedTrigger;
    private float startTime;

    private Stopwatch timer = new Stopwatch();

    void Start () 
	{
        timer.Start();

        startTime = Time.time;
	}
	
	void Update () 
	{

        TimeSpan diff = DateTime.Now - timeMaster.GetComponent<TimeMaster>().startTime;

        if (counter < timestamps.Length && diff.TotalSeconds >= timestamps[counter])
        {
            activated = !activated;
            activatedTrigger = true;
            counter++;
        }

        if(targetObject.GetComponent<ParticleSystem>() == true)
        {
            if (activated && activatedTrigger)
            {
                targetObject.GetComponent<ParticleSystem>().Play();
                activatedTrigger = false;
            }
            else if (!activated && activatedTrigger)
            {
                targetObject.GetComponent<ParticleSystem>().Pause();
                activatedTrigger = false;
            }
        }
        else
        {
            if (activated && activatedTrigger)
            {
                targetObject.SetActive(true);
                activatedTrigger = false;
            }
            else if (!activated && activatedTrigger)
            {
                targetObject.SetActive(false);
                activatedTrigger = false;
            }
        }
    }
}
