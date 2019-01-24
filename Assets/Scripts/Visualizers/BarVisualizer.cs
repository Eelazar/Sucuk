using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarVisualizer : MonoBehaviour {

    #region Editor Variables
    public enum Shape { Circle, Line }
    
    [Tooltip("The GameObject that will be instatiated")]
    [SerializeField]
    private GameObject cubePrefab;
    [Tooltip("Amount of objects spawned. Important: Must be multiple of 8, result must me multiple of 2!")]
    [SerializeField]
    public int n = 400;
    [Tooltip("Higher amplitude means bigger movements")]
    [SerializeField]
    private float amplitude;
    [Tooltip("The curve the visualizer will have")]
    [SerializeField]
    private AnimationCurve curve;
    [Tooltip("Add a random value between -this and this")]
    [SerializeField]
    private float randomRange;
    [Tooltip("Add a base to all values")]
    [SerializeField]
    private float baseScale;
    [Tooltip("Higher values means smoother, but also more restricted movement")]
    [Range(0F, 0.2F)]
    [SerializeField]
    private float smoothTime;
    [Tooltip("The shape of the visualizer")]
    [SerializeField]
    private Shape shape;

    [Header("Circle Visualizer")]
    [Tooltip("The radius of the circle")]
    [SerializeField]
    private float distanceCircle;

    [Header("Line Visualizer")]
    [Tooltip("The distance between objects")]
    [SerializeField]
    private float distanceLine;
    #endregion Editor Variables

    #region Private Variables
    //Reference
    private float velocity;

    //Spectrum Arrays    
    private GameObject[] cubeGOArray;
    private float[] spectrum;
    private float[] extendedSpectrum;
    private float[] multipliers;

    //Calculation variables
    private int distanceBetween;
    private int offset;
    private int percentagePerStep;
    #endregion Private Variables

    void Start ()
    {
        spectrum = new float[8];
        cubeGOArray = new GameObject[n];

        if(shape == Shape.Circle)
        {
            SpawnCircleVisualizer();
        }
        else if (shape == Shape.Line)
        {
            SpawnLineVisualizer();
        }

        InitializeExtendedSpectrum();
	}
	
	void Update ()
    {
        spectrum = WwiseListener.spectrum;

        CalculateExtendedSpectrum();

        AnimateExtendedSpectrum();
    }

    void SpawnCircleVisualizer()
    {
        for (int i = 0; i < n; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(cubePrefab);
            go.transform.position = this.transform.position;
            go.transform.parent = this.transform;
            go.name = "Cube Instance " + i;

            this.transform.eulerAngles = new Vector3(0, -0.9F * i, 0);

            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, distanceCircle);
            cubeGOArray[i] = go;
        }
    }

    void SpawnLineVisualizer()
    {
        for (int i = 0; i < n; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(cubePrefab);
            Vector3 position = this.transform.position + new Vector3(distanceLine * i, 0, 0);
            go.transform.position = position;
            go.transform.parent = this.transform;
            go.name = "Cube Instance " + i;
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
            for (int j = 0; j < distanceBetween; j++)
            {
                //Multiply the spectrum value with the according percentage
                float multiplier = multipliers[counter + j];
                extendedSpectrum[counter + j] = curve.Evaluate(spectrum[i] * multiplier);
            }

            //Increase the counter when one set of extended values is done
            counter += distanceBetween;
        }
    }

    private void AnimateExtendedSpectrum()
    {
        for (int i = 0; i < n; i++)
        {
            //cubeGOArray[i].transform.localScale = new Vector3(1, spectrum[i] * amplitude, 1);

            float yPosition = Mathf.SmoothDamp(cubeGOArray[i].transform.localScale.y, extendedSpectrum[i] * amplitude, ref velocity, smoothTime);

            Vector3 scale = cubeGOArray[i].transform.localScale;
            cubeGOArray[i].transform.localScale = new Vector3(scale.x, yPosition + Random.Range(-randomRange, randomRange) + baseScale, scale.z);
        }
    }

}
