using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CollisionComponent : MonoBehaviour
{
    Entity entity;
    EntityManager entityManager;

    void OnEnable()
    {
        entity = gameObject.GetComponent<GameObjectEntity>().Entity;
        entityManager = World.Active.GetExistingManager<EntityManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        AddOrUpdateImpact(collision, CollisionState.Enter);
    }

    void OnCollisionStay(Collision collision)
    {
        AddOrUpdateImpact(collision, CollisionState.Stay);
    }

    void OnCollisionExit(Collision collision)
    {
        AddOrUpdateImpact(collision, CollisionState.Exit);
    }

    void AddOrUpdateImpact(Collision collision, CollisionState newState)
    {
        var collidingEntity = collision.gameObject.GetComponent<GameObjectEntity>().Entity;

        if (!entityManager.HasComponent<CollisionDataComponent>(collidingEntity))
        {
            entityManager.AddComponent(collidingEntity, typeof(CollisionDataComponent));
        }

        entityManager.SetComponentData<CollisionDataComponent>(collidingEntity, new CollisionDataComponent
        {
            State = newState,
            OtherEntity = entity,
            Collision = new CollisionData(collision)
        });
    }
}