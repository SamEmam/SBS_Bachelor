using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct FactionData : IComponentData
{
    public FactionEnum faction;
}

public class FactionComponent : ComponentDataProxy<FactionData>
{
    //Player = 0
    //Enemy = 1
    //Asteroid = 2
}

public enum FactionEnum
{
    Player = 0,
    Enemy,
    Asteroid,
    Objective
}
