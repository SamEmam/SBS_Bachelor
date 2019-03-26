using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    public int targetScore;
    public int enemyScore = int.MaxValue;
    public int targetedBy;
    public int tempTargetedBy;
}
