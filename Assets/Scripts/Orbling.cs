using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Orbling : MonoBehaviour {

    [SerializeField]
    public SoundType soundType;

    public enum SoundType { Percussion, Bass, Lead }

}
