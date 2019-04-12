using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    public Transform target;
    public Transform parentTransform;
    public Transform weaponBase;
    private RotationComponent parentRotationComponent;

    private void Start()
    {
        parentTransform = transform.parent;
        parentRotationComponent = gameObject.GetComponentInParent<RotationComponent>();
    }

    private void Update()
    {
        target = parentRotationComponent.target;
    }
}
