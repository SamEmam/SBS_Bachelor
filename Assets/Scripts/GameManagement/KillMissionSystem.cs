using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateAfter(typeof(ShipCounterSystem))]
public class KillMissionSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<ShipCounterComponent> ShipCounterC;
        public ComponentArray<KillMissionComponent> KillMissionC;
        public ComponentArray<GameManager> GameManager;
        public EntityArray entities;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var shipCounterC = components.ShipCounterC[i];
            var killMissionC = components.KillMissionC[i];
            var gameManager = components.GameManager[i];

            // Functionality
            if (killMissionC.cooldownBeforeSystemStart > 0)
            {
                killMissionC.cooldownBeforeSystemStart -= Time.deltaTime;
                return;
            }
            if (shipCounterC.playersAlive <= 0)
            {
                killMissionC.cooldownBeforeSystemStart = float.MaxValue;
                gameManager.GameIsLost = true;
                Debug.Log("Lost: " + gameManager.GameIsLost);
                Debug.Log("Won: " + gameManager.GameIsWon);
            }
            else if (shipCounterC.enemiesAlive <= 0)
            {
                killMissionC.cooldownBeforeSystemStart = float.MaxValue;
                gameManager.GameIsWon = true;
                Debug.Log("Won: " + gameManager.GameIsWon);
                Debug.Log("Lost: " + gameManager.GameIsLost);

            }

        }
    }
}