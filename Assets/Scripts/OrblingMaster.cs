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

	void Start () 
	{
        spawners = GetComponentsInChildren<OrblingSpawner>();
	}
	
	void LateUpdate () 
	{
        if (breakdownCounter < TrackRegistry.destructionTimestamps.Length)
        {
            if (Time.timeSinceLevelLoad >= TrackRegistry.destructionTimestamps[breakdownCounter])
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
            go.GetComponent<Orbling>().DeleteOrbling();
        }
        foreach (GameObject go in TrackRegistry.bassOrblings)
        {
            go.GetComponent<Orbling>().DeleteOrbling();
        }
        foreach (GameObject go in TrackRegistry.leadOrblings)
        {
            go.GetComponent<Orbling>().DeleteOrbling();
        }
        foreach (GameObject go in TrackRegistry.kickOrblings)
        {
            go.GetComponent<Orbling>().DeleteOrbling();
        }
        foreach (GameObject go in TrackRegistry.chordOrblings)
        {
            go.GetComponent<Orbling>().DeleteOrbling();
        }

        TrackRegistry.percussionOrblings.Clear();
        TrackRegistry.bassOrblings.Clear();
        TrackRegistry.leadOrblings.Clear();
        TrackRegistry.kickOrblings.Clear();
        TrackRegistry.chordOrblings.Clear();
    }
}
