﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class DeathSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Object_Destroyed_If_Health_Is_Zero_Test()
    {
        var deadObject = new GameObject().AddComponent<DeathComponent>();
        deadObject.gameObject.tag = "TestObject";
        deadObject.gameObject.AddComponent<HealthComponent>();
        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();

        EntityManager.SetComponentData(deadObject.GetComponent<GameObjectEntity>().Entity, new DeathData { deathState = DeathEnum.Alive });
        EntityManager.SetComponentData(deadObject.GetComponent<GameObjectEntity>().Entity, new HealthData { health = 0 });

        yield return new WaitForSeconds(0.5f);

        Assert.False(deadObject);
    }

    [UnityTest]
    public IEnumerator _Object_Not_Destroyed_If_Health_Is_More_Than_Zero_Test()
    {
        var deadObject = new GameObject().AddComponent<DeathComponent>();
        deadObject.gameObject.tag = "TestObject";
        deadObject.gameObject.AddComponent<HealthComponent>();
        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();

        EntityManager.SetComponentData(deadObject.GetComponent<GameObjectEntity>().Entity, new DeathData { deathState = DeathEnum.Alive });
        EntityManager.SetComponentData(deadObject.GetComponent<GameObjectEntity>().Entity, new HealthData { health = 1 });

        yield return new WaitForSeconds(0.5f);

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
