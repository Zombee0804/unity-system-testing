using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class td_moneyDisplay : MonoBehaviour
{
    public Text moneyDisplay;

    void Update() {
        moneyDisplay.text = " Money: " + td_playerWallet.playerMoney;
    }
}
