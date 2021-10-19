using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public Rigidbody2D bulletBody;
    public float moveSpeed;

    public float lifeSpan;
    private float lifeAlarm;

    void Update() {
        bulletBody.position -= new Vector2(moveSpeed, 0) * Time.deltaTime;
        
        lifeAlarm += Time.deltaTime;
        if (lifeAlarm >= lifeSpan) {
            Destroy(gameObject);
        }
    }
}
