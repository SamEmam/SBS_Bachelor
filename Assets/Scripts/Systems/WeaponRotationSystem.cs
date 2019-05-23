using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateAfter(typeof(UnityEngine.Experimental.PlayerLoop.FixedUpdate))]
public class WeaponRotationSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<AimComponent> aimC;
        public ComponentArray<TargetInRangeComponent> targetInRangeC;
        public ComponentArray<WeaponRotationComponent> weaponRotationC;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;


        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var aimC = components.aimC[i];
            var targetInRangeC = components.targetInRangeC[i];
            var weaponRotationC = components.weaponRotationC[i];
            var transform = components.transform[i];

            // Functionality
            if (aimC.parentRotationComponent.target)
            {
                aimC.target = aimC.parentRotationComponent.target;                                                                      // Update target to match parent target
            }

            if (targetInRangeC.isInRange && aimC.target)                                                                                // If target is not null and in range
            {
                var pos = aimC.target.position - transform.position;                                                                    // Get position of target if position of this transform was (0, 0, 0)
                Quaternion targetRotation = Quaternion.LookRotation(pos);                                                               // Set the rotation from (0, 0, 0) to pos
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, weaponRotationC.rotationSpeed * deltaTime);   // Rotate towards target rotation over time
            }
        }
    }
}
