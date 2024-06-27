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
    private GameManager gameManager;

    void Start()
    {
        RefreshHealth();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void TakeDamage(int amount = 1)
    {
        currentHealth -= amount;
        UpdateHealth();
        if (currentHealth <= 0)
        {
            gameManager.Die();
        }
    }

    void UpdateHealth()
    {
        healthText.text = "Health: " + currentHealth;
    }

    public void RefreshHealth()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }
}
