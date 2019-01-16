using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGrid : MonoBehaviour {

    //Serialized
    [SerializeField]
    private int xSize;
    [SerializeField]
    private int ySize;
    [SerializeField]
    private float nodeDistance;
    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float smoothTime;
    
    //Private
    private Vector3[] vertices;
    private Mesh mesh;
    private float[] spectrum = new float[8];
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7 };

    private float velocity;


    void Awake()
    {
        StartCoroutine(Generate());
    }

    void Start () 
	{

    }

    void Update () 
	{
        spectrum = AudioSpectrumListener.frequencyBand;

        Move();
	}

    void Move()
    {
        int counter = 0;
        int spectrumIndex = 0;

        
        for (int i = 0; i < vertices.Length; i++)
        {
            //if ((counter % 2) == 0)
            //{
            //    counter++;
            //    continue;
            //}

            if (spectrumIndex > 7)
            {
                spectrumIndex = 0;
                RandomizeArrayPointers();
            }

            Vector3 v = vertices[i];
            Vector3 destination = new Vector3(v.x, v.y, Mathf.SmoothDamp(v.z, - spectrum[randomPointers[spectrumIndex]] * amplitude, ref velocity, smoothTime));
            vertices[i] = destination;

            mesh.vertices = vertices;
            mesh.RecalculateNormals();

            counter++;
            spectrumIndex++;
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

    private IEnumerator Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x * nodeDistance, y * nodeDistance);
            }
        }

        mesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {

                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;

                mesh.triangles = triangles;
            }
        }
        mesh.RecalculateNormals();

        yield return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
#endif
}
