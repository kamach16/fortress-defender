using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public bool lost = false;

    public bool GetIfLost()
    {
        return lost;
    }

    public void LoseGame()
    {
        lost = true;
        print("DEFEAT");
    }
}
