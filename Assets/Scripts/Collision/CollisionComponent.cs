using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CollisionComponent : MonoBehaviour
{
    Entity entity;
    EntityManager entityManager;

    // Get entity and entity manager
    void OnEnable()
    {
        entity = gameObject.GetComponent<GameObjectEntity>().Entity;
        entityManager = World.Active.GetExistingManager<EntityManager>();
    }

    // Triggered once when starting to collide with another collider
    void OnCollisionEnter(Collision collision)
    {
        AddOrUpdateCollision(collision, CollisionState.Enter);
    }

    // Triggered while colliding with another collider
    void OnCollisionStay(Collision collision)
    {
        AddOrUpdateCollision(collision, CollisionState.Stay);
    }

    // Triggered once when stopping collision with another collider
    void OnCollisionExit(Collision collision)
    {
        AddOrUpdateCollision(collision, CollisionState.Exit);
    }

    // Adds CollisionDataComponent to other entities that are colliding with this entity
    void AddOrUpdateCollision(Collision collision, CollisionState newState)
    {
        // Get the colliding colliders Entity
        var collidingEntity = collision.gameObject.GetComponent<GameObjectEntity>().Entity;
        
        // Give the other Entity collision data if it has none
        if (!entityManager.HasComponent<CollisionDataComponent>(collidingEntity))
        {
            entityManager.AddComponent(collidingEntity, typeof(CollisionDataComponent));
        }

        // Update the collision data of the other Entity
        entityManager.SetComponentData<CollisionDataComponent>(collidingEntity, new CollisionDataComponent
        {
            collisionState = newState,
            otherEntity = entity
        });
    }
}