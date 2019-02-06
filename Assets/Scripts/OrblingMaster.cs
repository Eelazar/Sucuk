using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrblingMaster : MonoBehaviour {
    
    [SerializeField]
    private float minDestroyDelay;
    [SerializeField]
    private float maxDestroyDelay;

    private OrblingSpawner[] spawners;
    private int breakdownCounter;
    private float startTime;

    void Start () 
	{
        spawners = GetComponentsInChildren<OrblingSpawner>();
        startTime = Time.time;
	}
	
	void LateUpdate () 
	{
        if (breakdownCounter < TrackRegistry.destructionTimestamps.Length)
        {
            if (Time.time - startTime >= TrackRegistry.destructionTimestamps[breakdownCounter])
            {
                foreach (OrblingSpawner spawner in spawners)
                {
                    spawner.Clear(breakdownCounter);
                }

                DeleteOrblings();

                breakdownCounter++;
            }
        }
	}

    public void DeleteOrblings()
    {
        foreach(GameObject go in TrackRegistry.percussionOrblings)
        {
            if(go != null)
            {
                go.GetComponent<Orbling>().DeleteOrbling();
            }            
        }
        foreach (GameObject go in TrackRegistry.bassOrblings)
        {
            if (go != null)
            {
                go.GetComponent<Orbling>().DeleteOrbling();
            }
        }
        foreach (GameObject go in TrackRegistry.leadOrblings)
        {
            if (go != null)
            {
                go.GetComponent<Orbling>().DeleteOrbling();
            }
        }
        foreach (GameObject go in TrackRegistry.kickOrblings)
        {
            if (go != null)
            {
                go.GetComponent<Orbling>().DeleteOrbling();
            }
        }
        foreach (GameObject go in TrackRegistry.chordOrblings)
        {
            if (go != null)
            {
                go.GetComponent<Orbling>().DeleteOrbling();
            }
        }

        TrackRegistry.percussionOrblings.Clear();
        TrackRegistry.bassOrblings.Clear();
        TrackRegistry.leadOrblings.Clear();
        TrackRegistry.kickOrblings.Clear();
        TrackRegistry.chordOrblings.Clear();
    }
}
