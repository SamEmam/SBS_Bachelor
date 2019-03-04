using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct DeathData : IComponentData
{
    public int isDead;

    //public ParticleSystem deathEffect;
}

public class DeathComponent : ComponentDataProxy<DeathData>
{

}

