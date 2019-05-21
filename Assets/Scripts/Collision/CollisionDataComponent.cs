using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct CollisionDataComponent : IComponentData
{
    public Entity otherEntity;
    public CollisionState collisionState;
}
