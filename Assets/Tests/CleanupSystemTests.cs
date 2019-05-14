using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class CleanupSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Object_Destroyed_If_Deathstate_Is_Dead_Test()
    {
        var deadObject = new GameObject().AddComponent<DeathComponent>();
        deadObject.gameObject.tag = "TestObject";

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();
        
        EntityManager.SetComponentData(deadObject.GetComponent<GameObjectEntity>().Entity, new DeathData { deathState = DeathEnum.Dead });
        
        yield return null;

        Assert.False(deadObject);
    }

    [UnityTest]
    public IEnumerator _Object_Not_Destroyed_If_Deathstate_Is_Alive_Test()
    {
        var deadObject = new GameObject().AddComponent<DeathComponent>();
        deadObject.gameObject.tag = "TestObject";

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();

        EntityManager.SetComponentData(deadObject.GetComponent<GameObjectEntity>().Entity, new DeathData { deathState = DeathEnum.Alive });

        yield return null;

        Assert.True(deadObject);
    }


    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("TestObject"))
        {
            Destroy(gameObject);
        }
    }
}
