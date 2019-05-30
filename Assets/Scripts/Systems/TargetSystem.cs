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

            // Reset temp targeted by amount to zero
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

                // Reset enemyScore to max int
                targetC.enemyScore = int.MaxValue;

                // Functionality
                var dist = CalculateDistance(transform, otherTransform);

                // Check if within maxDistance
                if (dist < targetC.maxDistance)                                                                     
                {

                    if (FactionCheck(
                        transform, otherTransform,
                        factionC.faction, otherFactionC.faction,
                        transform.tag, otherTransform.tag,
                        targetC.isCloseEnoughToWaypoint))
                    {

                        ObjectiveCheck(otherFactionC.faction, targetC.enemyScore, 2000);

                        TargetedByCheck(transform, otherRotationC.target, targetC.enemyScore, targetC.tempTargetedBy, 1000);

                        // Check targeted by amount and deduct score
                        targetC.enemyScore -= targetC.targetedBy * 200;

                        DistanceCheck(dist, targetC.enemyScore, 50);

                        UpdateTarget(targetC.enemyScore, targetC.targetScore, rotationC, otherTransform);
                    }

                }
            }
            // Set amount of ships targeted by and reset temp counter
            targetC.targetedBy = targetC.tempTargetedBy;
            targetC.targetScore = 0;
        }
    }

    // Calculates distance between two positions
    float CalculateDistance(Transform transform, Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    /*
    * If this ship is not target ship
    * And this ship is close enough to waypoint
    * And this faction is not target faction
    * And this tag is not target tag
    */
    bool FactionCheck(Transform transform, Transform target, FactionEnum faction, FactionEnum targetFaction, string tag, string targetTag, bool closeEnoughToWaypoint)
    {
        if (transform != target && closeEnoughToWaypoint && faction != targetFaction && tag != targetTag)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Check if target is Objective and deduct penalty
    void ObjectiveCheck(FactionEnum faction, int enemyScore, int penalty)
    {
        if (faction == FactionEnum.Objective)
        {
            enemyScore -= penalty;
        }
    }

    // Check if targeted by other ship, deduct penalty, and +1 to ships targeted by
    void TargetedByCheck(Transform transform, Transform targetOfTarget, int enemyScore, int targetedBy, int penalty)
    {
        if (transform == targetOfTarget)
        {
            enemyScore -= penalty;
            targetedBy++;
        }
    }

    // Check distance between this ship and other ship, and deduct score
    void DistanceCheck(float distance, int enemyScore, int distanceInterval)
    {
        if (distance < distanceInterval)
        {
            enemyScore -= (int)(distance * 10);
        }

        else if (distance < distanceInterval * 2)
        {
            enemyScore -= (int)(distance * 4);
        }

        else if (distance < distanceInterval * 6)
        {
            enemyScore -= (int)(distance / 2);
        }

        else
        {
            enemyScore -= (int)(distance * 2);
        }
    }

    // Set new target if score of other ship is higher than current target
    void UpdateTarget(int enemyScore, int targetScore, RotationComponent rotationC, Transform target)
    {
        if (enemyScore >= targetScore)
        {
            targetScore = enemyScore;
            rotationC.target = target;
        }
    }
}