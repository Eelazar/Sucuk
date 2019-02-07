using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMaster : MonoBehaviour {

    public DateTime startTime;

    void Awake()
    {
    }

    void Start () 
	{
        startTime = DateTime.Now;        
		
	}
	
	void Update () 
	{
		
	}
}
