using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInRangeComponent : MonoBehaviour
{
    public bool isInRange;
    public int minRange;
    public int maxRange;
    
    // Used for Unit Test Only
    public void Construct(int minRange, int maxRange)
    {
        this.minRange = minRange;
        this.maxRange = maxRange;
    }
}
