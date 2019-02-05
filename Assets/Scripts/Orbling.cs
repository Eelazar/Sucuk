using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Orbling : MonoBehaviour {

    public enum TrackType { Percussion, Bass, Lead, Kick, Chord }

    [SerializeField]
    public TrackType trackType;
    [Tooltip("The orbling will be destroyed after this amount of time, after being activated")]
    [SerializeField]
    private float selfDestructTime;
    [SerializeField]
    public GameObject particlePrefab;

    public SteamVR_Action_Boolean grabPinch;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;

    [HideInInspector]
    public GameObject owner;
    [HideInInspector]
    public bool activated;

    private bool activatedTrigger;
    private float destroyTimestamp;

    void Start()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        transform.GetComponents<SphereCollider>()[1].isTrigger = true;

        grabPinch.AddOnChangeListener(OnTriggerPressedOrReleased, inputSource);        
    }

    void Update()
    {
        if(activatedTrigger == true)
        {
            destroyTimestamp = Time.time + selfDestructTime;
            activatedTrigger = false;
        }

        if(activated == true)
        {
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }    
        
    }

    private void LateUpdate()
    {
        if (activated == true && Time.time >= destroyTimestamp)
        {
            DeleteOrbling();
        }
    }

    private void OnTriggerPressedOrReleased(SteamVR_Action_In action_In)
    {
        if (transform != null)
        {
            if (transform.GetComponent<Interactable>().wasHovering)
            {
                activated = true;
                activatedTrigger = true;
            }
        }
    }

    public void DeleteOrbling()
    {
        grabPinch.RemoveOnChangeListener(OnTriggerPressedOrReleased, inputSource);
        Destroy(gameObject);
    }

}
