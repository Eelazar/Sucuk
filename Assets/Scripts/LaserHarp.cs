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
    private Vector3[] stringEnds;
    private bool[] notes = new bool[5];
    private bool[] noteSwitches = new bool[5];

    void Start () 
	{
        CreateHarp();

        AkSoundEngine.PostEvent("HarpD_play", gameObject);
        AkSoundEngine.PostEvent("HarpE_play", gameObject);
        AkSoundEngine.PostEvent("HarpF_play", gameObject);
        AkSoundEngine.PostEvent("HarpA_play", gameObject);
        AkSoundEngine.PostEvent("HarpB_play", gameObject);

        AkSoundEngine.PostEvent("HarpD_stop", gameObject);
        AkSoundEngine.PostEvent("HarpE_stop", gameObject);
        AkSoundEngine.PostEvent("HarpF_stop", gameObject);
        AkSoundEngine.PostEvent("HarpA_stop", gameObject);
        AkSoundEngine.PostEvent("HarpB_stop", gameObject);
    }
	
	void Update () 
	{
        CheckStrings();
        PlaySounds();
	}

    /// <summary>
    /// Creates the harp by spawning prefabs with LineRenderers, rotating them in the correct direction, and setting the LineRenderer node positions
    /// </summary>
    void CreateHarp()
    {
        strings = new GameObject[stringAmount];
        stringDirections = new Vector3[stringAmount];
        stringEnds = new Vector3[stringAmount];
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
            //Save the target position for later
            stringEnds[i] = target;
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
            RaycastHit hit;
            if (Physics.Raycast(strings[i].transform.position, stringDirections[i], out hit, length))
            {
                notes[i] = true;
                strings[i].GetComponent<LineRenderer>().SetPosition(1, hit.point);
            }
            else
            {
                notes[i] = false;
                strings[i].GetComponent<LineRenderer>().SetPosition(1, stringEnds[i]);
            }
        }
    }

    void PlaySounds()
    {
        if(notes[0] == true)
        {
            if(noteSwitches[0] == false)
            {
                AkSoundEngine.PostEvent("HarpD", gameObject);
                noteSwitches[0] = true;
            }            
        }
        else
        {
            AkSoundEngine.PostEvent("HarpD_stop", gameObject);
            noteSwitches[0] = false;
        }

        if (notes[1] == true)
        {
            if (noteSwitches[1] == false)
            {
                AkSoundEngine.PostEvent("HarpE", gameObject);
                noteSwitches[1] = true;
            }
        }
        else
        {
            AkSoundEngine.PostEvent("HarpE_stop", gameObject);
            noteSwitches[1] = false;
        }

        if (notes[2] == true)
        {
            if (noteSwitches[2] == false)
            {
                AkSoundEngine.PostEvent("HarpF", gameObject);
                noteSwitches[2] = true;
            }
        }
        else
        {
            AkSoundEngine.PostEvent("HarpF_stop", gameObject);
            noteSwitches[2] = false;
        }

        if (notes[3] == true)
        {
            if (noteSwitches[3] == false)
            {
                AkSoundEngine.PostEvent("HarpA", gameObject);
                noteSwitches[3] = true;
            }
        }
        else
        {
            AkSoundEngine.PostEvent("HarpA_stop", gameObject);
            noteSwitches[3] = false;
        }

        if (notes[4] == true)
        {
            if (noteSwitches[4] == false)
            {
                AkSoundEngine.PostEvent("HarpB", gameObject);
                noteSwitches[4] = true;
            }
        }
        else
        {
            AkSoundEngine.PostEvent("HarpB", gameObject);
            noteSwitches[4] = false;
        }
    }
}
