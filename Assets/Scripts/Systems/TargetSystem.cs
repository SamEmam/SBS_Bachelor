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

        var playerTargetCs = new List<TargetComponent>();
        var playerRotationCs = new List<RotationComponent>();
        var playerFactionCs = new List<FactionData>();
        var playerTransforms = new List<Transform>();
        var playerEntities = new List<Entity>();

        var enemyTargetCs = new List<TargetComponent>();
        var enemyRotationCs = new List<RotationComponent>();
        var enemyFactionCs = new List<FactionData>();
        var enemyTransforms = new List<Transform>();
        var enemyEntities = new List<Entity>();
        

        // Filtering player and enemy spaceships
        for (int i = 0; i < components.Length; i++)
        {
            if (em.GetComponentData<FactionData>(components.entities[i]).faction == FactionEnum.Player)
            {
                playerTargetCs.Add(components.targetC[i]);
                playerRotationCs.Add(components.rotationC[i]);
                playerTransforms.Add(components.transform[i]);
                playerEntities.Add(components.entities[i]);
            }
            else
            {

                enemyTargetCs.Add(components.targetC[i]);
                enemyRotationCs.Add(components.rotationC[i]);
                enemyTransforms.Add(components.transform[i]);
                enemyEntities.Add(components.entities[i]);
            }
        }

        CompareTargets(
            playerEntities, enemyEntities,
            playerTargetCs, enemyTargetCs,
            playerRotationCs, enemyRotationCs,
            playerTransforms, enemyTransforms,
            em);

        CompareTargets(
            enemyEntities, playerEntities,
            enemyTargetCs, playerTargetCs,
            enemyRotationCs, playerRotationCs,
            enemyTransforms, playerTransforms,
            em);
    }

    void CompareTargets(
            List<Entity> entities, List<Entity> otherEntities,
            List<TargetComponent> targetCs, List<TargetComponent> otherTargetCs,
            List<RotationComponent> rotationCs, List<RotationComponent> otherRotationCs,
            List<Transform> transforms, List<Transform> otherTransforms,
            EntityManager em)
    {
        for (int j = 0; j < entities.Count; j++)
        {
            // Setup
            var entity = entities[j];
            var targetC = targetCs[j];
            var rotationC = rotationCs[j];
            var transform = transforms[j];
            var factionC = em.GetComponentData<FactionData>(entity);

            // Reset temp targeted by amount to zero
            targetC.tempTargetedBy = 0;

            // Other ship
            for (int k = 0; k < otherEntities.Count; k++)
            {
                // Setup
                var otherEntity = otherEntities[j];
                var otherTargetC = otherTargetCs[j];
                var otherRotationC = otherRotationCs[j];
                var otherTransform = otherTransforms[j];
                var otherFactionC = em.GetComponentData<FactionData>(otherEntity);

                // Reset enemyScore to max int
                targetC.enemyScore = int.MaxValue;

                // Functionality
                var dist = CalculateDistance(transform, otherTransform);

                // Check if within maxDistance
                if (dist < targetC.maxDistance)
                {

                    if (targetC.isCloseEnoughToWaypoint)
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