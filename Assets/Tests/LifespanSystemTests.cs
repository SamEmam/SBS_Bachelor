using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class LifespanSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Object_Destroyed_If_Lifespan_Is_Zero_Test()
    {
        GameObject lifespanObject = new GameObject();
        lifespanObject.tag = "TestObject";
        lifespanObject.AddComponent<Rigidbody>();
        lifespanObject.AddComponent<LifespanComponent>();
        lifespanObject.AddComponent<GameObjectEntity>();
        float lifespan = 0f;
        bool lifeHasEnded = false;
        lifespanObject.GetComponent<LifespanComponent>().Construct(lifespan, lifeHasEnded);

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();
        Assert.True(lifespanObject);
        
        yield return new WaitForSeconds(0.5f);
        
        Assert.False(lifespanObject);
    }

    [UnityTest]
    public IEnumerator _Object_Not_Destroyed_After_1frame_If_Lifespan_Is_Five_test()
    {
        GameObject lifespanObject = new GameObject();
        lifespanObject.tag = "TestObject";
        lifespanObject.AddComponent<Rigidbody>();
        lifespanObject.AddComponent<LifespanComponent>();
        lifespanObject.AddComponent<GameObjectEntity>();
        float lifespan = 5f;
        bool lifeHasEnded = false;
        lifespanObject.GetComponent<LifespanComponent>().Construct(lifespan, lifeHasEnded);

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();
        Assert.True(lifespanObject);

        yield return new WaitForSeconds(0.5f);

        Assert.True(lifespanObject);
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
