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
            Vector3 spawnPos = (pathCorners[i].transform.position + pathCorners[i+1].transform.position)/2;
            Vector3 diff = pathCorners[i].transform.position - pathCorners[i+1].transform.position;
            Vector3 scaleVec = new Vector3(pathVisualWidth, diff.magnitude + pathVisualWidth, 0);

            float rot;
            Vector3 diffN = diff.normalized;
            if (diffN == Vector3.up) {
                rot = 0;
            }
            else if (diffN == Vector3.right) {
                rot = 90;
            }
            else if (diffN == Vector3.down) {
                rot = 180;
            }
            else if (diffN == Vector3.left) {
                rot = 270;
            }
            else {
                rot = Mathf.Atan(diff.y/diff.x) * Mathf.Rad2Deg;
                rot -= 90;
                scaleVec.y -= pathVisualWidth/2;
            }

            GameObject path = Instantiate(pathPrefab, spawnPos, Quaternion.identity);
            path.transform.localScale = scaleVec;
            path.transform.eulerAngles = new Vector3(0, 0, rot);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        for (var i = 0; i < pathCorners.Length-1; i += 1) {
            Gizmos.DrawLine(pathCorners[i].transform.position, pathCorners[i+1].transform.position);
        }
    }
}
