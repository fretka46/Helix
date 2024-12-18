using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageHandler : MonoBehaviour
{
    public TextMeshProUGUI HealthBar;
    public Player Player;

    public float MaxHealth = 100f;
    private float _health = 100f;
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            UpdateHealthBar();
        }
    }

    void Start()
    {
        UpdateHealthBar();
        Player = GetComponent<Player>();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log($"{Player.Nickname} took {damage} damage. Current health: {Health}");

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }

        UpdateHealthBar();
    }

    public void Die()
    {
        Debug.Log($"{Player.Nickname} died");
    }

    void UpdateHealthBar()
    { 
        if (HealthBar != null)
            HealthBar.text = "HP: " + Health;
    }
}
