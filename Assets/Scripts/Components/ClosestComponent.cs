using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct ClosestData : IComponentData
{
    public float closestDistance;
}

public class ClosestComponent : ComponentDataProxy<ClosestData>
{

}