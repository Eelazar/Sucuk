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





    public enum OrblingType { Percussion, Bass, Lead }

    void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    private void SpawnOrbling()
    {
        switch (trackType)
        {
            case OrblingType.Percussion:
                //Spawn

                break;
            case OrblingType.Bass:

                break;
            case OrblingType.Lead:

                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1F);
    }
}
