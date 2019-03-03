﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct CollisionDataComponent : IComponentData
{
    public Entity OtherEntity;
    public CollisionState State;
    public CollisionData Collision;
}


public struct CollisionData
{
    public CollisionData(Collision collision)
    {
        // deconstruct any collision data into blittable data here
        Contact = collision.contactCount > 0 ? new Contact(collision.GetContact(0)) : new Contact { };
    }
    public Contact Contact;

}

public struct Contact
{
    public Contact(ContactPoint contactPoint)
    {
        Point = contactPoint.point;
        Normal = contactPoint.normal;
    }

    public Vector3 Point;
    public Vector3 Normal;
}