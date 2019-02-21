using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

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
                Object.Instantiate(entity.weaponC.shotPrefab, entity.weaponC.firePoint.position, entity.weaponC.firePoint.rotation);

                entity.weaponC.fireCountdown = 1f / entity.weaponC.fireRate;
            }

            entity.weaponC.fireCountdown -= deltatime;
        }
        
    }
}
