using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHarp : MonoBehaviour {

    [SerializeField]
    private GameObject stringPrefab;
    [SerializeField]
    private int stringAmount;
    [SerializeField]
    private float arcWidth;
    [SerializeField]
    private float arcHeight;

    private GameObject[] strings;
    private Vector3[] stringDirections;

	void Start () 
	{
        CreateHarp();
	}
	
	void Update () 
	{
        CheckStrings();
	}

    /// <summary>
    /// Creates the harp by spawning prefabs with LineRenderers, rotating them in the correct direction, and setting the LineRenderer node positions
    /// </summary>
    void CreateHarp()
    {
        strings = new GameObject[stringAmount];
        stringDirections = new Vector3[stringAmount];
        //Calculate the distance between strings
        float distance = arcWidth / stringAmount;

        for(int i = 0; i < strings.Length; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(stringPrefab);
            go.name = "String " + (i + 1);
            go.transform.parent = transform;
            go.transform.localPosition= Vector3.zero;
            
            //Set the first node of the lineRenderer 
            LineRenderer lr = go.GetComponent<LineRenderer>();
            lr.SetPosition(0, transform.position);            

            //Target.xPosition = (leftmost position of the arc) + (offset of half the distance (because the count starts at 0)) + (distance * index)
            float x = (-(arcWidth / 2)) + (distance /2 ) + (distance * i);
            //Target.yPosition = Height of the arc
            Vector3 target = new Vector3(x, arcHeight, 0);
            //Add the position of the parent object
            target += transform.position;
            //Calculate the direction of the rotation
            Vector3 targetDirection = target - go.transform.position;
            targetDirection.Normalize();
            //Rotate the string
            go.transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

            //Set the second node of the lineRenderer 
            lr.SetPosition(1, target);

            strings[i] = go;
            stringDirections[i] = targetDirection;
        }
    }

    /// <summary>
    /// Checks for hits along the strings of the harp, using the LineRenderers so as to stay true to what the player sees.
    /// </summary>
    void CheckStrings()
    {
        for(int i = 0; i < strings.Length; i++)
        {
            float length = Vector3.Distance(strings[i].transform.position, strings[i].GetComponent<LineRenderer>().GetPosition(1));
            if (Physics.Raycast(strings[i].transform.position, stringDirections[i], length))
            {
                Debug.Log("Hit at string " + i);
            }
        }
    }
}
