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
        float rotationSpeed = 10f;


        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var weaponC = components.weaponC[i];
            var aimC = components.aimC[i];
            var targetInRangeC = components.targetInRangeC[i];
            var transform = components.transform[i];

            // Functionality
            if (targetInRangeC.isInRange && aimC.target)
            {
                var pos = aimC.target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(pos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);

                

                if (weaponC.fireCountdown <= 0f)
                {
                    if (!weaponC.useLaser)
                    {
                        for (int j = 0; j < weaponC.firePoints.Length; j++)
                        {
                            var shot = Object.Instantiate(weaponC.shotPrefab, weaponC.firePoints[j].position, weaponC.firePoints[j].rotation);
                            if (shot.GetComponent<RotationComponent>())
                            {
                                shot.GetComponent<RotationComponent>().target = aimC.target;
                            }
                        }
                    }

                    else
                    {
                        if (!weaponC.lineRenderer.enabled)
                        {
                            weaponC.lineRenderer.enabled = true;
                        }
                        weaponC.laserPoint.transform.parent = null;
                        weaponC.laserPoint.transform.position = aimC.target.position;
                        weaponC.lineRenderer.SetPosition(0, weaponC.firePoints[0].position);
                        weaponC.lineRenderer.SetPosition(1, weaponC.laserPoint.transform.position);
                        

                    }
                    weaponC.fireCountdown = 1f / weaponC.fireRate;

                }
                
            }
            else if (weaponC.useLaser)
            {
                if (weaponC.lineRenderer.enabled)
                {
                    weaponC.lineRenderer.enabled = false;
                    weaponC.laserPoint.transform.localPosition = Vector3.zero;
                }
            }


            if (weaponC.fireCountdown > 0)
            {
                weaponC.fireCountdown -= deltaTime;
            }
        }
    }
}
