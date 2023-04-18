using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameManager gameManager;

    public void TakeDamage(float damage)
    {
        if (gameManager.GetIfLost()) return;

        health -= damage;

        if (health <= 0) gameManager.LoseGame();
    }
}
