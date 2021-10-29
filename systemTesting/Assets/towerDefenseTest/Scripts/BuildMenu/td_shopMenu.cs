using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_shopMenu : MonoBehaviour
{
    public bool menuActive;
    public GameObject shopMenuUI;

    void Start() {
        CloseMenu();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) == true) {
            if (menuActive == true) {
                CloseMenu();
            }
            else {
                OpenMenu();
            }
        }
    }

    public void CloseMenu() {
        menuActive = false;
        Time.timeScale = 1f;
        shopMenuUI.SetActive(false);
    }

    public void OpenMenu() {
        menuActive = true;
        Time.timeScale = 0f;
        shopMenuUI.SetActive(true);
    }
}
