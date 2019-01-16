using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBandVisualizer : MonoBehaviour {

    public GameObject cubePrefab;
    public float distance;
    public float amplitude;

    public bool rotate;

    private GameObject[] cubeGOArray = new GameObject[8];
    private float[] frequencyBand = new float[8];

    //Smooth Stuff
    private float velocity;
    public float smoothTime;

    void Start ()
    {
        SpawnVisualizer();
	}
	
	void Update ()
    {
        frequencyBand = AudioSpectrumListener.frequencyBand;

        for(int i = 0; i < 8; i++)
        {
            //cubeGOArray[i].transform.localScale = new Vector3(10, frequencyBand[i] * amplitude, 10);

            float yPosition = Mathf.SmoothDamp(cubeGOArray[i].transform.localScale.y, frequencyBand[i] * amplitude, ref velocity, smoothTime);

            cubeGOArray[i].transform.localScale = new Vector3(10, yPosition, 10);
        }

        if (rotate)
        {
            gameObject.transform.Rotate(new Vector3(0, 0.1F, 0));
        }
	}

    void SpawnVisualizer()
    {
        for(int i = 0; i < 8; i++)
        {
            this.transform.eulerAngles = new Vector3(0, -45 * i, 0);
            GameObject go = GameObject.Instantiate<GameObject>(cubePrefab);
            go.transform.position = this.transform.position;
            go.transform.parent = this.transform;
            go.name = "Cube Instance " + i;
            go.transform.position = Vector3.forward * distance;
            cubeGOArray[i] = go;
        }
    }
}
