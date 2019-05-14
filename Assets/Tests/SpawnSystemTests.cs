using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;


public class SpawnSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Instantiate_Asteroid_From_Prefab_With_SpawnSystem_Test()
    {
        var spawnPrefab = Resources.Load("Tests/asteroid");
        var spawner = new GameObject().AddComponent<SpawnComponent>();
        spawner.gameObject.tag = "TestObject";
        int minDistance = 0;
        int maxDistance = 1;
        spawner.Construct((GameObject)spawnPrefab, 1, minDistance, maxDistance);
        spawner.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var spawnedAsteroid = GameObject.FindWithTag("Asteroid");

        Assert.NotNull(spawnedAsteroid);
    }

    [UnityTest]
    public IEnumerator _Instantiate_Asteroid_At_Random_Position_Within_MinDistance_And_MaxDistance_Test()
    {
        var spawnPrefab = Resources.Load("Tests/asteroid");
        var spawner = new GameObject().AddComponent<SpawnComponent>();
        spawner.gameObject.tag = "TestObject";
        int minDistance = 5;
        int maxDistance = 10;
        spawner.Construct((GameObject)spawnPrefab, 1, minDistance, maxDistance);
        spawner.gameObject.AddComponent<GameObjectEntity>();

        yield return new WaitForSeconds(0.5f);

        var spawnedAsteroid = GameObject.FindWithTag("Asteroid");

        Assert.True(!Physics.CheckSphere(Vector3.zero, minDistance) && Physics.CheckSphere(Vector3.zero, maxDistance));
    }

    [UnityTest]
    public IEnumerator _Instantiate_Asteroid_At_Random_Position_Outside_MinDistance_And_MaxDistance_Test()
    {
        var spawnPrefab = Resources.Load("Tests/asteroid");
        var spawner = new GameObject().AddComponent<SpawnComponent>();
        spawner.gameObject.tag = "TestObject";
        int minDistance = 0;
        int maxDistance = 4;
        spawner.Construct((GameObject)spawnPrefab, 1, minDistance, maxDistance);
        spawner.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var spawnedAsteroid = GameObject.FindWithTag("Asteroid");
        minDistance = 5;
        maxDistance = 10;
        Assert.False(!Physics.CheckSphere(Vector3.zero, minDistance) && Physics.CheckSphere(Vector3.zero, maxDistance));
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(gameObject);
        }
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("TestObject"))
        {
            Destroy(gameObject);
        }
    }
}


