using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FleetManager : MonoBehaviour
{
    public SpawnComponent spawnC;
    public int[] shipCost;
    public int[] shipSell;

    // Attributes ONLY FOR TESTING
    public Text shipCount1, shipCount2, shipCount3, shipCount4, shipCount5, shipCount6, currencyText;

    private void Awake()
    {
        LoadFleet();
    }

    // This update method is ONLY FOR TESTING
    private void Update()
    {
        if (shipCount1)
        {
            shipCount1.text = "Ship 1: " + PlayerPrefs.GetInt("Fleet_" + 0).ToString();
            shipCount2.text = "Ship 2: " + PlayerPrefs.GetInt("Fleet_" + 1).ToString();
            shipCount3.text = "Ship 3: " + PlayerPrefs.GetInt("Fleet_" + 2).ToString();
            shipCount4.text = "Ship 4: " + PlayerPrefs.GetInt("Fleet_" + 3).ToString();
            shipCount5.text = "Ship 5: " + PlayerPrefs.GetInt("Fleet_" + 4).ToString();
            shipCount6.text = "Ship 6: " + PlayerPrefs.GetInt("Fleet_" + 5).ToString();

            currencyText.text = "Currency: " + PlayerPrefs.GetInt("Currency");
        }
        
    }

    // This reload method is ONLY FOR TESTING
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddShip(int shipIndex)
    {
        if (PlayerPrefs.GetInt("Currency") > shipCost[shipIndex])
        {
            int currency = PlayerPrefs.GetInt("Currency");
            PlayerPrefs.SetInt("Currency", currency - shipCost[shipIndex]);
            int currentShipCount = PlayerPrefs.GetInt("Fleet_" + shipIndex);
            PlayerPrefs.SetInt("Fleet_" + shipIndex, currentShipCount + 1);
        }
        
    }

    public void RemoveShip(int shipIndex)
    {
        int currentShipCount = PlayerPrefs.GetInt("Fleet_" + shipIndex);
        if (currentShipCount > 0)
        {
            PlayerPrefs.SetInt("Fleet_" + shipIndex, currentShipCount - 1);

            int currency = PlayerPrefs.GetInt("Currency");
            PlayerPrefs.SetInt("Currency", currency + shipSell[shipIndex]);
        }
        else
        {
            Debug.Log("Ship Count for ShipIndex is already 0");
        }
    }

    public void SaveFleet()
    {
        PlayerPrefs.SetInt("Fleet_count", spawnC.numberOfSpawns.Length);

        for (int i = 0; i < spawnC.numberOfSpawns.Length; i++)
        {
            PlayerPrefs.SetInt("Fleet_" + i, spawnC.numberOfSpawns[i]);
        }
    }

    public void LoadFleet()
    {
        PlayerPrefs.GetInt("Fleet_Count", spawnC.numberOfSpawns.Length);

        for (int i = 0; i < spawnC.numberOfSpawns.Length; i++)
        {
            spawnC.numberOfSpawns[i] = PlayerPrefs.GetInt("Fleet_" + i, 0);
        }
    }
}
