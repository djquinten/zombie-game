using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount = 1)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // Add logic for what happens when the player dies
    }

    // public Text healthText;

    void Update()
    {
        // if (healthText != null)
        // {
        //     healthText.text = "Health: " + currentHealth;
        // }
    }
}
