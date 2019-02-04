using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour {

    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private float[] timestamps;

    private int counter;
    private bool activated;
    private bool activatedTrigger;
    private float startTime;

	void Start () 
	{
        startTime = Time.time;
	}
	
	void Update () 
	{
        if (counter < timestamps.Length && Time.time >= startTime + timestamps[counter])
        {
            enabled = !enabled;
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
