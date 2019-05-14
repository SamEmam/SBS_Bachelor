using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifespanComponent : MonoBehaviour
{
    public float lifespan;
    public GameObject explosionPrefab;
    public bool lifeHasEnded = false;
    //public ParticleSystem ObjectParticles;

    // For Unit Testing Only
    public void Construct(float lifespan, bool lifeHasEnded)
    {
        this.lifespan = lifespan;
        this.lifeHasEnded = lifeHasEnded;
    }
}
