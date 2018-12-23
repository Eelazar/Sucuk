using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Orb : MonoBehaviour
{
    [Header("Mesh Variables")]
    [Tooltip("Higher grid Size means more vertices")]
    [SerializeField]
    private int gridSize;
    [Tooltip("The radius of the sphere")]
    [SerializeField]
    private float radius;

    [Header("Music Visualization")]
    [Tooltip("Higher amplitude means bigger movements")]
    [SerializeField]
    private float amplitude;
    [Tooltip("Higher values means smoother, but also more restricted movement")]
    [Range(0F, 0.2F)]
    [SerializeField]
    private float smoothDamp;

    //Visualization Stuff
    private float[] eightPointSpectrum = new float[8];
    private Vector3 velocity;
    private int[] spectrumPointers;
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7 };

    //Mesh Stuff
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    private Vector3[] originalVertices;

    private void Start()
    {
        Generate();
        CopyArray();
        DistributeSpectrumPointers();
    }

    private void Update()
    {
        eightPointSpectrum = AudioSpectrumListener.frequencyBand;

        VisualizeRawEightPoint();
    }

    private void VisualizeRawEightPoint()
    {
        for(int i = 0; i < vertices.Length; i++)
        {

            Vector3 direction = originalVertices[i].normalized;
            Vector3 destination = Vector3.SmoothDamp(vertices[i], originalVertices[i] + (direction * (eightPointSpectrum[spectrumPointers[i]] * amplitude)), ref velocity, smoothDamp);
            vertices[i] = destination;

            mesh.vertices = vertices;
        }
    }

    void DistributeSpectrumPointers()
    {
        spectrumPointers = new int[vertices.Length];

        System.Random r = new System.Random();

        int countdownIndex = 8;
        for(int i = 0; i < spectrumPointers.Length; i++)
        {
            if(countdownIndex <= 0)
            {
                countdownIndex = 8;
            }
            int randomIndex = r.Next(countdownIndex);
            int number = randomPointers[randomIndex];
            randomPointers[randomIndex] = randomPointers[countdownIndex - 1];
            randomPointers[countdownIndex - 1] = number;

            spectrumPointers[i] = number;

            countdownIndex--;
        }
    }

    void CopyArray()
    {
        //Copies the position of all the vertices into a static array to conserve the original values
        originalVertices = new Vector3[vertices.Length];

        if (vertices.Length == originalVertices.Length)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                originalVertices[i] = vertices[i];
            }
        }

        else Debug.Log("Array sizes do not match");
    }

    #region MeshStuff
    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Orb";

        CreateVertices();
        CreateTriangles();
    }

    private void CreateVertices()
    {
        int cornerVertices = 8;
        int edgeVertices = (gridSize * 3 - 3) * 4;
        int faceVertices = ((gridSize - 1) * (gridSize - 1) * 3) * 2;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
        normals = new Vector3[vertices.Length];

        int v = 0;
        for (int y = 0; y <= gridSize; y++)
        {
            for (int x = 0; x <= gridSize; x++)
            {
                SetVertex(v++, x, y, 0);
            }
            for (int z = 1; z <= gridSize; z++)
            {
                SetVertex(v++, gridSize, y, z);
            }
            for (int x = gridSize - 1; x >= 0; x--)
            {
                SetVertex(v++, x, y, gridSize);
            }
            for (int z = gridSize - 1; z > 0; z--)
            {
                SetVertex(v++, 0, y, z);
            }
        }
        for (int z = 1; z < gridSize; z++)
        {
            for (int x = 1; x < gridSize; x++)
            {
                SetVertex(v++, x, gridSize, z);
            }
        }
        for (int z = 1; z < gridSize; z++)
        {
            for (int x = 1; x < gridSize; x++)
            {
                SetVertex(v++, x, 0, z);
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
    }

    private void CreateTriangles()
    {
        int quads = ((gridSize * gridSize) * 3) * 2;
        int[] triangles = new int[quads * 6];
        int ring = (gridSize + gridSize) * 2;
        int t = 0, v = 0;

        for (int y = 0; y < gridSize; y++, v++)
        {
            for (int q = 0; q < ring - 1; q++, v++)
            {
                t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
            }
            t = SetQuad(triangles, t, v, v - ring + 1, v + ring, v + 1);
        }

        t = CreateTopFace(triangles, t, ring);
        t = CreateBottomFace(triangles, t, ring);

        mesh.triangles = triangles;
    }

    private int CreateTopFace(int[] triangles, int t, int ring)
    {
        int v = ring * gridSize;
        for (int x = 0; x < gridSize - 1; x++, v++)
        {
            t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
        }
        t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);

        int vMin = ring * (gridSize + 1) - 1;
        int vMid = vMin + 1;
        int vMax = v + 2;

        for (int z = 1; z < gridSize - 1; z++, vMin--, vMid++, vMax++)
        {
            t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + gridSize - 1);
            for (int x = 1; x < gridSize - 1; x++, vMid++)
            {
                t = SetQuad(
                    triangles, t,
                    vMid, vMid + 1, vMid + gridSize - 1, vMid + gridSize);
            }
            t = SetQuad(triangles, t, vMid, vMax, vMid + gridSize - 1, vMax + 1);
        }

        int vTop = vMin - 2;
        t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
        for (int x = 1; x < gridSize - 1; x++, vTop--, vMid++)
        {
            t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
        }
        t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);

        return t;
    }

    private int CreateBottomFace(int[] triangles, int t, int ring)
    {
        int v = 1;
        int vMid = vertices.Length - (gridSize - 1) * (gridSize - 1);
        t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
        for (int x = 1; x < gridSize - 1; x++, v++, vMid++)
        {
            t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
        }
        t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

        int vMin = ring - 2;
        vMid -= gridSize - 2;
        int vMax = v + 2;

        for (int z = 1; z < gridSize - 1; z++, vMin--, vMid++, vMax++)
        {
            t = SetQuad(triangles, t, vMin, vMid + gridSize - 1, vMin + 1, vMid);
            for (int x = 1; x < gridSize - 1; x++, vMid++)
            {
                t = SetQuad(
                    triangles, t,
                    vMid + gridSize - 1, vMid + gridSize, vMid, vMid + 1);
            }
            t = SetQuad(triangles, t, vMid + gridSize - 1, vMax + 1, vMid, vMax);
        }

        int vTop = vMin - 1;
        t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
        for (int x = 1; x < gridSize - 1; x++, vTop--, vMid++)
        {
            t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
        }
        t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

        return t;
    }

    private void SetVertex(int i, int x, int y, int z)
    {
        Vector3 v = new Vector3(x, y, z) * 2f / gridSize - Vector3.one;

        float x2 = v.x * v.x;
        float y2 = v.y * v.y;
        float z2 = v.z * v.z;
        Vector3 s;
        s.x = v.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f);
        s.y = v.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f);
        s.z = v.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f);

        normals[i] = v.normalized;
        vertices[i] = normals[i] * radius;
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }
    #endregion MeshStuff
}
