using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_enemySpawner : MonoBehaviour
{
    [Header("Setup")]
    public td_pathManager pathManager;
    public int currentWave;
    public td_EnemyWave[] waves;

    [Header("Spawn Vars")]
    public bool allEnemiesSpawned;
    public bool allEnemiesDestroyed;
    public bool levelComplete;
    private int currentSpawnIndex;
    private float currentWaveAlarm;
    private List<GameObject> enemies = new List<GameObject>();

    void Start() {
        currentWave = 0;

        currentSpawnIndex = 0;
        currentWaveAlarm = 0;
        allEnemiesSpawned = false;
        allEnemiesDestroyed = false;
        levelComplete = false;
    }

    void Update() {
        if (levelComplete == false) {
            if (allEnemiesSpawned == false) {
                currentWaveAlarm += Time.deltaTime;

                EnemyStruct enemySpawnData = waves[currentWave].enemySpawnTimes[currentSpawnIndex];
                if (enemySpawnData.spawnTime < currentWaveAlarm) {
                    SpawnEnemy();
                    currentSpawnIndex += 1;
                }
                if (currentSpawnIndex >= waves[currentWave].enemySpawnTimes.Length) {
                    allEnemiesSpawned = true;
                }
            }
            else if (allEnemiesDestroyed == true) {
                if (Input.GetKeyDown(KeyCode.Space) == true) {
                    currentSpawnIndex = 0;
                    currentWaveAlarm = 0;
                    enemies = new List<GameObject>();
                    allEnemiesSpawned = false;
                    allEnemiesDestroyed = false;
                    if (currentWave < waves.Length - 1) {
                        currentWave += 1;
                    }
                    else {
                        levelComplete = true;
                    }
                }
            }
            else {
                allEnemiesDestroyed = true;
                foreach (GameObject enemy in enemies) {
                    if (enemy != null) {
                        allEnemiesDestroyed = false;
                        break;
                    }
                }
            }
        }   
    }

    void SpawnEnemy() {
        GameObject enemy = Instantiate(
            waves[currentWave].enemySpawnTimes[currentSpawnIndex].enemyPrefab,
            pathManager.pathCorners[0].transform.position,
            Quaternion.identity
        );
        enemies.Add(enemy);
        enemy.GetComponent<td_enemyMovement>().pathManager = pathManager;
    }
}
