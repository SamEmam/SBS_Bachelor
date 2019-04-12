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
        public ComponentArray<AimComponent> aimC;
        public ComponentArray<TargetInRangeComponent> targetInRangeC;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;
    
    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var weaponC = components.weaponC[i];
            var aimC = components.aimC[i];
            var targetInRangeC = components.targetInRangeC[i];
            var transform = components.transform[i];

            // Functionality
            if (targetInRangeC.isInRange)
            {
                var pos = aimC.target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(pos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * deltaTime);

                if (weaponC.fireCountdown <= 0f)
                {
                    for (int j = 0; j < weaponC.firePoints.Length; j++)
                    {
                        var shot = Object.Instantiate(weaponC.shotPrefab, weaponC.firePoints[j].position, weaponC.firePoints[j].rotation);
                        if (shot.GetComponent<RotationComponent>())
                        {
                            shot.GetComponent<RotationComponent>().target = aimC.target;
                        }
                    }
                    weaponC.fireCountdown = 1f / weaponC.fireRate;
                }
                
            }

            

            weaponC.fireCountdown -= deltaTime;



            
        }
    }
}
