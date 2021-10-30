using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_towerPlacement : MonoBehaviour
{
    public Camera cam;
    private bool isPlaced;
    public td_pathManager pathManager;

    public SpriteRenderer spriteRen;
    private bool flashRed;
    public float flashLength;
    private float flashAlarm;
    private Color defaultColour;

    void Start() {
        isPlaced = false;
        flashRed = false;
        
        defaultColour = spriteRen.color;
    }

    void Update() {
        if (isPlaced == false) {
            // Moving to mouse poss
            Vector3 mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            transform.position = mousePos;


            // Placing
            if (Input.GetMouseButtonDown(0) == true) {
                // Checking if the placement is inside the enemy path
                bool isValidPlace = true;
                Vector2[] toCheck = new Vector2[] {Vector2.up, Vector2.right};
                foreach (Vector2 direction in toCheck) {
                    RaycastHit2D ray01 = Physics2D.Raycast(transform.position, direction);
                    RaycastHit2D ray02 = Physics2D.Raycast(transform.position, -direction);
                    if (ray01.collider != null && ray02.collider != null) {
                        if (ray01.collider.gameObject.tag == "cornerPoint" && ray02.collider.gameObject.tag == "cornerPoint") {

                            int ray01Index = System.Array.FindIndex(pathManager.pathCorners, x => x == ray01.collider.gameObject);
                            int ray02Index = System.Array.FindIndex(pathManager.pathCorners, x => x == ray02.collider.gameObject);
                            if (Mathf.Abs(ray01Index - ray02Index) == 1) {
                                isValidPlace = false;
                                break;
                            }

                        }
                    }
                }

                if (isValidPlace == true) {
                    isPlaced = true;
                }
                else {
                    flashRed = true;
                }
            }
        }

        if (flashRed == true) {
            spriteRen.color = Color.red;
            flashAlarm += Time.deltaTime;
            if (flashAlarm >= flashLength) {
                spriteRen.color = defaultColour;
                flashRed = false;
                flashAlarm = 0;
            }
        }
    }
}
