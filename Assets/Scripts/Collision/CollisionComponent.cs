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

    // Triggered once when colliding with another collider
    void OnCollisionEnter(Collision collision)
    {
        AddOrUpdateCollision(collision, CollisionState.Enter);
    }

    // Triggered as long as colliding with another collider
    void OnCollisionStay(Collision collision)
    {
        AddOrUpdateCollision(collision, CollisionState.Stay);
    }

    // Triggered when stopping collision with another collider
    void OnCollisionExit(Collision collision)
    {
        AddOrUpdateCollision(collision, CollisionState.Exit);
    }

    // Adds CollisionDataComponent to entities that are colliding with another collider
    void AddOrUpdateCollision(Collision collision, CollisionState newState)
    {
        var collidingEntity = collision.gameObject.GetComponent<GameObjectEntity>().Entity;

        if (!entityManager.HasComponent<CollisionDataComponent>(collidingEntity))
        {
            entityManager.AddComponent(collidingEntity, typeof(CollisionDataComponent));
        }

        entityManager.SetComponentData<CollisionDataComponent>(collidingEntity, new CollisionDataComponent
        {
            collisionState = newState,
            otherEntity = entity
        });
    }
}