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

    // Attributes are temporary for hangar
    public Text smallGunCount, smallMissileCount, mediumGunCount, mediumMissileCount, largeGunCount, smallLaserCount, currencyText;

    private void Awake()
    {
        LoadFleet();
    }

    // This update method is temporary for hangar
    private void Update()
    {
        UpdateHangarText();
        
    }

    // This text method is temporary for hangar
    public void UpdateHangarText()
    {
        smallGunCount.text = "Small Gun Ship\n " + PlayerPrefs.GetInt("Fleet_" + 0).ToString();
        smallMissileCount.text = "Small Missile Ship\n " + PlayerPrefs.GetInt("Fleet_" + 1).ToString();
        mediumGunCount.text = "Medium Gun Ship\n " + PlayerPrefs.GetInt("Fleet_" + 2).ToString();
        mediumMissileCount.text = "Medium Missile Ship\n " + PlayerPrefs.GetInt("Fleet_" + 3).ToString();
        largeGunCount.text = "Large Gun Ship\n " + PlayerPrefs.GetInt("Fleet_" + 4).ToString();
        smallLaserCount.text = "Small Laser Ship\n " + PlayerPrefs.GetInt("Fleet_" + 5).ToString();

        currencyText.text = "Currency: " + PlayerPrefs.GetInt("Currency");
    }

    // This reload method is temporary for hangar
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddShip(int shipIndex)
    {
        if (PlayerPrefs.GetInt("Currency") >= shipCost[shipIndex])
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
