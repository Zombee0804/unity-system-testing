using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shader_playerMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public shader_playerHealth SIHealth;
    public float moveSpeed;

    void Update() {
        if (SIHealth.isAlive == true) {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(horizontalMovement, verticalMovement);

            playerBody.position += movement * moveSpeed * Time.deltaTime;
        }
    }
}
