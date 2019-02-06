using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivatorTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject targetObject;

	void Start () 
	{
        
	}
	
	void Update () 
	{

    }

    public void Trigger(bool on)
    {
        if (targetObject.GetComponent<ParticleSystem>() == true)
        {
            if (on)
            {
                targetObject.GetComponent<ParticleSystem>().Play();
            }
            else if (!on)
            {
                targetObject.GetComponent<ParticleSystem>().Pause();
            }
        }
        else
        {
            if (on)
            {
                targetObject.SetActive(true);
            }
            else if (!on)
            {
                targetObject.SetActive(false);
            }
        }
    }
}
