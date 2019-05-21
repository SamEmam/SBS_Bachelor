using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class InputSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<InputComponent> inputC;
        public ComponentArray<WaypointComponent> waypointC;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        // Camera movement
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        // scroll inverted
        var scroll = Input.GetAxis("Mouse ScrollWheel") * -1f;

        // Mouse tracking
        var mousePosition = Input.mousePosition;
        var cameraRay = Camera.main.ScreenPointToRay(mousePosition);

        // LayerMasks setup
        var floorLayerMask = LayerMask.GetMask("Floor");
        var objectiveLayerMask = LayerMask.GetMask("Objective");

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var inputC = components.inputC[i];
            var waypointC = components.waypointC[i];

            inputC.Horizontal = horizontal;
            inputC.Vertical = vertical;
            inputC.Scroll = scroll;

            // Functionality

            /*
             * If mouse clicks on objectiveLayerMask
             * set the position, and set the parent to the objective
             * play waypoint particles
             */
            if (Physics.Raycast(cameraRay, hitInfo: out RaycastHit objectiveHit, maxDistance: 10000, layerMask: objectiveLayerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    waypointC.waypoint.position = objectiveHit.point;
                    waypointC.waypoint.SetParent(objectiveHit.collider.transform);
                    waypointC.waypointParticles.Play();
                }
            }

            /*
             * If mouse clicks on floorLayerMask
             * set the position, and set the parent to null
             * reset the rotation of the waypoint
             * play waypoint particles
             */
            else if (Physics.Raycast(cameraRay, hitInfo: out RaycastHit floorHit, maxDistance: 10000, layerMask: floorLayerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    waypointC.waypoint.parent = null;
                    waypointC.waypoint.position = floorHit.point;
                    waypointC.waypoint.rotation = Quaternion.Euler(new Vector3(270,0,0));
                    waypointC.waypointParticles.Play();
                }
            }
        }
    }
}
