using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class AsteroidSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Instantiate_Asteroid_With_Random_Size_Test()
    {
        var asteroid = new GameObject().AddComponent<AsteroidComponent>();
        asteroid.gameObject.tag = "Asteroid";
        float minSize = 5f;
        float maxSize = 10f;
        asteroid.Construct(minSize, maxSize);
        asteroid.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var spawnedAsteroid = FindObjectOfType<AsteroidComponent>().transform;
        var compareAsteroid = new GameObject();
        compareAsteroid.tag = "Asteroid";
        Assert.AreNotEqual(spawnedAsteroid.localScale, compareAsteroid.transform.localScale);
    }
    [UnityTest]
    public IEnumerator _Instantiate_Asteroid_Without_Size_Change_If_Attributes_Are_1_Test()
    {
        var asteroid = new GameObject().AddComponent<AsteroidComponent>();
        asteroid.gameObject.tag = "Asteroid";
        float minSize = 1f;
        float maxSize = 1f;
        asteroid.Construct(minSize, maxSize);
        asteroid.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var spawnedAsteroid = FindObjectOfType<AsteroidComponent>().transform;
        var compareAsteroid = new GameObject();
        compareAsteroid.tag = "Asteroid";
        Assert.AreEqual(spawnedAsteroid.localScale, compareAsteroid.transform.localScale);
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
