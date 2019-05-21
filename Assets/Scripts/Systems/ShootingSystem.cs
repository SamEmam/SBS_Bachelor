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

            // Functionality
            if (weaponC.fireCountdown <= 0f)
            {
                if (!weaponC.useLaser)
                {
                    for (int j = 0; j < weaponC.firePoints.Length; j++)
                    {
                        var shot = Object.Instantiate(weaponC.shotPrefab, weaponC.firePoints[j].position, weaponC.firePoints[j].rotation);          // For each firepoint, instantiate shot
                        if (shot.GetComponent<RotationComponent>())
                        {
                            shot.GetComponent<RotationComponent>().target = aimC.target;                                                            // If shot has rotation, set inherit target
                        }
                    }
                }

                else                                                                                    // If uses laser
                {
                    if (!weaponC.lineRenderer.enabled)                                                  // If laser is disabled, enable laser
                    {
                        weaponC.laserPoint.gameObject.SetActive(true);
                        weaponC.lineRenderer.enabled = true;
                    }
                    weaponC.laserPoint.transform.parent = null;                                         // Set the laser point parent to null
                    weaponC.laserPoint.transform.position = aimC.target.position;                       // Set the laser point position to target position
                    weaponC.lineRenderer.SetPosition(0, weaponC.firePoints[0].position);                // Draw line from firePoint to laser point
                    weaponC.lineRenderer.SetPosition(1, weaponC.laserPoint.transform.position);


                }
                weaponC.fireCountdown = 1f / weaponC.fireRate;                                          // Reset countdown to fireRate

            }

            else if (weaponC.useLaser)                                                                  // if not ready to fire, and uses laser
            {
                if (weaponC.lineRenderer.enabled)                                                       // if laser is enabled, disable laser
                {
                    weaponC.lineRenderer.enabled = false;
                    weaponC.laserPoint.gameObject.SetActive(false);
                }
            }


            if (weaponC.fireCountdown > 0)                                                              // If fireCountdown is more than zero, then count down
            {
                weaponC.fireCountdown -= deltaTime;
            }
        }
    }
    
}
