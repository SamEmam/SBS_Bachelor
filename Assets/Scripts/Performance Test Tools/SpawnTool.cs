using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTool : MonoBehaviour
{
    public GameObject[] player, enemy;
    public int amountOfShips;
    public int minRange, maxRange;
    public Transform spawnpos;
    public int shipCounter;


    // Start is called before the first frame update
    void Start()
    {
        amountOfShips = PlayerPrefs.GetInt("ShipCount");

        for (int i = 0; i < amountOfShips / 12; i++)
        {
            foreach (var ship in player)
            {
                spawnpos.position = Random.insideUnitSphere * (minRange + (maxRange - minRange)) + transform.position;
                Instantiate(ship, spawnpos.position, Random.rotation);
                shipCounter++;
            }
            foreach (var ship in enemy)
            {
                spawnpos.position = Random.insideUnitSphere * (minRange + (maxRange - minRange)) + transform.position;
                Instantiate(ship, spawnpos.position, Random.rotation);
                shipCounter++;
            }
        }
    }
}
