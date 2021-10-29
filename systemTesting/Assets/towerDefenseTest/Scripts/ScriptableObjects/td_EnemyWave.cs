using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStruct {
    public GameObject enemyPrefab;
    public float spawnTime;
}

[CreateAssetMenu(fileName = "EnemyWave", menuName = "Scriptable Objects/Tower Defense/Enemy Wave")]
public class td_EnemyWave : ScriptableObject
{
    public EnemyStruct[] enemySpawnTimes;
}
