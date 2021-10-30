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

    [Header("Attacking Timers")]
    public float attackInterval;
    public float attackAlarm;

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
    }

    void DamageEnemy() {
        if (attackAlarm >= attackInterval) {
            attackAlarm = 0;
            if (toHit != null) {
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                td_projectileMovement projMovement = proj.GetComponent<td_projectileMovement>();
                // // projMovement.targetPos = toHit.transform.position + toHit.movement;
                projMovement.toHit = toHit;
                projMovement.damage = towerDamage;
            }
        }
        else {
            attackAlarm += Time.deltaTime;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
