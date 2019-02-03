using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrblingMaster : MonoBehaviour {

    [SerializeField]
    private GameObject[] spawners;
    [SerializeField]
    private float minDestroyDelay;
    [SerializeField]
    private float maxDestroyDelay;

    private int breakdownCounter;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if(Time.timeSinceLevelLoad >= TrackRegistry.destructionTimestamps[breakdownCounter])
        {
            foreach(GameObject go in spawners)
            {
                go.GetComponent<OrblingSpawner>().Clear();
            }

            DeleteOrblings();

            breakdownCounter++;
        }
	}

    public void DeleteOrblings()
    {
        foreach(GameObject go in TrackRegistry.percussionOrblings)
        {
            Destroy(go, Random.Range(minDestroyDelay, maxDestroyDelay));
        }
        foreach (GameObject go in TrackRegistry.bassOrblings)
        {
            Destroy(go, Random.Range(minDestroyDelay, maxDestroyDelay));
        }
        foreach (GameObject go in TrackRegistry.leadOrblings)
        {
            Destroy(go, Random.Range(minDestroyDelay, maxDestroyDelay));
        }

        TrackRegistry.percussionOrblings.Clear();
        TrackRegistry.bassOrblings.Clear();
        TrackRegistry.leadOrblings.Clear();
    }
}
