using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ele_playerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D playerBody;
    public SpriteRenderer playerRen;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    private int jumpCount;
    public int jumpCountMax;
    public bool isGrounded;
    public float lastDir;

    [Header("Health")]
    public float startingHealth;
    public float currentHealth;

    void Start() {
        currentHealth = startingHealth;
    }

    void Update() {
        PlayerMovement();
        PlayerHealth();
        PlayerColourChange();
    }

    void PlayerMovement() {
        float movement = Input.GetAxisRaw("Horizontal");
        playerBody.position += new Vector2(movement, 0) * moveSpeed * Time.deltaTime;
        lastDir = Mathf.Sign(movement);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpCountMax) {
            playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount += 1;
            isGrounded = false;
        }
    }

    void PlayerHealth() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    void PlayerColourChange() {
        Color toSet = Color.white;
        toSet.g = (currentHealth/startingHealth);
        toSet.b = (currentHealth/startingHealth);
        playerRen.color = toSet;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "ground_normal") {
            jumpCount = 0;
            isGrounded = true;
        }
    }
}
