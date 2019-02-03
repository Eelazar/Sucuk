using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrblingSpawner : MonoBehaviour {

    [SerializeField]
    private OrblingType trackType;
    [SerializeField]
    private GameObject orblingPrefab;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private float minDestroyDelay;
    [SerializeField]
    private float maxDestroyDelay;


    private GameObject orblingChild;
    private bool occupied;
    private float spawnTimestamp;


    private enum OrblingType { Percussion, Bass, Lead }

    void Start () 
	{
        spawnTimestamp = Time.time + spawnTime;
	}
	
	void Update () 
	{
		if(occupied == false && spawnTimestamp >= Time.time)
        {
            SpawnOrbling();
        }

        if(occupied == true)
        {
            if (orblingChild.GetComponent<Orbling>().activated == true)
            {
                occupied = false;
                spawnTimestamp = Time.time + respawnTime;
            }
        }
	}

    private void SpawnOrbling()
    {
        switch (trackType)
        {
            case OrblingType.Percussion:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.percussionOrblings.Add(orblingChild);

                occupied = true;

                break;
            case OrblingType.Bass:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.percussionOrblings.Add(orblingChild);

                occupied = true;

                break;
            case OrblingType.Lead:
                orblingChild = GameObject.Instantiate<GameObject>(orblingPrefab);
                orblingChild.GetComponent<Orbling>().owner = this.gameObject;
                TrackRegistry.percussionOrblings.Add(orblingChild);

                occupied = true;

                break;
            default:
                break;
        }
    }

    public void Clear()
    {
        if(orblingChild != null)
        {
            Destroy(orblingChild, Random.Range(minDestroyDelay, maxDestroyDelay));
            spawnTimestamp = Time.time + spawnTime;
            occupied = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 6F);
    }
}
