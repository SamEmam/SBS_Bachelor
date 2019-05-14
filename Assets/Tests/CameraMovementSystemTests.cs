using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class CameraMovementSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Camera_Move_If_Height_Is_Zero_Test()
    {
        var cameraController = new GameObject().AddComponent<InputComponent>();
        cameraController.gameObject.tag = "TestObject";
        var initialPos = cameraController.transform.position;
        cameraController.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var postUpdatePos = cameraController.transform.position;
        Assert.AreNotEqual(initialPos, postUpdatePos);
    }

    [UnityTest]
    public IEnumerator _Camera_Move_If_Height_Is_100_Test()
    {
        var cameraController = new GameObject().AddComponent<InputComponent>();
        cameraController.gameObject.tag = "TestObject";
        cameraController.transform.position = new Vector3(0, 100, 0);
        var initialPos = cameraController.transform.position;
        cameraController.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var postUpdatePos = cameraController.transform.position;
        Assert.AreNotEqual(initialPos, postUpdatePos);
    }

    [UnityTest]
    public IEnumerator _Camera_Horizontal_Move_Test()
    {
        var cameraController = new GameObject().AddComponent<InputComponent>();
        cameraController.gameObject.tag = "TestObject";
        cameraController.transform.position = new Vector3(0, 25, 0);
        var initialPos = cameraController.transform.position;
        float horizontal = 1f;
        float vertical = 0f;
        float scroll = 0f;
        cameraController.Construct(horizontal, vertical, scroll);
        cameraController.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var postUpdatePos = cameraController.transform.position;

        Assert.AreNotEqual(initialPos.x, postUpdatePos.x);
        Assert.AreEqual(initialPos.y, postUpdatePos.y);
        Assert.AreEqual(initialPos.z, postUpdatePos.z);
    }

    [UnityTest]
    public IEnumerator _Camera_Vertical_Move_Test()
    {
        var cameraController = new GameObject().AddComponent<InputComponent>();
        cameraController.gameObject.tag = "TestObject";
        cameraController.transform.position = new Vector3(0, 25, 0);
        var initialPos = cameraController.transform.position;
        float horizontal = 0f;
        float vertical = 1f;
        float scroll = 0f;
        cameraController.Construct(horizontal, vertical, scroll);
        cameraController.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var postUpdatePos = cameraController.transform.position;

        Assert.AreEqual(initialPos.x, postUpdatePos.x);
        Assert.AreEqual(initialPos.y, postUpdatePos.y);
        Assert.AreNotEqual(initialPos.z, postUpdatePos.z);
    }

    [UnityTest]
    public IEnumerator _Camera_Scroll_Move_Test()
    {
        var cameraController = new GameObject().AddComponent<InputComponent>();
        cameraController.gameObject.tag = "TestObject";
        cameraController.transform.position = new Vector3(0, 30, 0);
        var initialPos = cameraController.transform.position;
        float horizontal = 0f;
        float vertical = 0f;
        float scroll = 1f;
        cameraController.Construct(horizontal, vertical, scroll);
        cameraController.gameObject.AddComponent<GameObjectEntity>();

        yield return null;

        var postUpdatePos = cameraController.transform.position;

        Assert.AreEqual(initialPos.x, postUpdatePos.x);
        Assert.AreNotEqual(initialPos.y, postUpdatePos.y);
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
