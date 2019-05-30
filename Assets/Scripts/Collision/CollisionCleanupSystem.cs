using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CollisionCleanupSystem : ComponentSystem
{
    struct Data
    {
        public readonly int Length;
        public ComponentDataArray<CollisionDataComponent> collisionDataC;
        public EntityArray Entity;
    }
    [Inject] Data data;

    protected override void OnUpdate()
    {
        for (var i = 0; i < data.Length; i++)
        {
            // Removes CollisionDataComponent from entites post updates to cleanup entities
            PostUpdateCommands.RemoveComponent<CollisionDataComponent>(data.Entity[i]);
        }
    }
}