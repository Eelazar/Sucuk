using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideshowMaster : MonoBehaviour {

    [SerializeField]
    private GameObject[] slides;
    [SerializeField]
    private float cameraDistance;
    [SerializeField]
    private float cameraMoveDuration;


    private int slideCounter;

	void Start () 
	{
		foreach(GameObject slide in slides)
        {
            slide.SetActive(false);
        }
	}
	
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Space) && slideCounter < slides.Length)
        {
            NextSlide();
        }
	}

    private void NextSlide()
    {
        if(slideCounter > 0)
        {
            slides[slideCounter - 1].SetActive(false);
        }

        slides[slideCounter].SetActive(true);

        slideCounter++;
    }
}
