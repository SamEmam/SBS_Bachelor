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
            // If spaceships target is null, set target to waypoint
            if (rotationC.target == null)
            {
                rotationC.target = waypointC.waypoint;
            }

            DistanceToWaypointCheck(waypointC.waypoint, transform, waypointC.maxDistFromWaypoint, rotationC, targetC.isCloseEnoughToWaypoint);
            
        }
    }

    void DistanceToWaypointCheck(Transform waypoint, Transform spaceship, int maxDistance, RotationComponent rotationC, bool closeEnoughToWaypoint)
    {
        var dist = Vector3.Distance(waypoint.position, spaceship.position);     // Calculate distance between ship and waypoint

        if (dist > maxDistance)                                                 // If distance is larger than max distance
        {
            rotationC.target = waypoint;                                        // Set target to waypoint and set isCloseEnoughToWaypoint boolean
            closeEnoughToWaypoint = false;
        }
        else
        {
            closeEnoughToWaypoint = true;                                       // Else set isCloseEnoughToWaypoint boolean
        }
    }
}
