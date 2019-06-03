using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifespanComponent : MonoBehaviour
{
    public float lifespan;
    public float updateTimeStamp;

    // For Unit Testing Only
    public void Construct(float lifespan)
    {
        this.lifespan = lifespan;
    }
}
