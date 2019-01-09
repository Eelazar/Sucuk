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
    private int[] randomPointers = { 0, 1, 2, 3, 4, 5, 6, 7, 8};
    private int[,] spectrumPointers;
    private Vector3 velocity;

    //Wwise
    private int type;
    private float[] wwiseSpectrum = new float[9];

    private float distanceX;
    private float distanceY;

    void Start () 
	{
        distanceX = xConst * trianglePrefab.transform.localScale.x;
        distanceY = yConst * trianglePrefab.transform.localScale.x;
        Generate();

        DistributeSpectrumPointers(9);
	}
	
	void Update () 
	{
        spectrum = AudioSpectrumListener.frequencyBand;

        //Animate();
        VisualizeWwise();
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

        //Normalizes the value to a value between 0 and 1
        for (int i = 0; i < wwiseSpectrum.Length; i++)
        {
            wwiseSpectrum[i] += 48;
            wwiseSpectrum[i] /= 48;
        }

        //Move the vertices
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                GameObject triangle = triangleArray[i, j];
                Vector3 destination = new Vector3(transform.position.x + wwiseSpectrum[spectrumPointers[i, j]] * amplitude, triangle.transform.position.y, triangle.transform.position.z);

                triangle.transform.position = Vector3.SmoothDamp(triangle.transform.position, destination, ref velocity, smoothTime);
            }
        }
    }

    void Animate()
    {
        for(int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                GameObject triangle = triangleArray[i, j];
                Vector3 destination = new Vector3(transform.position.x + spectrum[spectrumPointers[i, j]] * amplitude, triangle.transform.position.y, triangle.transform.position.z);

                triangle.transform.position = Vector3.SmoothDamp(triangle.transform.position, destination, ref velocity, smoothTime);
            }
        }
        
    }

    void DistributeSpectrumPointers(int spectrumSize)
    {
        spectrumPointers = new int[triangleArray.GetLength(0), triangleArray.GetLength(1)];

        System.Random r = new System.Random();

        int countdownIndex = spectrumSize;
        for (int i = 0; i < spectrumPointers.GetLength(0); i++)
        {
            for(int j = 0; j < spectrumPointers.GetLength(1); j++)
            {
                if (countdownIndex <= 0)
                {
                    countdownIndex = spectrumSize;
                }
                int randomIndex = r.Next(countdownIndex);
                int number = randomPointers[randomIndex];
                randomPointers[randomIndex] = randomPointers[countdownIndex - 1];
                randomPointers[countdownIndex - 1] = number;

                spectrumPointers[i, j] = number;

                countdownIndex--;
            }
        }        
    }
}
