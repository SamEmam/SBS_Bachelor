using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;


[Serializable]
public struct HealthData : IComponentData
{
    public int health;
}

public class HealthComponent : ComponentDataProxy<HealthData>
{

}
