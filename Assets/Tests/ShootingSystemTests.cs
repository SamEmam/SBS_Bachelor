using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;

public class ShootingSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _If_FireCountdown_Is_More_Than_Zero_It_Counts_Down_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestWeaponGO"));

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = targetTransform.right * 10;
        testWeaponObject.transform.SetParent(testParentObject.transform);

        float fireRate = 1f;
        float fireCountdown = 5f;
        bool useLaser = false;
        testWeaponObject.GetComponent<WeaponComponent>().Construct(fireRate, fireCountdown, useLaser);

        float initialFireCountdown = testWeaponObject.GetComponent<WeaponComponent>().fireCountdown;

        yield return null;

        float postFireCountdown = testWeaponObject.GetComponent<WeaponComponent>().fireCountdown;

        Assert.AreNotEqual(initialFireCountdown, postFireCountdown);
    }

    [UnityTest]
    public IEnumerator _If_targetInRange_And_Target_Is_Not_Null_Then_Rotate_Towards_Target_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestWeaponGO"));

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = targetTransform.right * 10;
        testWeaponObject.transform.SetParent(testParentObject.transform);

        float fireRate = 1f;
        float fireCountdown = 5f;
        bool useLaser = false;
        testWeaponObject.GetComponent<WeaponComponent>().Construct(fireRate, fireCountdown, useLaser);

        Quaternion initialRotation = testWeaponObject.transform.rotation;

        yield return new WaitForSeconds(0.5f);

        Quaternion postRotation = testWeaponObject.transform.rotation;

        Assert.AreNotEqual(initialRotation, postRotation);
        Assert.AreNotEqual(postRotation, testParentObject.transform.rotation);
    }

    [UnityTest]
    public IEnumerator _If_targetInRange_And_Target_Is_Not_Null_And_fireCountdown_Is_Zero_Then_Instantiate_Shot_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestWeaponGO"));

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = targetTransform.right * 10;
        testWeaponObject.transform.SetParent(testParentObject.transform);

        float fireRate = 1f;
        float fireCountdown = 0f;
        bool useLaser = false;
        testWeaponObject.GetComponent<WeaponComponent>().Construct(fireRate, fireCountdown, useLaser);
        
        yield return new WaitForSeconds(0.5f);
        
        Assert.NotNull(GameObject.FindGameObjectWithTag("Shot"));
    }

    [UnityTest]
    public IEnumerator _If_targetInRange_And_Target_Is_Not_Null_And_fireCountdown_Is_Zero_Then_Draw_Laser_Test()
    {
        var testParentObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestParentGO"));
        var testWeaponObject = Instantiate((GameObject)Resources.Load("Tests/shootingTestWeaponGO"));

        Transform targetTransform = testParentObject.GetComponent<RotationComponent>().target = new GameObject().transform;
        targetTransform.position = targetTransform.right * 10;
        testWeaponObject.transform.SetParent(testParentObject.transform);

        float fireRate = 1f;
        float fireCountdown = 0f;
        bool useLaser = true;
        testWeaponObject.GetComponent<WeaponComponent>().Construct(fireRate, fireCountdown, useLaser);
        
        Assert.False(testWeaponObject.GetComponent<LineRenderer>().enabled);

        yield return new WaitForSeconds(0.5f);

        Assert.True(testWeaponObject.GetComponent<LineRenderer>().enabled);
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
