using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    public int targetScore;
    public int enemyScore = int.MaxValue;
    public int targetedBy;
    public int tempTargetedBy;
    public bool isCloseEnoughToWaypoint;
    public float maxDistance = 100f;

    // Used for Unit Testing Only
    public void Construct(float maxDistance)
    {
        this.maxDistance = maxDistance;
    }
}
