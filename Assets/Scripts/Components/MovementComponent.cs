using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;


[Serializable]
public struct MovementData : IComponentData
{
    public int movementSpeed;
}

public class MovementComponent : ComponentDataProxy<MovementData>
{

}