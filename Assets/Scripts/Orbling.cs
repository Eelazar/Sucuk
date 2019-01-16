using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Orbling : MonoBehaviour {

    [SerializeField]
    public TrackType trackType;

    public enum TrackType { Percussion, Bass, Lead }

}
