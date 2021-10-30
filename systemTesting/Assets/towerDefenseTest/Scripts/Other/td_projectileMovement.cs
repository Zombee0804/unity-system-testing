using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_projectileMovement : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed;
    public float endBuffer;

    void Update() {
        Vector3 diff = targetPos - transform.position;

        if (diff.magnitude < endBuffer) {
            Destroy(gameObject);
        }

        diff = diff.normalized;
        transform.position += diff * speed * Time.deltaTime;
    }
}
