using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class td_enemySpawner : MonoBehaviour
{
    [Header("Setup")]
    public td_pathManager pathManager;
    public int currentWave;
    public td_EnemyWave[] waves;

    [Header("Spawn Vars")]
    public bool allEnemiesSpawned;
    public bool allEnemiesDestroyed;
    public bool gameComplete;
    private int currentSpawnIndex;
    private float currentWaveAlarm;
    private List<GameObject> enemies = new List<GameObject>();

    [Header("UI Vars")]
    public Text waveText;
    public Color completeColour;
    public Text enemyText;

    void Start() {
        currentWave = -1;

        currentSpawnIndex = 0;
        currentWaveAlarm = 0;
        allEnemiesSpawned = false;
        allEnemiesDestroyed = false;
        gameComplete = false;

        SetWaveText();
        SetEnemyText();
    }

    void SetWaveText() {
        waveText.color = Color.white;
        waveText.text = " Wave: " + (currentWave+1).ToString();
    }

    void SetEnemyText() {
        if (currentWave != -1) {
            float enemiesSpawned = currentSpawnIndex;
            float enemiesTotal = waves[currentWave].enemySpawnTimes.Length;
            enemyText.text = " Enemies: " + enemiesSpawned.ToString() + "/" + enemiesTotal.ToString();
        }
    }

    void Update() {
        if (gameComplete == false) {
            if (allEnemiesSpawned == false && currentWave != -1) {
                currentWaveAlarm += Time.deltaTime;

                EnemyStruct enemySpawnData = waves[currentWave].enemySpawnTimes[currentSpawnIndex];
                if (enemySpawnData.spawnTime < currentWaveAlarm) {
                    SpawnEnemy();
                    currentSpawnIndex += 1;
                    SetEnemyText();
                }
                if (currentSpawnIndex >= waves[currentWave].enemySpawnTimes.Length) {
                    allEnemiesSpawned = true;
                }
            }
            else if (allEnemiesDestroyed == false && currentWave != -1) {
                allEnemiesDestroyed = true;
                foreach (GameObject enemy in enemies) {
                    if (enemy != null) {
                        allEnemiesDestroyed = false;
                        break;
                    }
                }
            }
            else {
                waveText.color = completeColour;
                if (Input.GetKeyDown(KeyCode.Space) == true) {
                    currentSpawnIndex = 0;
                    currentWaveAlarm = 0;
                    enemies = new List<GameObject>();
                    allEnemiesSpawned = false;
                    allEnemiesDestroyed = false;
                    if (currentWave < waves.Length - 1) {
                        currentWave += 1;
                        SetWaveText();
                        SetEnemyText(); // Updates total enemy count
                    }
                    else {
                        gameComplete = true;
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
        td_enemyMovement enemyMovement = enemy.GetComponent<td_enemyMovement>();
        enemyMovement.pathManager = pathManager;
        enemyMovement.enemyIndex = currentSpawnIndex;
    }
}
