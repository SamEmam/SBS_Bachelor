using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidComponent : MonoBehaviour
{
    public bool hasSetSize = false;
    public float minSize;
    public float maxSize;

    // For Unit Testing Only
    public void Construct(float minSize, float maxSize)
    {
        this.minSize = minSize;
        this.maxSize = maxSize;
    }
}
