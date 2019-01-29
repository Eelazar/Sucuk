using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinGrid : MonoBehaviour {

    //Objects
    [Tooltip("The GameObject (prefab) that will be used to create nodes")]
    [SerializeField]
    private GameObject nodeGO;
    [Tooltip("The Material that will be used for the lines connecting the nodes")]
    [SerializeField]
    private Material lineM;

    //Grid Variables
    [Tooltip("The position of the [0,0] node")]
    [SerializeField]
    private Vector3 spawnPosition = Vector3.zero;
    [Tooltip("This value squared will be the amount of nodes")]
    [SerializeField]
    private int gridSize = 10;
    [Tooltip("The distance between each node")]
    [SerializeField]
    private float frequency = 1.5F;
    [Tooltip("This value will influence how much the y Position will fluctuate")]
    [SerializeField]
    private float amplitude = 3;

    //Movement Variables
    [Tooltip("The time it takes for nodes to move to their next position")]
    [SerializeField]
    private float lerpDuration = 2;
    [Tooltip("The curve describing the speed at which nodes will move")]
    [SerializeField]
    private AnimationCurve movementCurve;

    //Visualization
    [Tooltip("The seed ensures that each iteration will be different, since Perlin Noise isn't truly random")]
    [SerializeField]
    private float perlinSeed;

    //Arrays
    private GameObject[,] nodeArray;
    private Vector3[,] nodeArrayPositionCopy;
    private LineRenderer[] lrArrayI;
    private LineRenderer[] lrArrayJ;

    //Private variables
    private int perlinOffset;
    private float lerpStamp;
    private float lerpT;


    void Start ()
    {
        //Initialize Variables
        //I found that going above 0.3F created much less smooth fluctuations for some reason
        perlinSeed = Random.Range(0.1F, 0.2F);
        lrArrayI = new LineRenderer[gridSize];
        lrArrayJ = new LineRenderer[gridSize];

        //Initialize Grid
        InitializeGrid();

        //Initialize Grid Position Copy
        nodeArrayPositionCopy = new Vector3[gridSize, gridSize];
        CopyArray();


        //Initialize Lerp
        lerpT = 0;
        lerpStamp = Time.time;
        perlinOffset++;
    }
	

	void FixedUpdate ()
    {
        //Reset the lerp variables, make a new copy of the node positions, and increase the offset on the perlin noise when a lerp cycle is complete
		if(lerpT >= 1)
        {
            lerpT = 0;
            lerpStamp = Time.time;
            CopyArray();
            perlinOffset++;
        }
        //Move the grid with linear interpolation
        else if (lerpT <= 1)
        {
            MoveGridLerp();
        }
	}

    void InitializeGrid()
    {
        nodeArray = new GameObject[gridSize, gridSize];

        for(int i = 0; i < gridSize; i++)
        {
            for(int j = 0; j < gridSize; j++)
            {
                //Create the nodes at the given frequency
                GameObject node = GameObject.Instantiate<GameObject>(nodeGO);
                node.transform.position = new Vector3(spawnPosition.x + frequency * i, spawnPosition.y, spawnPosition.z + frequency * j);
                node.name = "Node " + i + j;
                

                //If the node is at the edge of the grid, give it a Line Renderer
                if(j == 0 || i == 0)
                {
                    node.AddComponent<LineRenderer>();
                    node.GetComponent<LineRenderer>().positionCount = gridSize;
                    node.GetComponent<LineRenderer>().material = lineM;
                    node.GetComponent<LineRenderer>().startWidth = 0.1F;
                    node.GetComponent<LineRenderer>().endWidth = 0.1F;

                    //Assign the node to the correct LineRenderer-node-rray
                    if (i == 0) lrArrayI[j] = node.GetComponent<LineRenderer>();
                    else if (j == 0) lrArrayJ[i] = node.GetComponent<LineRenderer>();
                }
                 //Assign the node to the node array
                nodeArray[i, j] = node;
            }
        }
        
        //Connect the nodes using LineRenderers
        DrawLines();
    }

    float GetPerlinHeight(int i, int j)
    {
        //Get a value from perlin noise, taking into account the seed (extra randomization) and the offset (movement)
        float perlinValue = Mathf.PerlinNoise(i * perlinSeed + perlinOffset, j * perlinSeed + perlinOffset);
        //Add the amplitude to set the range of possible values
        perlinValue *= amplitude;
        return perlinValue;
    }

    void DrawLines()
    {
        for (int i = 0; i < gridSize; i++)
        {
            //Create a temporary array of all the nodes in the second dimension of the array, for each node with a Line Renderer in the first dimension
            Vector3[] temp = new Vector3[gridSize];
            for(int j = 0; j < gridSize; j++)
            {
                temp[j] = nodeArray[i, j].transform.position;
            }
            nodeArray[i, 0].GetComponent<LineRenderer>().SetPositions(temp);
        }

        for (int i = 0; i < gridSize; i++)
        {
            //Create a temporary array of all the nodes in the first dimension of the array, for each node with a Line Renderer in the second dimension
            Vector3[] temp = new Vector3[gridSize];
            for (int j = 0; j < gridSize; j++)
            {
                temp[j] = nodeArray[j, i].transform.position;
            }
            nodeArray[0, i].GetComponent<LineRenderer>().SetPositions(temp);
        }
    }   


    void MoveGridLerp()
    {
        //Calculate the current lerp time value based on the desired duration
        lerpT = (Time.time - lerpStamp) / lerpDuration;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                //Lerp nodes to new Perlin position
                Transform t = nodeArray[i, j].transform;
                Vector3 destination = new Vector3(t.position.x, spawnPosition.y + GetPerlinHeight(i, j), t.position.z);
                t.position = Vector3.LerpUnclamped(nodeArrayPositionCopy[i, j], destination, movementCurve.Evaluate(lerpT));


                //Update LineRenderers
                if (lrArrayI[i] != null)
                {
                    lrArrayI[i].SetPosition(j, nodeArray[j, i].transform.position);
                }

                if (lrArrayJ[i] != null)
                {
                    lrArrayJ[i].SetPosition(j, nodeArray[i, j].transform.position);
                }
            }
        }
    }

    void CopyArray()
    {
        //Copies the position of all the nodes into a 'backup' array for correct lerping
        if (nodeArray.Length == nodeArrayPositionCopy.Length)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    nodeArrayPositionCopy[i, j] = nodeArray[i, j].transform.position;
                }
            }
        }

        else Debug.Log("Array sizes do not match");
    }
}
