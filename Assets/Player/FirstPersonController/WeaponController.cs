using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<Item> Inventory = new();
    public int CurrentItemIndex = 0;
    public int MaxItems = 5;
    public Item CurrentItem
    {
        get
        {
            if (Inventory.Count == 0)
                return null;

            return CurrentItemIndex >= Inventory.Count ? null : Inventory[CurrentItemIndex];
        }
    }

    public Player Owner => GetComponent<Player>();

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            ChangeItem();
        }

        // Check if player is trying to pickup item
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Picking up item");
            PickupItem();
        }

        // Update the position and rotation of the current item to follow the player's camera
        if (CurrentItem != null && CurrentItem.SpawnedItem != null)
        {
            Vector3 offsetPosition = Owner.PlayerCamera.transform.position + Owner.PlayerCamera.transform.TransformDirection(CurrentItem.PickedUpOffset);
            CurrentItem.SpawnedItem.transform.SetPositionAndRotation(offsetPosition, Owner.PlayerCamera.transform.rotation);
        }
    }

    void ChangeItem()
    {
        if (CurrentItem != null)
            CurrentItem.SpawnedItem.SetActive(false);

        if (Input.mouseScrollDelta.y > 0)
        {
            if (CurrentItemIndex == MaxItems - 1)
            {
                CurrentItemIndex = 0;
            }
            else
            {
                CurrentItemIndex++;
            }
        }
        else
        {
            if (CurrentItemIndex == 0)
            {
                CurrentItemIndex = MaxItems - 1;
            }
            else
            {
                CurrentItemIndex--;
            }
        }

        Owner.PlayerUi.IventoryIndex.text = $"{CurrentItemIndex.ToString()}\n{CurrentItem?.name}";

        Debug.Log($"Current index: {CurrentItemIndex}, Name: {CurrentItem?.ItemName}");

        if (CurrentItem != null)
        {
            HoldItem(CurrentItem);
        }
    }

    void PickupItem()
    {
        // Check if the player is already holding an item
        if (Inventory.Count < MaxItems)
        {
            // Check if the player is looking at an item
            RaycastHit hit;
            if (Physics.Raycast(Owner.PlayerCamera.transform.position, Owner.PlayerCamera.transform.forward, out hit, 5f))
            {
                // Check if the item has an Item component
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    // Add the item to the player's inventory
                    Inventory.Add(item);

                    // Deactivate the item in the world
                    item.gameObject.SetActive(false);

                    Debug.Log($"Picked up {item.ItemName}");
                }
                else
                    Debug.Log("Ray Component is not an Item: " + hit.collider.name);
            }
            else
            {
                Debug.Log("SearchItem Raycast did not hit anything");
            }
        }
    }

    void HoldItem(Item item)
    {
        // Reactivate the current item
        item.SpawnedItem.SetActive(true);

        // Add the spawned item to the player object
        item.SpawnedItem.transform.SetParent(Owner.transform);

        // Set the item's position and rotation to the player's position and rotation
        Vector3 offsetPosition = Owner.PlayerCamera.transform.position + Owner.PlayerCamera.transform.TransformDirection(CurrentItem.PickedUpOffset);
        item.SpawnedItem.transform.SetPositionAndRotation(offsetPosition, Owner.PlayerCamera.transform.rotation);

        Debug.Log($"Added inventory item at position {offsetPosition}, Offset: {CurrentItem.PickedUpOffset}");

        Debug.Log($"Current index: {CurrentItemIndex}, Name: {CurrentItem.ItemName}");
    }
}
