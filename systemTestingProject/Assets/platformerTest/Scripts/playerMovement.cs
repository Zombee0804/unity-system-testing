using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Component Variables
    public Rigidbody2D playerBody;

    // Movement Variables
    public float defaultMovespeed;
    public float defaultJumpforce;
    public float boostedMovespeed;
    public float boostedJumpforce;

    private float movespeed;
    private float jumpforce;

    private bool isGrounded;
    private bool isBoosted;
    private bool isJumpBoosted;

    void Start() {
        movespeed = defaultMovespeed;
        jumpforce = defaultJumpforce;

        isGrounded = true;
        isBoosted = false;
        isJumpBoosted = false;
    }

    void Update() {
        
        // Movement
        Vector2 movement = Vector2.zero;

        if (Input.GetKey("d") == true) {
            movement.x = 1;
        }
        else if (Input.GetKey("a") == true) {
            movement.x = -1;
        }

        playerBody.position += movement * movespeed * Time.deltaTime;

        // Jumping
        if (Input.GetKeyDown("w") && isGrounded == true) {
            playerBody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Boosted
        if (isBoosted == true) movespeed = boostedMovespeed; else movespeed = defaultMovespeed;
        if (isJumpBoosted == true) jumpforce = boostedJumpforce; else jumpforce = defaultJumpforce;
    }
    
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = true;
            isBoosted = false;
            isJumpBoosted = false;
        }
        if (col.gameObject.tag == "Boost Ground") {
            isBoosted = true;
            isGrounded = true;
        }
        if (col.gameObject.tag == "Jump Ground") {
            isJumpBoosted = true;
            isGrounded = true;
        }
    }
}
