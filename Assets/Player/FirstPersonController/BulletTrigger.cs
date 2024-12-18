using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player owner;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hit something
        Debug.Log("Bullet hit: " + collision.gameObject.name);

        DamageHandler damageHandler = collision.gameObject.GetComponentInParent<DamageHandler>();
        if (damageHandler != null)
        {
            damageHandler.TakeDamage(10f);
        }

        // Destroy the bullet after collision
        Destroy(gameObject);
    }
}
