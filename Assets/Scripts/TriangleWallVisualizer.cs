using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleWallVisualizer : MonoBehaviour {

    //Constants that provides the ideal distance so that the objects are correctly spaced
    const float xConst = 0.01F;
    const float yConst = 0.016F;

    #region Editor Variables
    [Tooltip("The prefab that will be instantiated for each triangle")]
    [SerializeField]
    private GameObject trianglePrefab;
    [Tooltip("The materials used on the triangles, will be randomly selected")]
    [SerializeField]
    private Material[] materials;
    [Tooltip("The amount of triangles to be spawned horizontally")]
    [SerializeField]
    private int gridX;
    [Tooltip("The amount of triangles to be spawned vertically")]
    [SerializeField]
    private int gridY;
    [Tooltip("Higher amplitude means bigger movements")]
    [SerializeField]
    private float amplitude;
    [Tooltip("Higher values means smoother, but also more restricted movement")]
    [Range(0F, 0.2F)]
    [SerializeField]
    private float smoothTime;
    #endregion Editor Variables

    #region Private Variables
    private GameObject[,] triangleArray;
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7, 8};
    private int[,] spectrumPointers;
    private Vector3 velocity;

    private float distanceX;
    private float distanceY;
    #endregion Private Variables

    void Start () 
	{
        distanceX = xConst * trianglePrefab.transform.localScale.x;
        distanceY = yConst * trianglePrefab.transform.localScale.x;
        Generate();

        DistributeSpectrumPointers(9);
	}
	
	void Update () 
	{
        VisualizeWwise();
	}

    void Generate()
    {
        triangleArray = new GameObject[gridX, gridY];
        Vector3 origin = transform.position;

        for(int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                GameObject go = Instantiate<GameObject>(trianglePrefab);
                go.transform.parent = transform;
                Vector3 position = new Vector3(0, j * distanceY, i * distanceX);
                go.transform.position = origin + position;
                go.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
                
                if(j % 2 == 0)
                {
                    if (i % 2 == 0)
                    {
                        go.transform.Rotate(new Vector3(0, 90, 180));
                    }
                    else go.transform.Rotate(new Vector3(0, 90, 0));
                }
                else if (i % 2 != 0)
                {
                    go.transform.Rotate(new Vector3(0, 90, 180));
                }
                else go.transform.Rotate(new Vector3(0, 90, 0));


                triangleArray[i, j] = go;
            }
        }
    }

    private void VisualizeWwise()
    {
        //Move the vertices
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                GameObject triangle = triangleArray[i, j];
                Vector3 destination = new Vector3(transform.position.x + WwiseListener.spectrum[spectrumPointers[i, j]] * amplitude, triangle.transform.position.y, triangle.transform.position.z);

                triangle.transform.position = Vector3.SmoothDamp(triangle.transform.position, destination, ref velocity, smoothTime);
            }
        }
    }    

    void DistributeSpectrumPointers(int spectrumSize)
    {
        spectrumPointers = new int[triangleArray.GetLength(0), triangleArray.GetLength(1)];

        System.Random r = new System.Random();

        int countdownIndex = spectrumSize;
        for (int i = 0; i < spectrumPointers.GetLength(0); i++)
        {
            for(int j = 0; j < spectrumPointers.GetLength(1); j++)
            {
                if (countdownIndex <= 0)
                {
                    countdownIndex = spectrumSize;
                }
                int randomIndex = r.Next(countdownIndex);
                int number = randomPointers[randomIndex];
                randomPointers[randomIndex] = randomPointers[countdownIndex - 1];
                randomPointers[countdownIndex - 1] = number;

                spectrumPointers[i, j] = number;

                countdownIndex--;
            }
        }        
    }
}
