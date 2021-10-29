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
            Vector3 diff = targetPos - transform.position;
            Vector3 movement = diff.normalized; // Makes its magnitude (length) 1. E.G (6, 0, 0) would become (1, 0, 0)
            if (diff.magnitude < posBuffer) {
                atTarget = true;
                transform.position = targetPos;
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
