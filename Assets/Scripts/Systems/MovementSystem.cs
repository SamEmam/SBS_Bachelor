using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementSystem : ComponentSystem
{
    struct Components
    {
        public MovementComponent movementC;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        foreach (var entity in GetEntities<Components>())
        {
            entity.transform.position += entity.transform.forward * entity.movementC.movementSpeed * deltaTime;
        }
    }
}
