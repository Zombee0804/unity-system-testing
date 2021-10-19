using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shader_playerHealth : MonoBehaviour
{
    public float startingHealth;
    private float currentHealth;
    public bool isAlive;

    [Header("Shader Variables")]
    public Material mat;
    private float fadeValue;
    public float fadeRate;

    public float hitLength;
    private float hitAlarm;

    void Start() {
        currentHealth = startingHealth;
        isAlive = true;

        mat = GetComponent<SpriteRenderer>().material;
        fadeValue = 1;
    }

    void Update() {
        if (isAlive == false) {
            if (fadeValue > 0) {
                fadeValue -= fadeRate * Time.deltaTime;
                mat.SetFloat("_StepValue", fadeValue);
            }
            else {
                Destroy(gameObject);
            }
        }

        if (mat.GetInt("_IsHit") == 1) {
            hitAlarm += Time.deltaTime;
            if (hitAlarm > hitLength) {
                mat.SetInt("_IsHit", 0);
            }
        }
    }

    void Hit() {
        if (currentHealth > 0) currentHealth -= 1;
        if (currentHealth <= 0) {
            isAlive = false;
        }
        else {
            mat.SetInt("_IsHit", 1);
            hitAlarm = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "bullet") {
            Destroy(col.gameObject);
            Hit();
        }
    }
}
