using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_projectileMovement : MonoBehaviour
{
    public td_enemyMovement toHit;
    public float damage;
    
    public float speed;
    public float endBuffer;

    void Update() {

        if (toHit == null) {
            Destroy(gameObject);
        }
        else {
            // // Vector3 diff = targetPos - transform.position;
            Vector3 diff = toHit.transform.position - transform.position;

            if (diff.magnitude < endBuffer) {
                toHit.health -= damage;
                Destroy(gameObject);
            }

            diff = diff.normalized;
            transform.position += diff * speed * Time.deltaTime;
        }
    }
}
