using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    public void SaveGame()
    {
        Debug.Log("Saving Game!");
        //PlayerPrefs.SetInt("Currency", 0);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Currency", 500);
    }

    public void AddCurrency()
    {
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 1000);
    }
}
