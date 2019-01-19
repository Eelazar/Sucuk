using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarVisualizer : MonoBehaviour {

    public GameObject cubePrefab;
    public float distance;
    public float amplitude;

    private GameObject[] cubeGOArray;

    //Smooth Stuff
    private float velocity;
    public float smoothTime;


    //Multiple of 8
    private int n = 400;
    private float[] spectrum;
    private float[] extendedSpectrum;
    private float[] multipliers;

    private int distanceBetween;
    private int offset;
    private int percentagePerStep;


    void Start ()
    {
        spectrum = new float[8];
        cubeGOArray = new GameObject[n];

        SpawnVisualizer();
        InitializeExtendedSpectrum();
	}
	
	void Update ()
    {
        spectrum = WwiseListener.spectrum;

        CalculateExtendedSpectrum();

        for (int i = 0; i < n; i++)
        {
            //cubeGOArray[i].transform.localScale = new Vector3(1, spectrum[i] * amplitude, 1);

            float yPosition = Mathf.SmoothDamp(cubeGOArray[i].transform.localScale.y, extendedSpectrum[i] * amplitude, ref velocity, smoothTime);

            Vector3 scale = cubeGOArray[i].transform.localScale;
            cubeGOArray[i].transform.localScale = new Vector3(scale.x, yPosition, scale.z);
        }
    }

    void SpawnVisualizer()
    {
        for (int i = 0; i < n; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(cubePrefab);
            go.transform.position = this.transform.position;
            go.transform.parent = this.transform;
            go.name = "Cube Instance " + i;

            this.transform.eulerAngles = new Vector3(0, -1.11111F * i, 0);

            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, distance);
            cubeGOArray[i] = go;
        }
    }

    private void InitializeExtendedSpectrum()
    {
        extendedSpectrum = new float[n];
        multipliers = new float[n];
        distanceBetween = n / 8;
        offset = distanceBetween / 2;
        percentagePerStep = 100 / offset;

        //Distribute Percentages
        int counter = 0;
        bool directionSwitch = false;
        for (int i = 0; i < n; i++)
        {
            //Check whether the percentages need to ascend or descend
            if (directionSwitch == false)
            {
                multipliers[i] = (percentagePerStep * counter) / 100F;
            }
            else if (directionSwitch == true)
            {
                multipliers[i] = (100F - (percentagePerStep * counter)) / 100F;
                Debug.Log(multipliers[i]);
            }

            //If the percentages have reached their maximum / minimum
            if (counter >= offset - 1)
            {
                //Reset the counter
                counter = 0;
                //Change the direction
                directionSwitch = !directionSwitch;
            }
            else
            {
                counter++;
            }
        }
    }

    private void CalculateExtendedSpectrum()
    {
        int counter = 0;
        //For each spectrum value we have
        for (int i = 0; i < 8; i++)
        {
            //Go through the corresponding extended values
            for (int j = 0; j < offset; j++)
            {
                //Multiply the spectrum value with the according percentage
                float multiplier = multipliers[counter + j];
                extendedSpectrum[counter + j] = spectrum[i] * multiplier;
            }

            //Increase the counter when one set of extended values is done
            counter += distanceBetween;
        }
    }

    private void AnimateExtendedSpectrum()
    {

    }

}
