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
    public IEnumerator _Object_Destroyed_If_DeathComponent_Is_Added_Test()
    {
        GameObject deadObject = new GameObject();
        deadObject.gameObject.tag = "TestObject";

        var entity = deadObject.AddComponent<GameObjectEntity>();

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();
        
        EntityManager.AddSharedComponentData(entity.Entity, new DeathData { });

        Assert.True(deadObject);

        yield return null;

        Assert.False(deadObject);
    }

    [UnityTest]
    public IEnumerator _Object_Not_Destroyed_If_DeathComponent_Is_Not_Added_Test()
    {
        GameObject deadObject = new GameObject();
        deadObject.gameObject.tag = "TestObject";

        var entity = deadObject.AddComponent<GameObjectEntity>();

        Assert.True(deadObject);

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
