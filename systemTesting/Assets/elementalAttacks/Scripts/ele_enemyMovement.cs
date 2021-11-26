using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ele_enemyMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D enemyBody;
    public Rigidbody2D playerBody;
    public ele_playerMovement playerMovement;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Health")]
    public float startingHealth;
    public float currentHealth;

    [Header("Attacking")]
    private float distanceFromPlayer;
    private bool playerGrounded;
    public float attackRange;

    public float damage;
    public float damageBuffer;
    private float damageAlarm;

    void Start() {
        currentHealth = startingHealth;
    }

    void Update() {
        if (playerBody != null) {
            distanceFromPlayer = (playerBody.position - enemyBody.position).x;
            playerGrounded = playerMovement.isGrounded;
            EnemyMovement();
            EnemyHealth();
            EnemyAttacking();
        }
    }

    void EnemyMovement() {
        RaycastHit2D sight = Physics2D.Raycast(transform.position, new Vector2(Mathf.Sign(distanceFromPlayer), 0), 0.6f);
        if (sight.collider == null) {
            enemyBody.position += new Vector2(Mathf.Sign(distanceFromPlayer), 0) * moveSpeed * Time.deltaTime;
        }
    }

    void EnemyHealth() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    void EnemyAttacking() {
        if (distanceFromPlayer < attackRange && playerGrounded) {
            if (damageAlarm <= 0) {
                playerMovement.currentHealth -= damage;
                damageAlarm = damageBuffer;
            }
            else {
                damageAlarm -= Time.deltaTime;
            }
        }
    }

    public void DamageEnemy(float damage, string damageType) {
        currentHealth -= damage;
    }
}
