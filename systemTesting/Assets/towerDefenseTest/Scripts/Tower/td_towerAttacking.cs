using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_towerAttacking : MonoBehaviour
{
    [Header("General")]
    public bool isPlaced;
    public float towerRange;
    public float towerDamage;
    public td_enemyMovement toHit;
    public GameObject projectilePrefab;

    [Header("Attacking Vars")]
    public float attackInterval;
    public float attackAlarm;

    public td_projectileMovement lastProj;
    public float bulletLife;
    public float bulletAlarm;

    void Start() {
        isPlaced = false;
    }

    void Update() {
        if (isPlaced == true) {
            toHit = null;
            Vector2 origin = new Vector2(transform.position.x, transform.position.y);

            RaycastHit2D[] enemiesInRange = Physics2D.CircleCastAll(
                origin, towerRange, Vector2.zero, 0f, LayerMask.GetMask("Enemy")
            );

            if (enemiesInRange.Length > 0) {
                int lowestIndex = 10000;
                foreach (RaycastHit2D rayHit in enemiesInRange) {
                    td_enemyMovement enemyMovement = rayHit.collider.gameObject.GetComponent<td_enemyMovement>();
                    if (enemyMovement.enemyIndex < lowestIndex) {
                        lowestIndex = enemyMovement.enemyIndex;
                        toHit = enemyMovement;
                    }
                }
            }
        }

        DamageEnemy();
        BulletLife();
    }

    void DamageEnemy() {
        if (attackAlarm >= attackInterval) {
            attackAlarm = 0;
            if (toHit != null) {
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                lastProj = proj.GetComponent<td_projectileMovement>();
                lastProj.toHit = toHit;
                lastProj.damage = towerDamage;
            }
        }
        else {
            attackAlarm += Time.deltaTime;
        }
    }

    void BulletLife() {
        if (lastProj != null) {
            bulletAlarm += Time.deltaTime;
            if (bulletAlarm >= bulletLife) {
                Destroy(lastProj.gameObject);
                bulletAlarm = 0;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
