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
            if (aimC.target)                                                            // If target is not null
            {
                var angle = CalculateAngle(aimC.weaponBase, aimC.target);               // Calculate angle between weaponbase and other ship

                var dist = CalculateDistance(aimC.weaponBase, aimC.target);             // Calculate distance between weaponbase and other ship

                /*
                * If distance within range
                * If angle within minRange and maxRange
                * If target.tag is not waypointTag
                */
                if (dist < maxDistance && CheckAngle(angle, targetInRangeC.minRange, targetInRangeC.maxRange) && aimC.target.tag != waypointTag)                                                                     // Is within distance if distance is < maxDistance
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

    // Calculate angle between two positions
    float CalculateAngle(Transform weaponBase, Transform target)
    {
        var directionToTarget = weaponBase.position - target.position;
        return Vector3.Angle(weaponBase.forward, directionToTarget);
    }

    // Calculate distance between two positions
    float CalculateDistance(Transform weaponBase, Transform target)
    {
        return Vector3.Distance(weaponBase.position, target.position);
    }

    // Check if an angle is between two values
    bool CheckAngle(float angle, float minAngle, float maxAngle)
    {
        if (Mathf.Abs(angle) > minAngle && Mathf.Abs(angle) < maxAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
