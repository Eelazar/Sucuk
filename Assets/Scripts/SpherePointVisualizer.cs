using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePointVisualizer : MonoBehaviour {

    public float distance;
    public GameObject prefab;
    public float smoothTime;
    public float amplitude;

    private Vector3[] points;
    private float[] spectrum;
    private GameObject[] barArray = new GameObject[8];

    private float velocity;
    

	void Start ()
    {        
        points = PointsOnSphere(8);
        List<GameObject> uspheres = new List<GameObject>();

        spectrum = AudioSpectrumListener.frequencyBand;

        SpawnVisualizer();
    }
	
	void Update ()
    {
		for(int i = 0; i < 8; i++)
        {
            float yPosition = Mathf.SmoothDamp(barArray[i].transform.localScale.y, spectrum[i] * amplitude, ref velocity, smoothTime);

            barArray[i].transform.localScale = new Vector3(prefab.transform.localScale.x, yPosition, prefab.transform.localScale.z);
        }
	}

    private void SpawnVisualizer()
    {
        for(int i = 0; i < points.Length; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(prefab);
            go.transform.parent = transform;
            go.transform.position = points[i] * distance;
            go.transform.rotation = Quaternion.LookRotation(transform.position - go.transform.position);
            go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles + new Vector3(-90, 0, 0));

            barArray[i] = go;
        }
    }

    Vector3[] PointsOnSphere(int n)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = 0;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            upts.Add(new Vector3(x, y, z));
        }
        Vector3[] pts = upts.ToArray();
        return pts;
    }
}
