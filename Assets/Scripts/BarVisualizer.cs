using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarVisualizer : MonoBehaviour {

    public GameObject cubePrefab;
    public float distance;
    public float amplitude;

    private GameObject[] cubeGOArray;
    private float[] spectrum;
    private int spectrumRange;

	void Start ()
    {
        spectrumRange = AudioSpectrumListener.spectrum.Length;
        spectrum = new float[spectrumRange];
        cubeGOArray = new GameObject[spectrumRange];

        SpawnVisualizer();
	}
	
	void Update ()
    {
        spectrum = AudioSpectrumListener.spectrum;

	    for(int i = 0; i < spectrumRange; i++)
        {
            cubeGOArray[i].transform.localScale = new Vector3(1, spectrum[i] * amplitude, 1);
        }	
	}

    void SpawnVisualizer()
    {
        for (int i = 0; i < spectrumRange; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(cubePrefab);
            go.transform.position = this.transform.position;
            go.transform.parent = this.transform;
            go.name = "Cube Instance " + i;

            switch (AudioSpectrumListener.staticSpectrumRange)
            {
                case AudioSpectrumListener.SpectrumRange.High:
                    this.transform.eulerAngles = new Vector3(0, -0.703125F * i, 0);
                    break;
                case AudioSpectrumListener.SpectrumRange.Mid:
                    this.transform.eulerAngles = new Vector3(0, -1.40625F * i, 0);
                    break;
                case AudioSpectrumListener.SpectrumRange.Low:
                    this.transform.eulerAngles = new Vector3(0, -2.8125F * i, 0);
                    break;
                default:
                    this.transform.eulerAngles = new Vector3(0, -0.703125F * i, 0);
                    break;
            }
            
            go.transform.position = Vector3.forward * distance;
            cubeGOArray[i] = go;
        }
    }
}
