using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public GameObject shotPrefab;
    public Transform firePoint;
}
