using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class RotationSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Do_Not_Rotate_Object_If_rotationSpeed_Is_Zero_Test()
    {
        var testObject = new GameObject().AddComponent<RotationComponent>();
        testObject.gameObject.tag = "TestObject";
        testObject.gameObject.AddComponent<GameObjectEntity>();

        GameObject target = new GameObject();
        target.gameObject.tag = "TestObject";
        target.transform.position = new Vector3(10, 10, 10);
        float rotationSpeed = 0f;

        testObject.Construct(target.transform, rotationSpeed);

        var initialRot = testObject.transform.rotation;


        yield return null;

        var postUpdateRot = testObject.transform.rotation;
        
        Assert.AreEqual(initialRot, postUpdateRot);
    }

    [UnityTest]
    public IEnumerator _Do_Not_Rotate_Object_If_target_Is_Null_Test()
    {
        var testObject = new GameObject().AddComponent<RotationComponent>();
        testObject.gameObject.tag = "TestObject";
        testObject.gameObject.AddComponent<GameObjectEntity>();

        float rotationSpeed = 0f;

        testObject.Construct(null, rotationSpeed);

        var initialRot = testObject.transform.rotation;


        yield return null;

        var postUpdateRot = testObject.transform.rotation;
        
        Assert.AreEqual(initialRot, postUpdateRot);
    }

    [UnityTest]
    public IEnumerator _Rotate_Object_If_rotationSpeed_Is_More_Than_Zero_Test()
    {

        var testObject = new GameObject().AddComponent<RotationComponent>();
        testObject.gameObject.tag = "TestObject";
        testObject.gameObject.AddComponent<GameObjectEntity>();

        GameObject target = new GameObject();
        target.gameObject.tag = "TestObject";
        target.transform.position = new Vector3(10, 10, 10);
        float rotationSpeed = 1f;

        testObject.Construct(target.transform, rotationSpeed);

        var initialRot = testObject.transform.rotation;


        yield return new WaitForSeconds(0.5f);

        var postUpdateRot = testObject.transform.rotation;
        
        Assert.AreNotEqual(initialRot, postUpdateRot);
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
