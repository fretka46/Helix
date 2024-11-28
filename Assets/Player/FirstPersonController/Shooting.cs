using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Empty object for bullet spawn point
    public float bulletSpeed = 100f;
    public DamageHandler damageHandler;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse button
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Simulate taking damage
            damageHandler.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Simulate adding score
            damageHandler.AddScore(1);
        }
    }

    void Shoot()
    {
        // Calculate the spawn position 0.5 meters in front of the firePoint
        Vector3 spawnPosition = firePoint.position + firePoint.forward * 1.5f;

        // Instantiate the bullet at the calculated position and with the firePoint's rotation
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);
        // Add bullet script to the bullet
        bullet.AddComponent<Bullet>();

        // Ensure the bullet has a Rigidbody component
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody>();
        }

        // Set the Rigidbody to use continuous collision detection
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Set the bullet's velocity
        rb.velocity = firePoint.forward * bulletSpeed;
    }
}
