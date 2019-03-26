using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct FactionData : IComponentData
{
    public int faction;
}

public class FactionComponent : ComponentDataProxy<FactionData>
{
    //Player = 0
    //Enemy = 1
    //Asteroid = 2
}
