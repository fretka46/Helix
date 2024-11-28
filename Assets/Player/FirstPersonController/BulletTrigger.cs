using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hit something
        Debug.Log("Bullet hit: " + collision.gameObject.name);

        // You can add additional logic here, such as applying damage to the hit object

        // Destroy the bullet after collision
        Destroy(gameObject);
    }
}
