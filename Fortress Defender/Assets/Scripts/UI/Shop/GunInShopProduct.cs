using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunInShopProduct : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private int price;
    [SerializeField] private bool bought;
    [SerializeField] public bool equipped;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private GunInShopProduct[] allGunsToBuy;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        if (!equipped) stateText.text = price.ToString() + "$"; // every state text is changing except pistol  
    }

    public void Interact() // buy or equip  
    {
        if (gameManager.currentMoney >= price && !bought) // buy
        {
            playerShooting.SelectWeapon(weapon);
            gameManager.SubtractMoney(price);
            bought = true;

            Debug.Log(weapon + " bought");
        }

        if (bought) // equip
        {
            Equip();
        }
    }

    private void Equip()
    {
        UnequipCurrentEquippedGun();

        playerShooting.SelectWeapon(weapon);
        stateText.text = "EQUIPPED";
        equipped = true;

        Debug.Log(weapon + " equipped");
    }

    private void UnequipCurrentEquippedGun()
    {
        foreach (GunInShopProduct gun in allGunsToBuy)
        {
            if (gun.equipped)
            {
                gun.equipped = false;
                gun.stateText.text = "EQUIP";
            }
        }
    }
}
