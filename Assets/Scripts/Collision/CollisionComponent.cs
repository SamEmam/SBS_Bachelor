using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


// https://github.com/Unity-Technologies/EntityComponentSystemSamples/issues/45

public struct CollisionComponent : IComponentData {
    public Entity OtherEntity;
    public Overlap state;
    public CollisionData Collision;
}


public struct CollisionData {
    public CollisionData (Collision2D collision) {
        // deconstruct any collision data into blittable data here
        Contact = collision.contactCount > 0 ? new Contact(collision.GetContact(0)) : new Contact { };
    }
    public Contact Contact;

}

public struct Contact {
    public Contact (ContactPoint contactPoint) {
        Point = contactPoint.point;
        RelativeVelocity = contactPoint.relativeVelocity;
        Normal = contactPoint.normal;
    }

    public Vector3 Point;
    public Vector3 RelativeVelocity;
    public Vector3 Normal;
}
