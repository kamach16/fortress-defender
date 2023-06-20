using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    public void UpdateMoneyText(int currentMoney)
    {
        moneyText.text = currentMoney.ToString() + "$";
    }
}
