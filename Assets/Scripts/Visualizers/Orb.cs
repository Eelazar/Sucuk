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
    //Wwise
    private Vector3 velocity;
    private int[] spectrumPointers;
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7 };
    #endregion Visualization Variables

    #region Orbling Processing Variables
    //Collider
    private SphereCollider trigger;

    //Track Management
    private string[] currentPercussionTracks = new string[1];
    private string[] currentBassTracks = new string[1];
    private string[] currentLeadTracks = new string[1];
    private string[] currentKickTracks = new string[1];
    private string[] currentChordTracks = new string[1];

    private int bassTrackCounter;
    private int percussionTrackCounter;
    private int leadTrackCounter;
    private int kickTrackCounter;
    private int chordTrackCounter;
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

        trigger.radius = Vector3.Distance(vertices[0], Vector3.zero);
    }

    #region Orbling Stuff

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Orbling>())
        {
            ProcessOrbling(collider.GetComponent<Orbling>());
            AkSoundEngine.PostEvent("Orbling_hit", gameObject);
        }
    }

    /// <summary>
    /// Triggers the necessary events in Wwise according to the Orbling type, and deletes it afterwards
    /// </summary>
    /// <param name="o">The Orbling instance to be processed</param>
    private void ProcessOrbling(Orbling o)
    {
        switch (o.trackType)
        {
            case Orbling.TrackType.Percussion:
                AkSoundEngine.SetState(currentPercussionTracks[percussionTrackCounter], "off");
                IncreaseTrackCounter(percussionTrackCounter, currentPercussionTracks.Length);
                AkSoundEngine.SetState(GetRandomTrack(TrackRegistry.percussionTracks, currentPercussionTracks, percussionTrackCounter), "on");
                break;
            case Orbling.TrackType.Bass:
                AkSoundEngine.SetState(currentBassTracks[bassTrackCounter], "off");
                IncreaseTrackCounter(bassTrackCounter, currentBassTracks.Length);
                string bassTrack = GetRandomTrack(TrackRegistry.bassTracks, currentBassTracks, bassTrackCounter);
                AkSoundEngine.SetState(bassTrack, "on");

                //dynamic track conditions
                if (bassTrack == "d1_bass02")
                { 
                    AkSoundEngine.SetState("d1_lead01", "off");
                    AkSoundEngine.SetState("d1_lead03", "off");
                }
                else if (bassTrack == "d1_bass03")
                      AkSoundEngine.SetState("d1_lead01", "off");
                else if (bassTrack == "d1_bass04")
                    AkSoundEngine.SetState("d1_lead01", "off");

                if (bassTrack == "d2bass03")
                {
                    AkSoundEngine.SetState(currentPercussionTracks[percussionTrackCounter], "off");
                    AkSoundEngine.SetState(currentKickTracks[kickTrackCounter], "off");
                }

                break;
            case Orbling.TrackType.Lead:
                AkSoundEngine.SetState(currentLeadTracks[leadTrackCounter], "off");
                IncreaseTrackCounter(leadTrackCounter, currentLeadTracks.Length);
                string leadTrack = GetRandomTrack(TrackRegistry.leadTracks, currentLeadTracks, leadTrackCounter);
                AkSoundEngine.SetState(leadTrack, "on");

                //dynamic track conditions
                if (leadTrack == "d1_lead01")
                {
                    AkSoundEngine.SetState(currentBassTracks[bassTrackCounter], "off");
                    AkSoundEngine.SetState("d1_bass01", "on");
                }

                break;
            case Orbling.TrackType.Kick:
                AkSoundEngine.SetState(currentKickTracks[kickTrackCounter], "off");
                IncreaseTrackCounter(kickTrackCounter, currentKickTracks.Length);
                AkSoundEngine.SetState(GetRandomTrack(TrackRegistry.kickTracks, currentKickTracks, kickTrackCounter), "on");
                break;
            case Orbling.TrackType.Chord:
                AkSoundEngine.SetState(currentChordTracks[chordTrackCounter], "off");
                IncreaseTrackCounter(chordTrackCounter, currentChordTracks.Length);
                string chordTrack = (GetRandomTrack(TrackRegistry.chordTracks, currentChordTracks, chordTrackCounter));
                AkSoundEngine.SetState(chordTrack, "on");

                //dynamic track conditions
                if (chordTrack == "intro_chords02")
                {
                    AkSoundEngine.SetState("intro_lead01", "off");
                }

                break;
            default:
                break;
        }

        GameObject go = GameObject.Instantiate<GameObject>(o.particlePrefab, o.transform.position, o.transform.rotation);
        Destroy(go, o.particleDestroyTime);

        o.RemoveListener();
        Destroy(o.gameObject);
    }

    /// <summary>
    /// Either increases the track counter, or resets it if it has reached the end of the current track list size
    /// </summary>
    /// <param name="trackCounter">The track counter to be increased</param>
    /// <param name="arrayLength">The length of the corresponding current track list</param>
    private void IncreaseTrackCounter(int trackCounter, int arrayLength)
    {
        if(trackCounter >= arrayLength)
        {
            trackCounter = 0;
        }
        else
        {
            trackCounter++;
        }
    }

    /// <summary>
    /// Gets a random track that is not already playing
    /// </summary>
    /// <param name="trackList">The track list from which to pick a random track</param>
    /// <param name="currentTrackList">The corresponding list of currently playing tracks to exclude</param>
    /// <param name="trackCounter">The corresponding track counter</param>
    /// <returns></returns>
    private string GetRandomTrack(List<string> trackList, string[] currentTrackList, int trackCounter)
    {
        string track;
        
        //If there is no track currently playing
        if(currentTrackList[trackCounter] == null)
        {
            //Get a random track from the list
            track = trackList[UnityEngine.Random.Range(0, trackList.Count)];
            //Remove the chosen track from the list
            trackList.Remove(track);
            //Set the chosen track as the current track
            currentTrackList[trackCounter] = track;

            return track;
        }
        //If there IS a track currently playing 
        else
        {
            //Save the track that is playing
            string trackToSwitch = currentTrackList[trackCounter];
            //Get a random track for the list
            track = trackList[UnityEngine.Random.Range(0, trackList.Count)];
            //Remove the chosen track from the list
            trackList.Remove(track);
            //Add the track that is playing back to the list
            trackList.Add(trackToSwitch);
            //Set the chosen track as the current track
            currentTrackList[trackCounter] = track;

            return track;
        }     
    }

    /// <summary>
    /// Switches off every track, lists need to be manually added to trackListList
    /// </summary>
    //private void TurnOffAllTracks()
    //{
    //    List<List<string>> trackListList = new List<List<string>> { TrackRegistry.percussionTracks, TrackRegistry.bassTracks, TrackRegistry.leadTracks, TrackRegistry.kickTracks, TrackRegistry.chordTracks };

    //    foreach(List<string> l in trackListList)
    //    {
    //        foreach(string s in l)
    //        {
    //            AkSoundEngine.SetState(s, "off");
    //        }
    //    }
    //}

    /// <summary>
    /// Clears all the "currently playing tracks" arrays
    /// </summary>
    private void ClearTrackArrays()
    {
        Array.Clear(currentBassTracks, 0, currentBassTracks.Length);
        Array.Clear(currentPercussionTracks, 0, currentPercussionTracks.Length);
        Array.Clear(currentLeadTracks, 0, currentLeadTracks.Length);
        Array.Clear(currentKickTracks, 0, currentKickTracks.Length);
        Array.Clear(currentChordTracks, 0, currentChordTracks.Length);
    }

    /// <summary>
    /// Replaces the tracklist and clears everything when the segment ist switched
    /// </summary>
    /// <param name="segment">The name of the segment that is being switched to</param>
    public void SwitchSegment(string segment)
    {
        switch (segment)
        {
            case "Intro":
                TrackRegistry.bassTracks = TrackRegistry.bassTracksIntro;
                TrackRegistry.percussionTracks = TrackRegistry.percussionTracksIntro;
                TrackRegistry.leadTracks = TrackRegistry.leadTracksIntro;
                TrackRegistry.kickTracks = TrackRegistry.kickTracksIntro;
                TrackRegistry.chordTracks = TrackRegistry.chordTracksIntro;

                
                ClearTrackArrays();
                break;

            case "Drop 1":
                TrackRegistry.bassTracks = TrackRegistry.bassTracksDrop1;
                TrackRegistry.percussionTracks = TrackRegistry.percussionTracksDrop1;
                TrackRegistry.leadTracks = TrackRegistry.leadTracksDrop1;
                TrackRegistry.kickTracks = TrackRegistry.kickTracksDrop;
                TrackRegistry.chordTracks = TrackRegistry.chordTracksDrop;

               
                ClearTrackArrays();
                break;

            case "Drop 2":
                TrackRegistry.bassTracks = TrackRegistry.bassTracksDrop2;
                TrackRegistry.percussionTracks = TrackRegistry.percussionTracksDrop2;
                TrackRegistry.leadTracks = TrackRegistry.leadTracksDrop2;
                TrackRegistry.kickTracks = TrackRegistry.kickTracksDrop2;
                TrackRegistry.chordTracks = TrackRegistry.chordTracksDrop2;

              
                ClearTrackArrays();
                break;

            default:
                Debug.Log("Current segment not recognized");
                break;
        }
    }

    #endregion Orbling Stuff

    #region Visualization Stuff

    /// <summary>
    /// Gets the spectrum values from Wwise and moves the vertices of the sphere accordingly
    /// </summary>
    private void VisualizeWwise()
    {
        //Move the vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 direction = originalVertices[i].normalized;
            Vector3 destination = Vector3.SmoothDamp(vertices[i], originalVertices[i] + (direction * (baseScale + (kickScaleMultiplier * WwiseListener.spectrum[8])) + (direction * (amplitude * WwiseListener.spectrum[spectrumPointers[i]]))), ref velocity, smoothDamp);
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
