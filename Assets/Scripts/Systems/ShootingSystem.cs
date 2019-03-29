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
        public readonly int Length;
        public ComponentArray<WeaponComponent> weaponC;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        var deltatime = Time.deltaTime;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var weaponC = components.weaponC[i];

            // Functionality
            if (weaponC.fireCountdown <= 0f)
            {
                var shot = Object.Instantiate(weaponC.shotPrefab, weaponC.firePoint.position, weaponC.firePoint.rotation);

                weaponC.fireCountdown = 1f / weaponC.fireRate;
            }

            weaponC.fireCountdown -= deltatime;
        }
    }
}
