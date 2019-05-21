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
        public ComponentDataArray<FactionData> factionC;
        public ComponentArray<Transform> transform;
        public EntityArray entities;
    }

    [Inject] private Components components;
    // private Transform lastEnemy;

    protected override void OnUpdate()
    {
        var em = World.Active.GetOrCreateManager<EntityManager>();

        //This ship
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var entity = components.entities[i];
            var rotationC = components.rotationC[i];
            var targetC = components.targetC[i];
            var transform = components.transform[i];

            var factionC = em.GetComponentData<FactionData>(entity);

            targetC.tempTargetedBy = 0;
            
            // Other ship
            for (int j = 0; j < components.Length; j++)
            {
                // Setup
                var otherEntity = components.entities[j];
                var otherRotationC = components.rotationC[j];
                var otherTargetC = components.targetC[j];
                var otherTransform = components.transform[j];

                var otherFactionC = em.GetComponentData<FactionData>(otherEntity);

                targetC.enemyScore = int.MaxValue;

                // Functionality
                var dist = Vector3.Distance(transform.position, otherTransform.position);                               // Calculate distance between this ship and other ship
                bool withinRange = false;
                if (dist < targetC.maxDistance)                                                                         // Check if within maxDistance
                {
                    withinRange = true;
                }
                
                // Check if this ship is not equal to other ship and faction is not equal
                /*
                 * If distance within range
                 * And this ship is not other ship
                 * And this ship is close enough to waypoint
                 * And this faction is not otherFaction
                 * And this tag is not other tag
                 */
                if (withinRange && transform != otherTransform && targetC.isCloseEnoughToWaypoint)
                {
                    if (factionC.faction != otherFactionC.faction && transform.tag != otherTransform.tag)
                    {
                        if (otherFactionC.faction == FactionEnum.Objective)                                     // Check if target is Objective and deduct score
                        {
                            targetC.enemyScore -= 2000;
                        }

                        if (rotationC.target == otherRotationC.target)                                          // Check if targeted by other ship, deduct score, and +1 to ships targeted by
                        {
                            targetC.enemyScore -= 1000;
                            targetC.tempTargetedBy++;
                        }

                        
                        targetC.enemyScore -= targetC.targetedBy * 200;                                         // Check targeted by amount and deduct score

                        
                        if (dist < 50)                                                                          // Check distance between this ship and other ship, and deduct score
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
                        else
                        {
                            targetC.enemyScore -= (int)(dist * 2);
                        }
                        
                        
                        if (targetC.enemyScore >= targetC.targetScore)                                          // Set new target if score of othership is higher than current target
                        {
                            targetC.targetScore = targetC.enemyScore;
                            rotationC.target = otherTransform;
                        }
                    }
                }
            }

            targetC.targetedBy = targetC.tempTargetedBy;                                                        // Set amount of ships targeted by and reset temp counter
            targetC.targetScore = 0;
        }
    }
}