using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private GameObject holder;

    private bool menuOn = false;

	void Start () 
	{
        holder.SetActive(false);
	}
	
	void Update () 
	{
        if (Input.GetButtonDown("Cancel") && !menuOn)
        {
            holder.SetActive(true);
            menuOn = !menuOn;
        }
        else if (Input.GetButtonDown("Cancel") && menuOn)
        {
            holder.SetActive(false);
            menuOn = !menuOn;
        }
    }
}
