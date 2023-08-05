using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float shield;
    [SerializeField] public float maxShield;
    [SerializeField] private GameObject gunmen;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealthDisplay playerHealthDisplay;
    [SerializeField] private PlayerShieldDisplay playerShieldDisplay;
    [SerializeField] private AudioSource audioSource;

    public void TakeDamage(float damage)
    {
        if (gameManager.lost) return;

        if (shield <= 0)
        {
            health = Mathf.Max(health - damage, 0); // if health will be below 0, then return 0
            playerHealthDisplay.UpdateHealth(health);
            audioSource.Play();
        }
        else if (shield > 0)
        {
            if(damage > shield)
            {
                float extraDamage = damage - shield;
                health = Mathf.Max(health - extraDamage, 0); // if health will be below 0, then return 0
                playerHealthDisplay.UpdateHealth(health);
            }

            shield = Mathf.Max(shield - damage, 0); // if shield will be below 0, then return 0
            playerShieldDisplay.UpdateShield(shield);
            audioSource.Play();
        }

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

    public void AddShield(float shieldToAdd)
    {
        shield = Mathf.Min(shield + shieldToAdd, maxShield); // if shield will be higher than 100, then return 100
        playerShieldDisplay.UpdateShield(shield);
    }
}
