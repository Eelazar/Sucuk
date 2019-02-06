using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class OrblingPraesi : MonoBehaviour {

    public enum TrackType { Percussion, Bass, Lead, Kick, Chord }

    [SerializeField]
    public TrackType trackType;
    [SerializeField]
    public GameObject particlePrefab;
    [SerializeField]
    public float particleDestroyTime;

    private bool activatedTrigger;
    private float destroyTimestamp;
    private float lerpStartTime;

    void Start()
    {
        transform.GetComponents<SphereCollider>()[1].isTrigger = true;
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {

    }

}
