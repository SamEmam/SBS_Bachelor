using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class MovementSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _vectorMove_Object_Forward_If_movementSpeed_Is_More_Than_Zero_Test()
    {
        GameObject testObject = Instantiate((GameObject)Resources.Load("Tests/movementTestGO"));
        testObject.gameObject.tag = "TestObject";

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();

        EntityManager.SetComponentData(testObject.GetComponent<GameObjectEntity>().Entity, new MovementData { movementType = MovementType.vectorMovement ,movementSpeed = 1 });

        var initialPos = testObject.transform.position;


        yield return new WaitForSeconds(0.5f);
        
        var postUpdatePos = testObject.transform.position;
        Assert.AreEqual(initialPos.x, postUpdatePos.x);
        Assert.AreEqual(initialPos.y, postUpdatePos.y);
        Assert.AreNotEqual(initialPos.z, postUpdatePos.z);
    }

    [UnityTest]
    public IEnumerator _rigidbodyMove_Object_Forward_If_movementSpeed_Is_More_Than_Zero_Test()
    {
        GameObject testObject = Instantiate((GameObject)Resources.Load("Tests/movementTestGO"));
        testObject.gameObject.tag = "TestObject";

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();

        EntityManager.SetComponentData(testObject.GetComponent<GameObjectEntity>().Entity, new MovementData { movementType = MovementType.rigidbodyMovement, movementSpeed = 1 });

        var initialPos = testObject.transform.position;


        yield return new WaitForSeconds(0.5f);

        var postUpdatePos = testObject.transform.position;
        Assert.AreEqual(initialPos.x, postUpdatePos.x);
        Assert.AreEqual(initialPos.y, postUpdatePos.y);
        Assert.AreNotEqual(initialPos.z, postUpdatePos.z);
    }

    [UnityTest]
    public IEnumerator _Do_Not_Move_Object_When_movementSpeed_Is_Zero_Test()
    {
        GameObject testObject = Instantiate((GameObject)Resources.Load("Tests/movementTestGO"));
        testObject.gameObject.tag = "TestObject";

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();

        EntityManager.SetComponentData(testObject.GetComponent<GameObjectEntity>().Entity, new MovementData { movementType = MovementType.vectorMovement, movementSpeed = 0 });

        var initialPos = testObject.transform.position;


        yield return new WaitForSeconds(0.5f);

        var postUpdatePos = testObject.transform.position;
        Assert.AreEqual(initialPos.x, postUpdatePos.x);
        Assert.AreEqual(initialPos.y, postUpdatePos.y);
        Assert.AreEqual(initialPos.z, postUpdatePos.z);
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
