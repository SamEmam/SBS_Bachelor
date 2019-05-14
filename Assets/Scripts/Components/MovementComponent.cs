using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;


[Serializable]
public struct MovementData : IComponentData
{
    public MovementType movementType;
    public int movementSpeed;

}

public class MovementComponent : ComponentDataProxy<MovementData>
{
    
}

public enum MovementType
{
    vectorMovement = 1,
    rigidbodyMovement = 2
}