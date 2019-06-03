using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefsTool : MonoBehaviour
{
    public int spaceshipCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("ShipCount", spaceshipCount);
    }
}
