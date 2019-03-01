using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

// https://github.com/Unity-Technologies/EntityComponentSystemSamples/issues/45

public class CollisionEventManager : MonoBehaviour
{
    Entity entity;
    EntityManager em;
    
    public enum Overlap
    {
        Enter,
        Stay,
        Exit
    }

    void OnEnable()
    {
        entity = gameObject.GetComponent<GameObjectEntity>().Entity;
        em = World.Active.GetExistingManager<EntityManager>();
    }

    void OnCollisionEnter(Collision col)
    {
        AddOrUpdateCollision(col, Overlap.Enter);
    }

    void OnCollisionStay(Collision col)
    {
        AddOrUpdateCollision(col, Overlap.Stay);
    }

    void OnCollisionExit(Collision col)
    {
        AddOrUpdateCollision(col, Overlap.Exit);
    }

    void AddOrUpdateCollision(Collision col, Overlap newState)
    {
        var collidingEntity = collision.gameObject.GetComponent<GameObjectEntity>().Entity;

        if (!em.HasComponent<CollisionComponent>(collidingEntity))
        {
            em.AddComponent(collidingEntity, typeof(CollisionComponent));
        }

        em.SetComponentData<CollisionComponent>(collidingEntity, new CollisionComponent
        {
            State = newState,
            OtherEntity = entity,
            Collision = new CollisionData(collision)
        });
    }
}
