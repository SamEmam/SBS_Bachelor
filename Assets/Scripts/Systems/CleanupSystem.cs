using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public class CleanupSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        [ReadOnly]
        public SharedComponentDataArray<DeathData> deathC;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var transform = components.transform[i];

            // Destroy GameObject
            Object.Destroy(transform.gameObject);
        }
    }
}
