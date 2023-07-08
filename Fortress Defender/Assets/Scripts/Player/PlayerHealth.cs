using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] private GameObject gunmen;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealthDisplay playerHealthDisplay;

    public void TakeDamage(float damage)
    {
        if (gameManager.lost) return;

        health = Mathf.Max(health - damage, 0); // if health will be below 0, then return 0
        playerHealthDisplay.UpdateHealth(health);

        if (health <= 0)
        {
            gameManager.LoseGame();
            gunmen.SetActive(false);
        }
    }

    public void AddHealth(float healthToAdd)
    {
        health = Mathf.Min(health + healthToAdd, maxHealth); // if health will be higher than 100, then return 100
        playerHealthDisplay.UpdateHealth(health);
    }
}
