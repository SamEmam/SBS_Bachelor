using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsCleanup : MonoBehaviour
{
    // Reset PlayerPrefs
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
    
}
