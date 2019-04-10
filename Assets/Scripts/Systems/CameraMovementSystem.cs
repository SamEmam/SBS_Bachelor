using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CameraMovementSystem : ComponentSystem
{
    struct Components
    {
        public Transform transform;
        public InputComponent inputC;
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        var moveSpeed = 50;
        var camMinHeight = 25;
        var camMaxHeight = 75;
        var scroll = 0f;

        foreach (var entity in GetEntities<Components>())
        {
            // If camera under min height, check if scroll is positive
            if (Camera.main.transform.position.y < camMinHeight && entity.inputC.Scroll > 0)
            {
                scroll = entity.inputC.Scroll;
            }
            // If camera over max height, check if scroll is negative
            else if (Camera.main.transform.position.y > camMaxHeight && entity.inputC.Scroll < 0)
            {
                scroll = entity.inputC.Scroll;
            }
            // If camera within min & max height
            else if (Camera.main.transform.position.y > camMinHeight && Camera.main.transform.position.y < camMaxHeight)
            {
                scroll = entity.inputC.Scroll;
            }

            // Move camera
            var moveVector = new Vector3(entity.inputC.Horizontal, scroll, entity.inputC.Vertical);
            var movePosition = entity.transform.position + moveVector.normalized * moveSpeed * deltaTime;

            entity.transform.position = movePosition;
        }
    }
}
