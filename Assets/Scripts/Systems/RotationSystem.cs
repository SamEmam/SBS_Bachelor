using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class RotationSystem : ComponentSystem
{
    struct Components
    {
        public RotationComponent rotationC;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        foreach (var entity in GetEntities<Components>())
        {
            var pos = entity.rotationC.target.position - entity.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(pos);
            entity.transform.rotation = Quaternion.Slerp(entity.transform.rotation, targetRotation, entity.rotationC.rotationSpeed * deltaTime);
        }
    }
}
