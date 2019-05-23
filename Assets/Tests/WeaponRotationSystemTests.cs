using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class WeaponRotationSystemTests : MonoBehaviour
{

    [UnityTest]
    public IEnumerator _If_targetInRange_And_Target_Is_Not_Null_Then_Rotate_Towards_Target_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestWeaponGO"));

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = targetTransform.right * 10;

        testWeaponObject.transform.SetParent(testParentObject.transform);

        Quaternion initialRotation = testWeaponObject.transform.rotation;

        yield return new WaitForSeconds(0.5f);

        Quaternion postRotation = testWeaponObject.transform.rotation;

        Assert.AreNotEqual(initialRotation, postRotation);
        Assert.AreNotEqual(postRotation, testParentObject.transform.rotation);
    }

    [UnityTest]
    public IEnumerator _If_Target_Is_Null_Then_Do_Not_Rotate_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestWeaponGO"));

        testWeaponObject.transform.SetParent(testParentObject.transform);

        Quaternion initialRotation = testWeaponObject.transform.rotation;

        yield return new WaitForSeconds(0.5f);

        Quaternion postRotation = testWeaponObject.transform.rotation;

        Assert.AreEqual(initialRotation, postRotation);
        Assert.AreEqual(postRotation, testParentObject.transform.rotation);
    }

    [UnityTest]
    public IEnumerator _If_Target_Not_In_Range_Do_Not_Rotate_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestWeaponGO"));

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = targetTransform.forward * 10;

        var minRange = 0;
        var maxRange = 1;

        testWeaponObject.GetComponent<TargetInRangeComponent>().Construct(minRange, maxRange);

        testWeaponObject.transform.SetParent(testParentObject.transform);

        Quaternion initialRotation = testWeaponObject.transform.rotation;

        yield return new WaitForSeconds(0.5f);

        Quaternion postRotation = testWeaponObject.transform.rotation;

        Assert.AreEqual(initialRotation, postRotation);
        Assert.AreEqual(postRotation, testParentObject.transform.rotation);
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
