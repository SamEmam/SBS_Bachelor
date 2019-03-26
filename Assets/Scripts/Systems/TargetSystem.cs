using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class TargetSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<RotationComponent> rotationC;
        public ComponentDataArray<ClosestData> closestC;
        public ComponentDataArray<FactionData> factionC;
        public ComponentArray<Transform> transform;
        public EntityArray entities;
    }

    [Inject] private Components components;
    private Transform lastEnemy;

    protected override void OnUpdate()
    {
        // This ship
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var entity = components.entities[i];
            var rotationC = components.rotationC[i];
            var closestC = components.closestC[i];
            var factionC = components.factionC[i];
            var transform = components.transform[i];

            // Functionality
            closestC.closestDistance = Mathf.Infinity;

            if (rotationC.target)
            {
                lastEnemy = rotationC.target;
            }

            // Other ship
            for (int j = 0; j < components.Length; j++)
            {
                // Setup
                var otherEntity = components.entities[j];
                var otherRotationC = components.rotationC[j];
                var otherClosestC = components.closestC[j];
                var otherFactionC = components.factionC[i];
                var otherTransform = components.transform[j];

                // Functionality

                if (!lastEnemy)
                {
                    lastEnemy = otherTransform.transform;
                }

                //if (factionC.faction == otherFactionC.faction)
                //{
                //    return;
                //}

                // If this ship is not other ship, and if othership is not our current target
                if (transform != otherTransform && otherTransform.transform != lastEnemy)
                {
                    // Calculate distance between this ship and other ship
                    var dist = Vector3.Distance(transform.position, otherTransform.position);

                    // If distance between other ship is shorter than closest
                    // Sets new closest enemy
                    if (dist < closestC.closestDistance)
                    {
                        closestC.closestDistance = dist;
                        rotationC.target = otherTransform.transform;
                    }

                    // If othership is close, it is saved as lastEnemy, and cannot be targeted until new lastEnemy
                    // ClosestDist reset
                    if (closestC.closestDistance < 40)
                    {
                        lastEnemy = rotationC.target;
                        closestC.closestDistance = Mathf.Infinity;
                    }
                }
            }
        }
    }
}
