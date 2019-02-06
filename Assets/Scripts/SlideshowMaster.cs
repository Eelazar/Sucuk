using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SlideshowMaster : MonoBehaviour {

    [SerializeField]
    private GameObject[] slides;
    [SerializeField]
    private Transform[] cameraPositions;
    [SerializeField]
    private float cameraMoveDuration;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject[] activatorScripts;
    [SerializeField]
    private PostProcessingProfile[] lerpProfiles;
    
    
    private int stepCounter;

	void Start () 
	{
		foreach(GameObject slide in slides)
        {
            slide.SetActive(false);
        }
	}
	
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PreviousStep();
        }
    }

    private void NextStep()
    {
        ShowStep(stepCounter);
        stepCounter++;
    }

    private void PreviousStep()
    {
        stepCounter --;
        ShowStep(stepCounter - 1);
    }

    private void ShowStep(int step)
    {
        switch (step)
        {
            case 0:
                LerpSlide(0, 0);
                break;
            case 1:
                LerpSlide(1, 1);
                break;
            case 2:
                LerpSlide(2, 2);
                break;
            case 3:
                LerpSlide(3, 3);
                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
            case 9:

                break;
            case 10:

                break;
            case 11:

                break;
            case 12:

                break;
            case 13:

                break;
            case 14:

                break;
            case 15:

                break;
            case 16:

                break;
            case 17:

                break;
            case 18:

                break;
            case 19:

                break;
            case 20:

                break;
            default:
                Debug.Log("No more steps programmed");
                break;
        }
    }

    private void LerpSlide(int slideIndex, int cameraIndex)
    {
        slides[slideIndex].SetActive(true);

        StartCoroutine(LerpCamera(cameraPositions[cameraIndex]));

        StartCoroutine(DeactivateSlides(slides[slideIndex]));
    }

    private void TPSlide(int slideIndex, int cameraIndex)
    {
        foreach (GameObject slide in slides)
        {
            slide.SetActive(false);
        }

        slides[slideIndex].SetActive(true);

        SetCamera(cameraPositions[cameraIndex]);
    }

    private IEnumerator DeactivateSlides(GameObject exception)
    {
        yield return new WaitForSeconds(cameraMoveDuration);

        foreach (GameObject slide in slides)
        {
            if(slide != exception)
            {
                slide.SetActive(false);
            }
        }
    }

    private IEnumerator LerpCamera(Transform target)
    {
        Vector3 position = cam.transform.position;
        Quaternion rotation = cam.transform.rotation;
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        float t = 0.0F;
        float lerpStart = Time.time;

        while(t <= 1F)
        {
            t = (Time.time - lerpStart) / cameraMoveDuration;

            cam.transform.position = Vector3.Lerp(position, targetPosition, t);
            cam.transform.rotation = Quaternion.Slerp(rotation, targetRotation, t);

            yield return null;
        }
    }

    private void SetCamera(Transform target)
    {
        cam.transform.position = target.position;
        cam.transform.rotation = target.rotation;
    }
}
