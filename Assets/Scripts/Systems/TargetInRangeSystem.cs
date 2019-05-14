﻿using System.Collections;
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
        public ComponentArray<Transform> transform;
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
            var transform = components.transform[i];

            // New Functionality
            if (aimC.target)
            {
                var directionToTarget = aimC.weaponBase.position - aimC.target.position;
                var angle = Vector3.Angle(aimC.weaponBase.forward, directionToTarget);

                // Calculate distance between this ship and other ship
                var dist = Vector3.Distance(aimC.weaponBase.position, aimC.target.position);
                bool withinRange = false;
                if (dist < maxDistance)
                {
                    withinRange = true;
                }


                if (withinRange && Mathf.Abs(angle) > targetInRangeC.minRange && Mathf.Abs(angle) < targetInRangeC.maxRange && aimC.target.tag != waypointTag)
                {
                    targetInRangeC.isInRange = true;
                    //Debug.DrawLine(transform.position, aimC.target.position, Color.green);
                }
                else
                {
                    targetInRangeC.isInRange = false;
                    //Debug.DrawLine(transform.position, aimC.target.position, Color.red);
                }
            }
            
        }
        
    }
}
