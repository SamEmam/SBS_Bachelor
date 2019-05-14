using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class WaypointSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _If_Distance_To_Waypoint_Is_More_Than_maxDist_Then_Target_Waypoint_Test()
    {
        var testObject = new GameObject().AddComponent<RotationComponent>();
        testObject.tag = "TestObject";
        Transform targetTransform = testObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        //targetTransform.position = targetTransform.forward * 10;

        var testWaypoint = new GameObject("playerWaypoint");
        testWaypoint.tag = "TestObject";
        testWaypoint.transform.position = new Vector3(10, 10, 10);

        int maxDistFromWaypoint = 0;
        testObject.gameObject.AddComponent<WaypointComponent>().Construct(maxDistFromWaypoint, WaypointEnum.playerWaypoint);
        testObject.gameObject.AddComponent<TargetComponent>();

        testObject.gameObject.AddComponent<GameObjectEntity>();
        

        Assert.False(testObject.target == testWaypoint.transform);

        yield return null;

        Assert.True(testObject.target == testWaypoint.transform);
    }

    [UnityTest]
    public IEnumerator _If_Distance_To_Waypoint_Is_Less_Than_maxDist_Then_Target_Waypoint_Test()
    {
        var testObject = new GameObject().AddComponent<RotationComponent>();
        testObject.tag = "TestObject";
        Transform targetTransform = testObject.GetComponent<RotationComponent>().target = new GameObject().transform;

        var testWaypoint = new GameObject("playerWaypoint");
        testWaypoint.tag = "TestObject";
        testWaypoint.transform.position = new Vector3(10, 10, 10);
        
        int maxDistFromWaypoint = 100;
        testObject.gameObject.AddComponent<WaypointComponent>().Construct(maxDistFromWaypoint, WaypointEnum.playerWaypoint);
        testObject.gameObject.AddComponent<TargetComponent>();

        testObject.gameObject.AddComponent<GameObjectEntity>();
        
        Assert.False(testObject.target == testWaypoint.transform);

        yield return null;

        Assert.False(testObject.target == testWaypoint.transform);
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
