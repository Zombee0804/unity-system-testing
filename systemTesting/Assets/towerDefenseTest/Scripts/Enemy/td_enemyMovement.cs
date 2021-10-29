using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_enemyMovement : MonoBehaviour
{
    public td_pathManager pathManager;

    [Header("Enemy Vars")]
    public float speed;
    public float healthMax;
    private float health;

    [Header("Path Vars")]
    private int targetIndex;
    private Vector3 targetPos;
    public float posBuffer;
    private bool atTarget;

    void Start() {
        health = healthMax;

        targetIndex = 0;
        atTarget = true;
    }

    void Update() {
        if (atTarget == true) {
            UpdateTargetPos();
        }
        else {
            Vector3 movement = Vector3.zero;
            Vector3 diff = targetPos - transform.position;
            if ((diff.x > -posBuffer && diff.x < posBuffer) && (diff.y > -posBuffer && diff.y < posBuffer)) {
                atTarget = true;
                transform.position = targetPos;
            }
            else if (diff.x == 0) {
                if (diff.y > 0) {
                    movement = Vector3.up;
                }
                else {
                    movement = Vector3.down;
                }
            }
            else if (diff.y == 0) {
                if (diff.x > 0) {
                    movement = Vector3.right;
                }
                else {
                    movement = Vector3.left;
                }
            }
            transform.position += movement * speed * Time.deltaTime;
        }
    }

    void UpdateTargetPos() {
        atTarget = false;
        
        if (targetIndex < pathManager.pathCorners.Length-1) {
            targetIndex += 1;
        }
        else {
            Destroy(gameObject);
        }

        targetPos = pathManager.pathCorners[targetIndex].transform.position;
    }
}
