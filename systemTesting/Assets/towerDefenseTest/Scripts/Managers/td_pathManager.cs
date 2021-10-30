using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_pathManager : MonoBehaviour
{
    public GameObject[] pathCorners;
    
    [Header("Visual Vars")]
    public GameObject pathPrefab;
    public float pathVisualWidth;

    void Start() {
        for (var i = 0; i < pathCorners.Length-1; i += 1) {
            Vector3 spawnPos = (pathCorners[i].transform.position + pathCorners[i+1].transform.position) / 2;
            Vector3 scaleVec = pathCorners[i].transform.position - pathCorners[i+1].transform.position;
            
            if (scaleVec.x < 0.1f) {
                scaleVec.x = pathVisualWidth;
                scaleVec.y += pathVisualWidth;
            }
            else if (scaleVec.y < 0.1f) {
                scaleVec.y = pathVisualWidth;
                scaleVec.x += pathVisualWidth;
            }

            GameObject path = Instantiate(pathPrefab, spawnPos, Quaternion.identity);
            path.transform.localScale = scaleVec;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        for (var i = 0; i < pathCorners.Length-1; i += 1) {
            Gizmos.DrawLine(pathCorners[i].transform.position, pathCorners[i+1].transform.position);
        }
    }
}
