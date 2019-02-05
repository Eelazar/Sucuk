using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour {

    [Tooltip("Reference to the orb script")]
    [SerializeField]
    private Orb orbScript;
    

    private int currentSegment;

    void Start () 
	{
        
	}
	
	void Update () 
	{
        if (Time.timeSinceLevelLoad > TrackRegistry.spawnTimestamps[currentSegment])
        {
            TriggerSegment();
        }
	}

    private void TriggerSegment()
    {
        switch (currentSegment)
        {
            case 0:
                //Trigger Intro
                orbScript.SwitchSegment("Intro");
                currentSegment++;
                break;

            case 1:
                //Trigger Drop 1 
                orbScript.SwitchSegment("Drop 1");
                currentSegment++;
                break;

            case 2:
                //Trigger Drop 2
                orbScript.SwitchSegment("Drop 2");
                currentSegment++;
                break;

            default:
                Debug.Log("Current segment not recognized");
                break;
        }
    }
}
