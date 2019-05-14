using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointComponent : MonoBehaviour
{
    public Transform waypoint;
    public int maxDistFromWaypoint = 150;
    public ParticleSystem waypointParticles;
    public WaypointEnum waypointEnum;

    private void Awake()
    {
        waypoint = GameObject.Find(waypointEnum.ToString()).transform;
        waypointParticles = waypoint.GetComponent<ParticleSystem>();
    }

    public void Construct(int maxDistFromWaypoint, WaypointEnum waypointEnum)
    {
        this.maxDistFromWaypoint = maxDistFromWaypoint;
        this.waypointEnum = waypointEnum;
    }
}

public enum WaypointEnum
{
    playerWaypoint,
    enemyWaypoint
}
