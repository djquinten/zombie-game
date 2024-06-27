using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int maxHealth = 5;

    private int _currentHealth;
    private GameManager _gameManager;

    private void Start()
    {
        RefreshHealth();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void TakeDamage(int amount = 1)
    {
        _currentHealth -= amount;
        UpdateHealth();
        
        if (_currentHealth <= 0)
        {
            _gameManager.Die();
        }
    }

    private void UpdateHealth()
    {
        healthText.text = "Health: " + _currentHealth;
    }

    public void RefreshHealth()
    {
        _currentHealth = maxHealth;
        UpdateHealth();
    }
}
