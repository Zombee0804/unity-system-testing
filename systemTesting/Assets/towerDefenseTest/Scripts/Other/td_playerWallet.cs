using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class td_playerWallet : MonoBehaviour
{
    [Header("General")]
    public static float playerMoney;
    public static int playerHealth;

    public float startingMoney;
    public int startingHealth;

    [Header("UI Variables")]
    public Text healthText; 

    void Start() {
        playerMoney = startingMoney;
        playerHealth = startingHealth;
    }

    void Update() {
        if (playerHealth <= 0) {
            Debug.Log("Game Over");
        }

        healthText.text = " Health: " + playerHealth.ToString();
    }
}
