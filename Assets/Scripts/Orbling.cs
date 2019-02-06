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

    [Header("Destroy Animation")]
    [SerializeField]
    private float upScaleDestroy;
    [SerializeField]
    private float downScaleDestroy;
    [SerializeField]
    private float upScaleDurationDestroy;
    [SerializeField]
    private float downScaleDurationDestroy;

    [Header("Spawn Animation")]
    [SerializeField]
    private float upScaleSpawn;
    [SerializeField]
    private float downScaleSpawn;
    [SerializeField]
    private float upScaleDurationSpawn;
    [SerializeField]
    private float downScaleDurationSpawn;

    [Header("Steam VR")]
    public SteamVR_Action_Boolean grabPinch;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;

    [HideInInspector]
    public GameObject owner;
    [HideInInspector]
    public bool activated;

    private bool activatedTrigger;
    private float destroyTimestamp;
    private float lerpStartTime;

    void Start()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        transform.GetComponents<SphereCollider>()[1].isTrigger = true;

        grabPinch.AddOnChangeListener(OnTriggerPressedOrReleased, inputSource);

        StartCoroutine(SpawnOrblingAnimation());
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

    public IEnumerator DeleteOrbling()
    {
        float t = 0.0F;
        lerpStartTime = Time.time;
        Vector3 oldScale = transform.localScale;
        Vector3 newScale = new Vector3(upScaleDestroy, upScaleDestroy, upScaleDestroy);

        while (t <= 1F)
        {
            t = (Time.time - lerpStartTime) / upScaleDurationDestroy;

            transform.localScale = Vector3.Lerp(oldScale, newScale, t);

            yield return null;
        }

        t = 0.0F;
        lerpStartTime = Time.time;
        oldScale = transform.localScale;
        newScale = new Vector3(downScaleDestroy, downScaleDestroy, downScaleDestroy);

        while (t <= 1F)
        {
            t = (Time.time - lerpStartTime) / downScaleDurationDestroy;

            transform.localScale = Vector3.Lerp(oldScale, newScale, t);

            yield return null;
        }

        RemoveListener();
        Destroy(gameObject);
    }

    public IEnumerator SpawnOrblingAnimation()
    {
        float t = 0.0F;
        lerpStartTime = Time.time;
        Vector3 oldScale = transform.localScale;
        Vector3 newScale = new Vector3(upScaleSpawn, upScaleSpawn, upScaleSpawn);

        while (t <= 1F)
        {
            t = (Time.time - lerpStartTime) / upScaleDurationSpawn;

            transform.localScale = Vector3.Lerp(oldScale, newScale, t);

            yield return null;
        }

        t = 0.0F;
        lerpStartTime = Time.time;
        oldScale = transform.localScale;
        newScale = new Vector3(downScaleSpawn, downScaleSpawn, downScaleSpawn);

        while (t <= 1F)
        {
            t = (Time.time - lerpStartTime) / downScaleDurationSpawn;

            transform.localScale = Vector3.Lerp(oldScale, newScale, t);

            yield return null;
        }
    }

    public void RemoveListener()
    {
        grabPinch.RemoveOnChangeListener(OnTriggerPressedOrReleased, inputSource);
    }

}
