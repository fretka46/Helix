using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Firearm ItemFirearm = null;
    public Throwable ItemThrowable = null;
    public Player Owner = null;
    public GameObject ItemPrefab = null;
    public GameObject SpawnedItem = null;
    public Vector3 PickedUpOffset;
    public bool IsThrowable => ItemThrowable != null;
    public bool IsFirearm => ItemFirearm != null;
    public bool IsInInventory => !SpawnedItem.activeSelf;

    public string ItemName = "Item";

    public void Drop()
    {
        // Instantiate the item at the calculated position and with the player's rotation
        SpawnedItem = Instantiate(ItemPrefab, Owner.Position, Owner.Rotation);

        // Ensure the item has a Rigidbody component
        Rigidbody rb;
        try
        {
            rb = SpawnedItem.GetComponent<Rigidbody>();
        }
        catch
        {
            rb = SpawnedItem.AddComponent<Rigidbody>();
        }

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.isKinematic = false;
        rb.useGravity = true;

        Owner = null;
    }
}
