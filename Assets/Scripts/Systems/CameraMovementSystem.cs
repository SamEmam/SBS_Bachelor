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
            // Keep camera within max and min height
            KeepCameraWithinBoundary(entity.transform, camMinHeight, camMaxHeight);

            scroll = SetCameraHeight(entity.transform, camMinHeight, camMaxHeight, entity.inputC.Scroll);

            // Move camera
            UpdatePosition(entity.transform, entity.inputC.Horizontal, scroll, entity.inputC.Vertical, moveSpeed, deltaTime);
        }
    }

    void KeepCameraWithinBoundary(Transform transform, int minHeight, int maxHeight)
    {
        // if position is less than minHeight, set height to min + 1
        if (transform.position.y <= minHeight)
        {
            transform.position = new Vector3(transform.position.x, minHeight + 1, transform.position.z);
        }
        // if position is more than maxHeight, set height to max - 1
        else if (transform.position.y >= maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight - 1, transform.position.z);
        }
    }

    float SetCameraHeight(Transform transform, int minHeight, int maxHeight, float scroll)
    {
        // If camera within min & max height
        if (transform.position.y > minHeight && transform.position.y < maxHeight)
        {
            return scroll;
        }
        else
        {
            return 0f;
        }
    }

    void UpdatePosition(Transform transform, float horizontal, float scroll, float vertical, float speed, float deltaTime)
    {
        var moveVector = new Vector3(horizontal, scroll, vertical);
        var movePosition = transform.position + moveVector.normalized * speed * deltaTime;

        transform.position = movePosition;
    }
}
