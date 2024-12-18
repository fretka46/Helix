using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Nickname = "Player";
    public DamageHandler DamageHandler => GetComponent<DamageHandler>();
    public Item CurrentItem => GetComponent<InventoryController>().CurrentItem;
    public PlayerUi PlayerUi => GetComponent<PlayerUi>();

    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    public GameObject PlayerCamera;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
