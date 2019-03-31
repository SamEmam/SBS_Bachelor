using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointComponent : MonoBehaviour
{
    public Transform waypoint;
    public int maxDistFromWaypoint;

    private void Awake()
    {
        waypoint = GameObject.Find("waypoint").transform;
    }
}
