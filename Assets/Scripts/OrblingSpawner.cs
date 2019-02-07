using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrblingSpawner : MonoBehaviour {

    [SerializeField]
    private OrblingType trackType;
    [SerializeField]
    private GameObject orblingPrefab;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private float minDestroyDelay;
    [SerializeField]
    private float maxDestroyDelay;


    private GameObject orblingChild;
    private bool occupied;
    private float spawnTimestamp;
    private float startTime;


    private enum OrblingType { Percussion, Bass, Lead, Kick, Chord }

    void Start () 
	{
        startTime = Time.realtimeSinceStartup;
        spawnTimestamp = startTime + TrackRegistry.spawnTimestamps[0];
	}
	
	void Update () 
	{
		if(occupied == false && spawnTimestamp <= Time.time)
        {
            SpawnOrbling();
        }

        if(occupied == true)
        {
            if (orblingChild.GetComponent<Orbling>().activated == true)
            {
                occupied = false;
                spawnTimestamp = Time.realtimeSinceStartup + respawnTime;
            }
        }
	}

    private void SpawnOrbling()
    {
        switch (trackType)
        {
            case OrblingType.Percussion:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab, transform.position, transform.rotation);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.percussionOrblings.Add(orblingChild);

                occupied = true;

                break;
            case OrblingType.Bass:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab, transform.position, transform.rotation);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.percussionOrblings.Add(orblingChild);

                occupied = true;

                break;
            case OrblingType.Lead:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab, transform.position, transform.rotation);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.percussionOrblings.Add(orblingChild);

                occupied = true;

                break;
            case OrblingType.Kick:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab, transform.position, transform.rotation);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.kickOrblings.Add(orblingChild);

                occupied = true;

                break;
            case OrblingType.Chord:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab, transform.position, transform.rotation);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.chordOrblings.Add(orblingChild);

                occupied = true;

                break;
            default:
                break;
        }
    }

    public void Clear(int index)
    {
        if(orblingChild != null)
        {
            orblingChild.GetComponent<Orbling>().RemoveListener();
            Destroy(orblingChild, Random.Range(minDestroyDelay, maxDestroyDelay));            
        }

        spawnTimestamp = startTime + TrackRegistry.spawnTimestamps[index + 1];
        occupied = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3F);
    }
}
