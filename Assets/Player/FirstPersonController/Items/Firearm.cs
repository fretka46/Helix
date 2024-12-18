using System;
using UnityEngine;

public class Firearm : MonoBehaviour
{
    public Item Item;
    public GameObject BulletPrefab;

    public Transform FirePoint;
    // Get currently attached firearm prefab
    public GameObject FirearmPrefab;
    public float BulletSpeed = 100f;
    public float FireRate = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        // Get the Firearm prefab
        FirearmPrefab = gameObject;
        Item = FirearmPrefab.GetComponent<Item>();
    }

    private DateTime _lastShot = DateTime.MinValue;

    void Update()
    {
        if (Item.Owner != null && Input.GetKey(KeyCode.Mouse0) && (DateTime.Now - _lastShot).Milliseconds > FireRate)
        {
            _lastShot = DateTime.Now;
            Shoot();
        }
    }

    void Shoot()
    {
        FirePoint = Item.SpawnedItem.transform.Find("FirePoint");

        // Calculate the spawn position 0.5 meters in front of the firePoint
        Vector3 spawnPosition = FirePoint.position + FirePoint.forward * 1.5f;

        // Instantiate the bullet at the calculated position and with the firePoint's rotation
        GameObject bullet = Instantiate(BulletPrefab, spawnPosition, FirePoint.rotation);
        // Add bullet script to the bullet
        bullet.AddComponent<Bullet>();
        bullet.GetComponent<Bullet>().owner = GetComponent<Player>();

        // Ensure the bullet has a Rigidbody component
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody>();
        }

        // Set the Rigidbody to use continuous collision detection
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Set the bullet's velocity
        rb.velocity = FirePoint.forward * BulletSpeed;
    }
}
