using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;


[Serializable]
public struct DamageData : IComponentData
{
    public int damage;
}

public class DamageComponent : ComponentDataProxy<DamageData>
{

}
    