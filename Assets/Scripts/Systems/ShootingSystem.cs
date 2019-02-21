using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;

public class ShootingSystem : ComponentSystem
{
    struct Components
    {
        public WeaponComponent weaponC;
    }


    protected override void OnUpdate()
    {
        var deltatime = Time.deltaTime;

        foreach (var entity in GetEntities<Components>())
        {
            if (entity.weaponC.fireCountdown <= 0f)
            {
                //var manager = World.Active.GetOrCreateManager<EntityManager>();

                //var shotArchetype = manager.CreateArchetype(typeof(Position), typeof(Rotation));

                //Entity shotEntity = manager.CreateEntity(shotArchetype);

                //manager.SetComponentData(shotEntity, new Position { Value = entity.weaponC.firePoint.position });
                //manager.SetComponentData(shotEntity, new Rotation { Value = entity.weaponC.firePoint.rotation });
                //NativeArray<Entity> entities = new NativeArray<Entity>(1, Allocator.Temp);
                //manager.Instantiate(entity.weaponC.shotPrefab,entities);
                //for (int i = 0; i < 1; i++)
                //{
                //    manager.SetComponentData(entities[i], new Position { Value = entity.weaponC.firePoint.position });
                //    manager.SetComponentData(entities[i], new Rotation { Value = entity.weaponC.firePoint.rotation });
                //}
                //entities.Dispose();

                var shot = Object.Instantiate(entity.weaponC.shotPrefab, entity.weaponC.firePoint.position, entity.weaponC.firePoint.rotation);

                entity.weaponC.fireCountdown = 1f / entity.weaponC.fireRate;
            }

            entity.weaponC.fireCountdown -= deltatime;
        }
        
    }
}
