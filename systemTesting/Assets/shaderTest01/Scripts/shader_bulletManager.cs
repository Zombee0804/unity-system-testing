using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shader_bulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public shader_playerHealth SIHealth;

    [Header("Alarm Vars")]
    public float bulletInterval;
    private float bulletAlarm;

    [Header("Position Vars")]
    public float xSpawn;
    public float[] bulletPositions;

    void Update() {
        if (SIHealth.isAlive == true) {
            bulletAlarm += Time.deltaTime;
            if (bulletAlarm >= bulletInterval) {
                bulletAlarm = 0;
                CreateBullet();
            }
        }
    }

    void CreateBullet() {
        Vector3 creationVec = new Vector3(xSpawn, bulletPositions[Random.Range(0, bulletPositions.Length)], 0);
        Instantiate(bulletPrefab, creationVec, Quaternion.identity);
    }
}
