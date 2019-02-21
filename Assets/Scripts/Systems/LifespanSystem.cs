using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LifespanSystem : ComponentSystem
{

    struct Components
    {
        public LifespanComponent lifespanC;
    }

    protected override void OnUpdate()
    {
        var deltatime = Time.deltaTime;

        foreach (var entity in GetEntities<Components>())
        {
            if (entity.lifespanC.lifespan <= 0)
            {
                Object.Destroy(entity.lifespanC.gameObject);
            }

            entity.lifespanC.lifespan -= deltatime;
        }
    }

}
