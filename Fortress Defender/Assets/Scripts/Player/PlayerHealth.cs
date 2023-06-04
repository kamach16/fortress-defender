using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameObject gunmen;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealthDisplay playerHealthDisplay;

    public void TakeDamage(float damage)
    {
        if (gameManager.GetIfLost()) return;

        health = Mathf.Max(health - damage, 0); // if health will be below 0, then return 0
        playerHealthDisplay.UpdateHealth(health);

        if (health <= 0)
        {
            gameManager.LoseGame();
            gunmen.SetActive(false);
        }
    }
}
