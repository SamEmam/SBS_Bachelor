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

            // Functionality
            if (weaponC.fireCountdown <= 0f && aimC.target && targetInRangeC.isInRange)
            {
                if (!weaponC.useLaser)
                {
                    ShootProjectile(weaponC.firePoints, weaponC.shotPrefab, aimC.target);
                }

                else                                                                                            // If uses laser
                {
                    EnableLaser(weaponC.lineRenderer, weaponC.laserPoint, weaponC.firePoints[0], aimC.target);
                }

                weaponC.fireCountdown = 1f / weaponC.fireRate;                                                  // Reset countdown to fireRate

            }

            else if (weaponC.useLaser)                                                                          // if not ready to fire, and uses laser
            {
                DisableLaser(weaponC.lineRenderer, weaponC.laserPoint);
            }
            
            if (weaponC.fireCountdown > 0)                                                                      // If fireCountdown is more than zero, then count down
            {
                weaponC.fireCountdown -= deltaTime;
            }
        }
    }

    void ShootProjectile(Transform[] firePoints, GameObject projectilePrefab, Transform target)
    {
        for (int j = 0; j < firePoints.Length; j++)
        {
            var shot = Object.Instantiate(projectilePrefab, firePoints[j].position, firePoints[j].rotation);        // For each firepoint, instantiate shot
            if (shot.GetComponent<RotationComponent>())
            {
                shot.GetComponent<RotationComponent>().target = target;                                             // If shot has rotation, set inherit target
            }
        }
    }

    void EnableLaser(LineRenderer laser, GameObject laserPoint, Transform firePoint, Transform target)
    {
        if (!laser.enabled)                                                     // If laser is disabled, enable laser
        {
            laserPoint.gameObject.SetActive(true);
            laser.enabled = true;
        }
        laserPoint.transform.parent = null;                                     // Set the laser point parent to null
        laserPoint.transform.position = target.position;                        // Set the laser point position to target position
        laser.SetPosition(0, firePoint.position);                               // Draw line from firePoint to laser point
        laser.SetPosition(1, laserPoint.transform.position);
    }

    void DisableLaser(LineRenderer laser, GameObject laserPoint)
    {
        if (laser.enabled)                                                      // if laser is enabled, disable laser
        {
            laser.enabled = false;
            laserPoint.SetActive(false);
        }
    }
}
