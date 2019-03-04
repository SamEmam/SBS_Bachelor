using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct TargetData : IComponentData
{
    public float closestDistance;
}

public class TargetComponent : ComponentDataProxy<TargetData>
{

}