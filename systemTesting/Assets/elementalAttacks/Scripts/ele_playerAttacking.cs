using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackElement {
    public string name;
    public float initalDamage;
    public Sprite iconSprite;
}

public class ele_playerAttacking : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer elementIcon;
    public ele_playerMovement playerMovement;

    [Header("Elements")]
    public AttackElement[] attackTypes;
    private int currentElementIndex;
    public AttackElement currentElement;

    [Header("Attacking")]
    public float attackRange;

    void Start() {
        currentElementIndex = 0;
        currentElement = attackTypes[currentElementIndex];
    }

    void Update() {
        ElementChanging();
        Attacking();
        UpdateElementIcon();
    }

    void ElementChanging() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            currentElementIndex += 1;
            if (currentElementIndex >= attackTypes.Length) {
                currentElementIndex = 0;
            }
            currentElement = attackTypes[currentElementIndex];
        }
    }

    void Attacking() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D attackRay = Physics2D.Raycast(transform.position, new Vector2(playerMovement.lastDir, 0), attackRange);
            if (attackRay.collider != null) {
                ele_enemyMovement enemyMovement = attackRay.collider.gameObject.GetComponent<ele_enemyMovement>();
                enemyMovement.DamageEnemy(currentElement.initialDamage, "");
            }
        }
    }

    void UpdateElementIcon() {
        elementIcon.sprite = currentElement.iconSprite;
    }
}
