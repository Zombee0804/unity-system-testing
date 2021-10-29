using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_pathManager : MonoBehaviour
{
    public GameObject[] pathCorners;

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        for (var i = 0; i < pathCorners.Length-1; i += 1) {
            Gizmos.DrawLine(pathCorners[i].transform.position, pathCorners[i+1].transform.position);
        }
    }
}
