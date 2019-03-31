using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;


[Serializable]
public struct MovementData : IComponentData
{
    public int fluidMovement;
    public int movementSpeed;
}

public class MovementComponent : ComponentDataProxy<MovementData>
{
    // FluidMovement = 1    This is strict vector movement
    // FluidMovement > 1    This is smooth rigidbody movement
}