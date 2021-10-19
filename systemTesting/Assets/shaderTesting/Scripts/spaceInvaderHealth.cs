using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceInvaderHealth : MonoBehaviour
{
    public float startingHealth;
    private float currentHealth;
    public bool isAlive;

    void Start() {
        currentHealth = startingHealth;
        isAlive = true;
    }

    void Update() {
        if (isAlive == false) Destroy(gameObject);
    }

    void Hit() {
        if (currentHealth > 0) currentHealth -= 1;
        if (currentHealth <= 0) isAlive = false;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "bullet") {
            Destroy(col.gameObject);
            Hit();
        }
    }
}
