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

        //Camera.main.transform.position.y

        foreach (var entity in GetEntities<Components>())
        {
            // if position is less than minHeight, set height to min + 1
            if (entity.transform.position.y <= camMinHeight)
            {
                entity.transform.position = new Vector3(entity.transform.position.x, camMinHeight + 1, entity.transform.position.z);
            }
            // if position is more than maxHeight, set height to max - 1
            else if (entity.transform.position.y >= camMaxHeight)
            {
                entity.transform.position = new Vector3(entity.transform.position.x, camMaxHeight - 1, entity.transform.position.z);
            }

            // If camera under min height, check if scroll is positive
            if (entity.transform.position.y < camMinHeight && entity.inputC.Scroll > 0)
            {
                scroll = entity.inputC.Scroll;
            }
            // If camera over max height, check if scroll is negative
            else if (entity.transform.position.y > camMaxHeight && entity.inputC.Scroll < 0)
            {
                scroll = entity.inputC.Scroll;
            }
            // If camera within min & max height
            else if (entity.transform.position.y > camMinHeight && entity.transform.position.y < camMaxHeight)
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
