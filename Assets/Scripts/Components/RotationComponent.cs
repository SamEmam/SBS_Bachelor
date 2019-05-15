using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class RotationComponent : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;

    // This is for Unit Testing only
    public void Construct(Transform target, float rotationSpeed)
    {
        this.target = target;
        this.rotationSpeed = rotationSpeed;
    }
}

