using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateAfter(typeof(SpawnSystem))]
public class ShipCounterSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<ShipCounterComponent> ShipCounterC;
        public EntityArray entities;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var ShipCounterC = components.ShipCounterC[i];

            // Functionality
            ShipCounterC.playersAlive = GameObject.FindGameObjectsWithTag(ShipCounterC.playerTag).Length;
            ShipCounterC.enemiesAlive = GameObject.FindGameObjectsWithTag(ShipCounterC.enemyTag).Length;

            ShipCounterC.playersText.text = "Players Alive: " + ShipCounterC.playersAlive;
            ShipCounterC.enemiesText.text = "Enemies Alive: " + ShipCounterC.enemiesAlive;
            
        }
    }
}