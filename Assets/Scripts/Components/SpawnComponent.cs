using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    public GameObject[] prefabs;
    public int[] numberOfSpawns;
    public float minDistance;
    public float maxDistance;

    // For Unit Testing Only
    public void Construct(GameObject prefabs, int numberOfSpawns, float minDistance, float maxDistance)
    {
        var prefabArray = new GameObject[1];
        prefabArray[0] = prefabs;
        var numbOfSpawnArray = new int[1];
        numbOfSpawnArray[0] = numberOfSpawns;

        this.prefabs = prefabArray;
        this.numberOfSpawns = numbOfSpawnArray;
        this.minDistance = minDistance;
        this.maxDistance = maxDistance;
    }
}