using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro

public class DamageHandler : MonoBehaviour
{
    public Image healthBar;
    public TextMeshProUGUI scoreText; // If using TextMeshPro
    // public Text scoreText; // If using the default Text component

    private int score = 0;
    private float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        UpdateScoreText();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthBar();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
