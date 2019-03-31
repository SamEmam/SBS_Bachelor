using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LifespanSystem : ComponentSystem
{

    struct Components
    {
        public readonly int Length;
        public ComponentArray<LifespanComponent> lifespanC;
        public ComponentArray<Rigidbody> rigidbody;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        var deltatime = Time.deltaTime;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var lifespanC = components.lifespanC[i];
            var rigidbody = components.rigidbody[i];
            var transform = components.transform[i];

            // Functionality
            if (lifespanC)
            {
                if (lifespanC.lifespan <= 0)
                {
                    // If object contains explosionPrefab, initiale and give velocity
                    if (lifespanC.explosionPrefab)
                    {
                        var explosion = Object.Instantiate(lifespanC.explosionPrefab, transform.position, transform.rotation);
                        var explosionRB = explosion.GetComponent<Rigidbody>();
                        explosionRB.velocity = rigidbody.velocity;
                        explosionRB.angularVelocity = Vector3.zero;

                        //// If object contains particles, split them and destroy them when they're clear of particles
                        //if (lifespanC.ObjectParticles)
                        //{
                        //    var particles = lifespanC.ObjectParticles;
                        //    particles.transform.parent = null;
                        //    var emit = particles.emission;
                        //    emit.rateOverTime = 0;
                        //    if (!particles.IsAlive())
                        //    {
                        //        Object.Destroy(particles);
                        //    }
                        //}


                    }
                    Object.Destroy(lifespanC.gameObject);
                    
                }
                else
                {
                    lifespanC.lifespan -= deltatime;
                }
            }

        }
    }
}
