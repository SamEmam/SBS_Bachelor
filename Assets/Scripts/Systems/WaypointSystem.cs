using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class WaypointSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<RotationComponent> rotationC;
        public ComponentArray<WaypointComponent> waypointC;
        public ComponentArray<TargetComponent> targetC;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;


    protected override void OnUpdate()
    {
        //This ship
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var rotationC = components.rotationC[i];
            var waypointC = components.waypointC[i];
            var targetC = components.targetC[i];
            var transform = components.transform[i];

            // Functionality
            if (rotationC.target == null)                                                       // If ship target is null, set it to waypoint
            {
                rotationC.target = waypointC.waypoint;
            }

            var dist = Vector3.Distance(waypointC.waypoint.position, transform.position);       // Calculate distance between ship and waypoint

            if (dist > waypointC.maxDistFromWaypoint)                                           // If distance is larger than max distance
            {
                rotationC.target = waypointC.waypoint;                                          // Set target to waypoint and set isCloseEnoughToWaypoint boolean
                targetC.isCloseEnoughToWaypoint = false;
            }
            else
            {
                targetC.isCloseEnoughToWaypoint = true;                                         // Else set isCloseEnoughToWaypoint boolean
            }
        }
    }
}
