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
        var lifespanObject = new GameObject().AddComponent<LifespanComponent>();
        lifespanObject.gameObject.tag = "TestObject";
        lifespanObject.gameObject.AddComponent<Rigidbody>();
        lifespanObject.gameObject.AddComponent<DeathComponent>();
        float lifespan = 0f;
        bool lifeHasEnded = false;
        lifespanObject.GetComponent<LifespanComponent>().Construct(lifespan, lifeHasEnded);

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();
        Assert.AreEqual(EntityManager.GetComponentData<DeathData>(lifespanObject.GetComponent<GameObjectEntity>().Entity).deathState, DeathEnum.Alive);
        
        yield return new WaitForSeconds(0.5f);
        
        Assert.False(lifespanObject);
    }

    [UnityTest]
    public IEnumerator _Object_Not_Destroyed_After_1frame_If_Lifespan_Is_Five_test()
    {
        var lifespanObject = new GameObject().AddComponent<LifespanComponent>();
        lifespanObject.gameObject.tag = "TestObject";
        lifespanObject.gameObject.AddComponent<Rigidbody>();
        lifespanObject.gameObject.AddComponent<DeathComponent>();
        float lifespan = 5f;
        bool lifeHasEnded = false;
        lifespanObject.GetComponent<LifespanComponent>().Construct(lifespan, lifeHasEnded);

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();
        Assert.AreEqual(EntityManager.GetComponentData<DeathData>(lifespanObject.GetComponent<GameObjectEntity>().Entity).deathState, DeathEnum.Alive);

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
