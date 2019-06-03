using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Entities;
using UnityEditor;


public class TargetSystemTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator _If_No_Other_Ships_Then_Target_Is_Null_Test()
    {
        var testPlayerGO = Instantiate((GameObject)Resources.Load("Tests/targetSystemTestPlayerGO"));

        float maxDist = 100f;
        testPlayerGO.GetComponent<TargetComponent>().Construct(maxDist);

        Assert.IsNull(testPlayerGO.GetComponent<RotationComponent>().target);

        yield return new WaitForSeconds(0.5f);

        Assert.IsNull(testPlayerGO.GetComponent<RotationComponent>().target);
    }

    [UnityTest]
    public IEnumerator _If_Other_Ship_Is_Same_Faction_Then_Target_Is_Null_Test()
    {
        var testPlayerGO = Instantiate((GameObject)Resources.Load("Tests/targetSystemTestPlayerGO"));
        var testEnemyGO = Instantiate((GameObject)Resources.Load("Tests/targetSystemTestEnemyGO"));

        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();

        EntityManager.SetComponentData(testEnemyGO.GetComponent<GameObjectEntity>().Entity, new FactionData { faction = FactionEnum.Player });

        float maxDist = 100f;
        testPlayerGO.GetComponent<TargetComponent>().Construct(maxDist);

        Assert.IsNull(testPlayerGO.GetComponent<RotationComponent>().target);

        yield return new WaitForSeconds(0.5f);

        Assert.IsNull(testPlayerGO.GetComponent<RotationComponent>().target);
    }

    [UnityTest]
    public IEnumerator _If_Enemy_Ship_Then_Target_Is_Enemy_Ship_Test()
    {
        var testPlayerGO = Instantiate((GameObject)Resources.Load("Tests/targetSystemTestPlayerGO"));
        var testEnemyGO = Instantiate((GameObject)Resources.Load("Tests/targetSystemTestEnemyGO"));
        

        float maxDist = 100f;
        testPlayerGO.GetComponent<TargetComponent>().Construct(maxDist);

        Assert.IsNull(testPlayerGO.GetComponent<RotationComponent>().target);

        yield return new WaitForSeconds(0.5f);

        Assert.AreEqual(testPlayerGO.GetComponent<RotationComponent>().target, testEnemyGO.transform);
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("TestObject"))
        {
            Destroy(gameObject);
        }

        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(gameObject);
        }

        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
