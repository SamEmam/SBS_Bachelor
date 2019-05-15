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
            if (rotationC.target == null)
            {
                rotationC.target = waypointC.waypoint;
            }

            var dist = Vector3.Distance(waypointC.waypoint.position, transform.position);
            if (dist > waypointC.maxDistFromWaypoint)
            {
                rotationC.target = waypointC.waypoint;
                targetC.isCloseEnoughToWaypoint = false;
            }
            else
            {
                targetC.isCloseEnoughToWaypoint = true;
            }
        }
    }
}
