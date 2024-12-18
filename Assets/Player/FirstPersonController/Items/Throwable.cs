using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Item Item;
    public GameObject throwablePrefab;
    public Transform throwPoint;
    public float throwSpeed = 100f;

    public Player owner;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Throw();
        }
    }

    void Throw()
    {
        // Calculate the spawn position 0.5 meters in front of the throwPoint
        Vector3 spawnPosition = throwPoint.position + throwPoint.forward * 1.5f;
        // Instantiate the throwable at the calculated position and with the throwPoint's rotation
        GameObject throwable = Instantiate(throwablePrefab, spawnPosition, throwPoint.rotation);
        // Add throwable script to the throwable
        throwable.AddComponent<Throwable>();
        throwable.GetComponent<Throwable>().owner = GetComponent<Player>();
        // Ensure the throwable has a Rigidbody component
        Rigidbody rb = throwable.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = throwable.AddComponent<Rigidbody>();
        }
        // Set the Rigidbody to use continuous collision detection
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        // Set the throwable's velocity
        rb.velocity = throwPoint.forward * throwSpeed;
    }
}
