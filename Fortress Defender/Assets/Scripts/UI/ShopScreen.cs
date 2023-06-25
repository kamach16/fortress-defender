using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void ContinueGame()
    {
        gameManager.ContinueGame();
    }
}
