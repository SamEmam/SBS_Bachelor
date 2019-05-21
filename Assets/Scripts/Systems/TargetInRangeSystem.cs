using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class TargetInRangeSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<AimComponent> aimC;
        public ComponentArray<TargetInRangeComponent> targetInRangeC;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        float maxDistance = 150f;
        string waypointTag = "Waypoint";

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var aimC = components.aimC[i];
            var targetInRangeC = components.targetInRangeC[i];

            // Functionality
            if (aimC.target)                                                                                // If target is not null
            {
                var directionToTarget = aimC.weaponBase.position - aimC.target.position;
                var angle = Vector3.Angle(aimC.weaponBase.forward, directionToTarget);                      // Calculate angle between weaponbase and other ship
                
                var dist = Vector3.Distance(aimC.weaponBase.position, aimC.target.position);                // Calculate distance between weaponbase and other ship
                bool withinRange = false;
                if (dist < maxDistance)                                                                     // Is within distance if distance is < maxDistance
                {
                    withinRange = true;
                }
                
                /*
                 * If distance within range
                 * If angle within minRange and maxRange
                 * If target.tag is not waypointTag
                 */
                if (withinRange && Mathf.Abs(angle) > targetInRangeC.minRange && Mathf.Abs(angle) < targetInRangeC.maxRange && aimC.target.tag != waypointTag)
                {
                    targetInRangeC.isInRange = true;
                }
                else
                {
                    targetInRangeC.isInRange = false;
                }
            }
        }
    }
}
