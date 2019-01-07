using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(SphereCollider))]
public class Orb : MonoBehaviour
{
    #region Editor Variables
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
    [Tooltip("Minimum size of the movements, before actual music values are added")]
    [SerializeField]
    private float baseScale;
    [Tooltip("Multiplies the values from the kick for bigger pulses")]
    [SerializeField]
    private float kickScaleMultiplier;
    #endregion Editor Variables

    #region Private Variables
    #region Mesh Variables
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    private Vector3[] originalVertices;
    #endregion Mesh Variables

    #region Visualization Variables
    //Classic
    private float[] eightPointSpectrum = new float[8];
    private Vector3 velocity;
    private int[] spectrumPointers;
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7 };
    //Wwise
    private int type;
    private float[] wwiseSpectrum = new float[9];
    #endregion Visualization Variables

    #region Orbling Processing Variables
    //Collider
    private SphereCollider trigger;
    //TrackID
    private string[] percussionTracks = new string[] { "", "" };
    private string[] bassTracks = new string[] { "", "" };
    private string[] leadTracks = new string[] { "", "" };
    #endregion Orbling Processing Variables

    #endregion Private Variables

    private void Start()
    {
        Generate();
        CopyArray();
        DistributeSpectrumPointers(8);

        trigger = transform.GetComponent<SphereCollider>();
        trigger.isTrigger = true;
    }

    private void Update()
    {
        VisualizeWwise();

        trigger.radius = Vector3.Distance(vertices[0], transform.position);
    }

    #region Orbling Stuff

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Orbling>())
        {
            ProcessOrbling(collider.GetComponent<Orbling>());
        }
    }

    /// <summary>
    /// Triggers the necessary events in Wwise according to the Orbling type, and deletes it afterwards
    /// </summary>
    /// <param name="o">The Orbling instance to be processed</param>
    private void ProcessOrbling(Orbling o)
    {
        if(o.soundType == Orbling.SoundType.Bass)
        {
            ShiftTrack(0, o.soundType);
        }
        else
        {
            ShiftTrack(0, o.soundType);
        }

        Destroy(o.gameObject);
    }

    /// <summary>
    /// Switches the current track out
    /// </summary>
    /// <param name="shiftMode">0 = Replace with track, 1 = Add track</param>
    /// <param name="trackType">The type of track that will be switched</param>
    private void ShiftTrack(int shiftMode, Orbling.SoundType trackType)
    {
        if(shiftMode == 0)
        {
            switch (trackType)
            {
                case Orbling.SoundType.Percussion:
                    break;
                case Orbling.SoundType.Bass:
                    break;
                case Orbling.SoundType.Lead:
                    break;
                default:
                    break;
            }
        }
        else if(shiftMode == 1)
        {

        }
    }

    #endregion Orbling Stuff

    #region Visualization Stuff

    /// <summary>
    /// Gets the spectrum values from Wwise and moves the vertices of the sphere accordingly
    /// </summary>
    private void VisualizeWwise()
    {
        //Get the values from Wwise
        type = 1;
        AkSoundEngine.GetRTPCValue("Fband1", gameObject, 0, out wwiseSpectrum[0], ref type);
        AkSoundEngine.GetRTPCValue("Fband2", gameObject, 0, out wwiseSpectrum[1], ref type);
        AkSoundEngine.GetRTPCValue("Fband3", gameObject, 0, out wwiseSpectrum[2], ref type);
        AkSoundEngine.GetRTPCValue("Fband4", gameObject, 0, out wwiseSpectrum[3], ref type);
        AkSoundEngine.GetRTPCValue("Fband5", gameObject, 0, out wwiseSpectrum[4], ref type);
        AkSoundEngine.GetRTPCValue("Fband6", gameObject, 0, out wwiseSpectrum[5], ref type);
        AkSoundEngine.GetRTPCValue("Fband7", gameObject, 0, out wwiseSpectrum[6], ref type);
        AkSoundEngine.GetRTPCValue("Fband8", gameObject, 0, out wwiseSpectrum[7], ref type);
        AkSoundEngine.GetRTPCValue("Mkick", gameObject, 0, out wwiseSpectrum[8], ref type);

        //Normalize the values to a float between 0 and 1
        for(int i = 0; i < wwiseSpectrum.Length; i++)
        {
            wwiseSpectrum[i] += 48;
            wwiseSpectrum[i] /= 48;
        }
        
        //Move the vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 direction = originalVertices[i].normalized;
            Vector3 destination = Vector3.SmoothDamp(vertices[i], originalVertices[i] + (direction * (baseScale + (kickScaleMultiplier * wwiseSpectrum[8])) + (direction * (amplitude * wwiseSpectrum[spectrumPointers[i]]))), ref velocity, smoothDamp);
            vertices[i] = destination;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    /// <summary>
    /// Fills the SpectrumPointers array with random ints in an equal manner (Sequence repeats after every number in given range was returned once) 
    /// </summary>
    /// <param name="range">The range of the random numbers generated, in this case equal to the spectrum size</param>
    private void DistributeSpectrumPointers(int range)
    {
        spectrumPointers = new int[vertices.Length];

        System.Random r = new System.Random();

        int countdownIndex = range;
        for(int i = 0; i < spectrumPointers.Length; i++)
        {
            if(countdownIndex <= 0)
            {
                countdownIndex = range;
            }
            int randomIndex = r.Next(countdownIndex);
            int number = randomPointers[randomIndex];
            randomPointers[randomIndex] = randomPointers[countdownIndex - 1];
            randomPointers[countdownIndex - 1] = number;

            spectrumPointers[i] = number;

            countdownIndex--;
        }
    }

    /// <summary>
    /// Copies the position of all the vertices into a fixed array to conserve the original values for calculations
    /// </summary>
    private void CopyArray()
    {
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

    #endregion Visualization Stuff

    #region Mesh Stuff

    /// <summary>
    /// Generate the sphere, one vertice at a time
    /// </summary>
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

    #endregion Mesh Stuff
}
