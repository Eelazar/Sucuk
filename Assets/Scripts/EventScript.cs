using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour {

    [Tooltip("Reference to the orb script")]
    [SerializeField]
    private Orb orbScript;

    [Tooltip("Array of all the timestamps / segment switches")]
    [SerializeField]
    private float[] timestamps = new float[6];

    private int currentSegment;

    void Start () 
	{
	}
	
	void Update () 
	{
        if (Time.timeSinceLevelLoad > timestamps[currentSegment])
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
                //Trigger Breakdown 1 
                orbScript.SwitchSegment("Breakdown 1");
                currentSegment++;
                break;

            case 2:
                //Trigger Drop 1
                orbScript.SwitchSegment("Drop 1");
                currentSegment++;
                break;

            case 3:
                //Trigger Breakdown 2
                orbScript.SwitchSegment("Breakdown 2");
                currentSegment++;
                break;

            case 4:
                //Trigger Drop 2
                orbScript.SwitchSegment("Drop 2");
                currentSegment++;
                break;

            case 5:
                // IDK
                break;

            default:
                Debug.Log("Current segment not recognized");
                break;
        }
    }
}
