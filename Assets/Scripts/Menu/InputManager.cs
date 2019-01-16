using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    private GameObject canvas;
    private bool menuOn = false;

	void Start () 
	{
        canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
	}
	
	void Update () 
	{
        if (Input.GetButtonDown("Cancel") && !menuOn)
        {
            canvas.SetActive(true);
            menuOn = !menuOn;
        }
        else if (Input.GetButtonDown("Cancel") && menuOn)
        {
            canvas.SetActive(false);
            menuOn = !menuOn;
        }
    }
}
