using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour {

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
    private int gridSize = 4;
    [Tooltip("The distance between each node")]
    [SerializeField]
    private float frequency = 1.5F;

    //Movement
    [Tooltip("This will influence the smoothness of the movement")]
    [SerializeField]
    private float smoothTime;
    [Tooltip("This value will influence how much the y Position will fluctuate")]
    [SerializeField]
    private float amplitude = 3;
    [Tooltip("This will randomize the placement of the frequency bands on the grid")]
    [SerializeField]
    private bool randomize;
    

    //Arrays
    private GameObject[,] nodeArray;
    private Vector3[,] nodeArrayPositionCopy;
    private LineRenderer[] lrArrayI;
    private LineRenderer[] lrArrayJ;
    private float[] spectrum = new float[8];
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7 };

    //Private variables
    private float velocity;


    void Start ()
    {
        //Initialize Variables
        //I found that going above 0.3F created much less smooth fluctuations for some reason
        lrArrayI = new LineRenderer[gridSize];
        lrArrayJ = new LineRenderer[gridSize];

        //Initialize Grid
        InitializeGrid();

        //Initialize Grid Position Copy
        nodeArrayPositionCopy = new Vector3[gridSize, gridSize];
        CopyArray();
    } 

    private void Update()
    {
        spectrum = WwiseListener.spectrum;
    }

    void FixedUpdate ()
    {
        MoveGrid();
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
                node.transform.parent = transform;
                

                //If the node is at the edge of the grid, give it a Line Renderer
                if(j == 0 || i == 0)
                {
                    node.AddComponent<LineRenderer>();
                    node.GetComponent<LineRenderer>().positionCount = gridSize;
                    node.GetComponent<LineRenderer>().material = lineM;
                    node.GetComponent<LineRenderer>().startWidth = 0.4F;
                    node.GetComponent<LineRenderer>().endWidth = 0.4F;

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


    void MoveGrid()
    {
        int spectrumIndex = 0;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (spectrumIndex > 7)
                {
                    spectrumIndex = 0;
                    RandomizeArrayPointers();
                }

                //Move nodes to new position
                Transform t = nodeArray[i, j].transform;
                Vector3 destination = new Vector3(t.position.x, Mathf.SmoothDamp(t.position.y, spectrum[randomPointers[spectrumIndex]] * amplitude, ref velocity, smoothTime), t.position.z);
                t.position = destination;

                spectrumIndex++;

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

    void RandomizeArrayPointers()
    {
        System.Random r = new System.Random();
        for (int i = randomPointers.Length; i > 0; i--)
        {
            int j = r.Next(i);
            int k = randomPointers[j];
            randomPointers[j] = randomPointers[i - 1];
            randomPointers[i - 1] = k;
        }
    }
}
