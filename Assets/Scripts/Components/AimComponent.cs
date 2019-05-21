using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    public Transform target;
    public Transform weaponBase;
    public RotationComponent parentRotationComponent;

    private void Start()
    {
        parentRotationComponent = gameObject.GetComponentInParent<RotationComponent>();
    }
}
