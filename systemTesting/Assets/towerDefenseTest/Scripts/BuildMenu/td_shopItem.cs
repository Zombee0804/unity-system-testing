using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_shopItem : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public td_shopMenu shopMenuUI;
    public td_pathManager pathManager;

    [Header("Shop Vars")]
    public GameObject towerPrefab;
    public float price;

    public void CreateTower() {
        if (price <= td_playerWallet.playerMoney) {
            shopMenuUI.CloseMenu();
            td_playerWallet.playerMoney -= price;

            Vector3 mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            GameObject tower = Instantiate(towerPrefab, mousePos, Quaternion.identity);
            td_towerPlacement towerPlacement = tower.GetComponent<td_towerPlacement>();
            towerPlacement.cam = cam;
            towerPlacement.pathManager = pathManager;
        }
    }
}
