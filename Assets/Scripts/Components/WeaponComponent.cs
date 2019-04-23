using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    [Header("General")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public GameObject shotPrefab;
    public Transform[] firePoints;

    [Header("Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public GameObject laserPoint;
}
