using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionSystem : MonoBehaviour
{
    public int damage;
    public GameObject impactEffect;
    public string collision;

    void Start()
    {
        damage = gameObject.GetComponent<DamageComponent>().damage;
    }

    public void OnTriggerEnter(Collider col)
    {

        ////all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        //if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<HealthComponent>() != null)
        //{
        //    if (col.gameObject.GetComponent<FactionComponent>().faction != gameObject.GetComponent<FactionComponent>().faction)
        //    {
        //        col.gameObject.GetComponent<HealthComponent>().health -= damage;
        //        //add an explosion or something
        //        Instantiate(impactEffect, transform.position, Quaternion.Inverse(transform.rotation));
        //        //destroy the projectile that just caused the trigger collision
        //        Destroy(gameObject);

        //    }
        //}
    }
}