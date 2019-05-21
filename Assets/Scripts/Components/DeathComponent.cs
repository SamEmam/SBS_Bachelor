using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct DeathData : IComponentData
{
    public DeathEnum deathState;
}

public class DeathComponent : ComponentDataProxy<DeathData>
{
    
}

public enum DeathEnum
{
    Alive = 0,
    Dead = 1
}

