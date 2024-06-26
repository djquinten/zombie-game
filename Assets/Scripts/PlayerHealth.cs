using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int maxHealth = 5;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        updateHealth();
    }

    public void TakeDamage(int amount = 1)
    {
        currentHealth -= amount;
        updateHealth();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
    }

    void updateHealth()
    {
        healthText.text = "Health: " + currentHealth;
    }
}
