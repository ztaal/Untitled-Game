using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        /** The name of the wave. */
        public string waveName;

        /** List of enemies to spawn for the wave. */
        public List<GameObject> enemyPrefabs;

        /** List of enemy names. */
        public List<string> enemyName;

        /** The number of enemies to spawn each wave. */
        public List<int> enemyCount;

        /** The total number of enemies to spawn each wave. */
        public int waveQuota;

        /** The interval at which to spawn the wave. */
        public float spawnInterval;

        /** The number of enemies to spawn for the wave. */
        public int spwanCount;
    }

    /** A list of all waves to spawn in the game. */
    public List<Wave> waves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
