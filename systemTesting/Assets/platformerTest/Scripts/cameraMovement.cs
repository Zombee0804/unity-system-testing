using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform target;
    public float movementDiv;

    void FixedUpdate() {
        if (target != null) {
            Vector3 movement = target.position - transform.position;
            movement.z = 0;
            transform.position += movement/movementDiv;
        }
    }
}
