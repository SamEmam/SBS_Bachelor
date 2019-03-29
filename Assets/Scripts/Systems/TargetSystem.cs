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
        public ComponentArray<TargetComponent> targetC;
        public ComponentDataArray<ClosestData> closestC;
        public ComponentDataArray<FactionData> factionC;
        public ComponentArray<Transform> transform;
        public EntityArray entities;
    }

    [Inject] private Components components;
    // private Transform lastEnemy;

    protected override void OnUpdate()
    {
        //This ship
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var entity = components.entities[i];
            var rotationC = components.rotationC[i];
            var targetC = components.targetC[i];
            var closestC = components.closestC[i];
            var factionC = components.factionC[i];
            var transform = components.transform[i];

            targetC.tempTargetedBy = 0;
            
            // Temp setting target to itself, this should be changed to waypoint transform in future
            if (!rotationC.target)
            {
                rotationC.target = transform;
            }

            // Functionality

            // Other ship
            for (int j = 0; j < components.Length; j++)
            {
                // Setup
                var otherEntity = components.entities[j];
                var otherRotationC = components.rotationC[j];
                var otherTargetC = components.targetC[j];
                var otherClosestC = components.closestC[j];
                var otherFactionC = components.factionC[i];
                var otherTransform = components.transform[j];

                targetC.enemyScore = int.MaxValue;

                // Functionality
                // Check if this ship is not equal to other ship and faction is not equal
                if (transform != otherTransform && transform.tag != otherTransform.tag)
                {
                    // Check if targeted by other ship
                    if (rotationC.target == otherRotationC.target)
                    {
                        targetC.enemyScore -= 1000;
                        targetC.tempTargetedBy++;
                    }

                    // Check targeted by amount
                    targetC.enemyScore -= targetC.targetedBy * 200;

                    // Check distance between this ship and other ship
                    var dist = Vector3.Distance(transform.position, otherTransform.position);
                    if (dist < 50)
                    {
                        targetC.enemyScore -= (int)(dist * 10);
                    }
                    else if (dist < 100)
                    {
                        targetC.enemyScore -= (int)(dist * 4);
                    }
                    else if (dist < 300)
                    {
                        targetC.enemyScore -= (int)(dist / 2);
                    }
                    else if (dist < 500)
                    {
                        targetC.enemyScore -= (int)(dist);
                    }
                    else
                    {
                        targetC.enemyScore -= (int)(dist * 2);
                    }

                    // Distance to waypoint (temporarily just a zero vector)
                    var distToWaypoint = Vector3.Distance(transform.position, Vector3.zero);

                    // Set new target
                    if (targetC.enemyScore >= targetC.targetScore && distToWaypoint < 200)
                    {
                        targetC.targetScore = targetC.enemyScore;
                        rotationC.target = otherTransform;
                    }
                }
            }
            targetC.targetedBy = targetC.tempTargetedBy;
            targetC.targetScore = 0;
        }
    }
}

    // OLD TARGET SYSTEM

    //protected override void OnUpdate()
    //{
    //    // This ship
    //    for (int i = 0; i < components.Length; i++)
    //    {
    //        // Setup
    //        var entity = components.entities[i];
    //        var rotationC = components.rotationC[i];
    //        var closestC = components.closestC[i];
    //        var factionC = components.factionC[i];
    //        var transform = components.transform[i];

    //        // Functionality
    //        closestC.closestDistance = Mathf.Infinity;

    //        if (rotationC.target)
    //        {
    //            lastEnemy = rotationC.target;
    //        }

    //        // Other ship
    //        for (int j = 0; j < components.Length; j++)
    //        {
    //            // Setup
    //            var otherEntity = components.entities[j];
    //            var otherRotationC = components.rotationC[j];
    //            var otherClosestC = components.closestC[j];
    //            var otherFactionC = components.factionC[i];
    //            var otherTransform = components.transform[j];

    //            // Functionality

    //            if (!lastEnemy)
    //            {
    //                lastEnemy = otherTransform.transform;
    //            }

    //            //if (factionC.faction == otherFactionC.faction)
    //            //{
    //            //    return;
    //            //}

    //            // If this ship is not other ship, and if othership is not our current target
    //            if (transform != otherTransform && otherTransform.transform != lastEnemy)
    //            {
    //                // Calculate distance between this ship and other ship
    //                var dist = Vector3.Distance(transform.position, otherTransform.position);

    //                // If distance between other ship is shorter than closest
    //                // Sets new closest enemy
    //                if (dist < closestC.closestDistance)
    //                {
    //                    closestC.closestDistance = dist;
    //                    rotationC.target = otherTransform.transform;
    //                }

    //                // If othership is close, it is saved as lastEnemy, and cannot be targeted until new lastEnemy
    //                // ClosestDist reset
    //                if (closestC.closestDistance < 40)
    //                {
    //                    lastEnemy = rotationC.target;
    //                    closestC.closestDistance = Mathf.Infinity;
    //                }
    //            }
    //        }
    //    }
    //}
