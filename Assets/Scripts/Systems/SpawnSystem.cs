using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnSystem : ComponentSystem
{
    struct Components
    {
        public GameObject[] spawnPrefab;
        public GameObject asteroidPrefab;
        public Vector3 asteroidSpawnPosition;
        public Vector3 spawnPosition;
        public int numberOfAsteroids;
    }

    [Inject] private Components components;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid(components.numberOfAsteroids);
    }

    private void SpawnAsteroid(int numberOfAsteroids)
    {
        var minSize = 0.2f;
        var maxSize = 1.5f;

        var minDistance = 3.0f;
        var maxDistance = 15.0f;

        for (var i = 0; i < numberOfAsteroids; i++)
        {
            var position = components.asteroidSpawnPosition;
            var size = Random.Range(minSize, maxSize);
            var prefab = components.asteroidPrefab;

            for (var j = 0; j < 100; j++)
            {
                position = Random.insideUnitSphere * (minDistance + (maxDistance - minDistance) * Random.value);
                position += components.asteroidSpawnPosition;
                if (!Physics.CheckSphere(position, size / 2.0f))
                {
                    break;
                }
            }
            Object.Instantiate(prefab, position, Random.rotation);
            //var instatiate = Instantiate(prefab, position, Random.rotation);
            //instatiate.transform.localScale = new Vector3(size, size, size);
        }
    }

    protected override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
