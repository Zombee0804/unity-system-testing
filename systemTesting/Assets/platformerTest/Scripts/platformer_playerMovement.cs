using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformer_playerMovement : MonoBehaviour
{
    [Header("Component Vars")]
    public Rigidbody2D playerBody;

    [Header("Movement Vars")]
    public float defaultMoveSpeed;
    public float defaultJumpForce;
    public float boostedMoveSpeed;
    public float boostedJumpForce;

    private float moveSpeed;
    private float jumpForce;
    public float groundedBuffer;
    private float grounded;

    [Header("Boost Variables")]
    public float speedBoostBuffer;
    private float speedBoostAlarm;

    public float jumpBoostBuffer;
    private float jumpBoostAlarm;

    void Start() {
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;

        grounded = -0.1f;
    }

    void Update() {

        // Updating Movement Variables
        if (speedBoostAlarm > 0) {
            moveSpeed = boostedMoveSpeed;
            speedBoostAlarm -= Time.deltaTime;
        }
        else {
            moveSpeed = defaultMoveSpeed;
        }

        if (jumpBoostAlarm > 0) {
            jumpForce = boostedJumpForce;
            jumpBoostAlarm -= Time.deltaTime;
        }
        else {
            jumpForce = defaultJumpForce;
        }

        // Movement
        float horizontalMovement = 0;
        if (Input.GetKey("d")) {
            horizontalMovement = 1;
        }
        else if (Input.GetKey("a")) {
            horizontalMovement = -1;
        }

        Vector2 moveVec = new Vector2(horizontalMovement * moveSpeed, 0);
        playerBody.position += moveVec * Time.deltaTime;

        // Jumping
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w")) && grounded == -1) {
            playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            grounded = groundedBuffer;
        }

        if (grounded >= 0) {
            // grounded will reduce to just below 0. the grounded collision only works if it less than 0, but the player can only just if it is -1
            // this effectively adds a buffer to the grounded collision of length groundedBuffer
            grounded -= Time.deltaTime;
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        if (LayerMask.LayerToName(col.gameObject.layer) == "Ground" && grounded < 0) {
            grounded = -1;
        }

        if (col.gameObject.tag == "ground_speed") {
            speedBoostAlarm = speedBoostBuffer;
        }

        if (col.gameObject.tag == "ground_jump") {
            jumpBoostAlarm = jumpBoostBuffer;
        }
    }

}
