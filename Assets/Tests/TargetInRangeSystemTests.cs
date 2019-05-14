using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class TargetInRangeSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _Target_Is_In_Range_If_Distance_Is_Less_Than_maxDistance_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/targetInRangeTestWeaponGO"));
        
        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = Vector3.forward;
        targetTransform.tag = "TestObject";
        testWeaponObject.transform.SetParent(testParentObject.transform);

        Assert.False(testWeaponObject.GetComponent<TargetInRangeComponent>().isInRange);
        
        yield return new WaitForSeconds(0.5f);
        
        Assert.True(testWeaponObject.GetComponent<TargetInRangeComponent>().isInRange);
    }

    [UnityTest]
    public IEnumerator _Target_Is_Not_In_Range_If_Distance_Is_More_Than_maxDistance_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/targetInRangeTestWeaponGO"));

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = Vector3.forward * 200;
        targetTransform.tag = "TestObject";
        testWeaponObject.transform.SetParent(testParentObject.transform);

        Assert.False(testWeaponObject.GetComponent<TargetInRangeComponent>().isInRange);

        yield return new WaitForSeconds(0.5f);

        Assert.False(testWeaponObject.GetComponent<TargetInRangeComponent>().isInRange);
    }

    [UnityTest]
    public IEnumerator _Target_Is_Not_In_Range_If_Angle_Is_Outside_Range_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/targetInRangeTestWeaponGO"));

        int minRange = 0;
        int maxRange = 1;

        testWeaponObject.GetComponent<TargetInRangeComponent>().Construct(minRange, maxRange);

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = new Vector3(-1,-1,-1);
        targetTransform.tag = "TestObject";
        testWeaponObject.transform.SetParent(testParentObject.transform);

        Assert.False(testWeaponObject.GetComponent<TargetInRangeComponent>().isInRange);

        yield return new WaitForSeconds(0.5f);

        Assert.False(testWeaponObject.GetComponent<TargetInRangeComponent>().isInRange);
    }


    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("TestObject"))
        {
            Destroy(gameObject);
        }

        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Shot"))
        {
            Destroy(gameObject);
        }
    }
}
