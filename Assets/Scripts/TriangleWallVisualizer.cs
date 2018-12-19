using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleWallVisualizer : MonoBehaviour {

    const float xConst = 0.01F;
    const float yConst = 0.016F;

    [SerializeField]
    private GameObject trianglePrefab;
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private int gridX;
    [SerializeField]
    private int gridY;    
    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float smoothTime;

    //Private
    private float[] spectrum = new float[8];
    private GameObject[,] triangleArray;
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7 };
    private Vector3 velocity;
    
    private float distanceX;
    private float distanceY;

    void Start () 
	{
        distanceX = xConst * trianglePrefab.transform.localScale.x;
        distanceY = yConst * trianglePrefab.transform.localScale.x;
        Generate();
	}
	
	void Update () 
	{
        spectrum = AudioSpectrumListener.frequencyBand;

        Animate();
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

    void Animate()
    {
        int spectrumIndex = 0;

        for(int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                if (spectrumIndex > 7)
                {
                    spectrumIndex = 0;
                    RandomizeArrayPointers();
                }

                GameObject triangle = triangleArray[i, j];
                Vector3 destination = new Vector3(transform.position.x + spectrum[randomPointers[spectrumIndex]] * amplitude, triangle.transform.position.y, triangle.transform.position.z);

                triangle.transform.position = Vector3.SmoothDamp(triangle.transform.position, destination, ref velocity, smoothTime);

                spectrumIndex++;
            }
        }
        
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
