using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        /** The name of the wave. */
        public string waveName;

        /** List of enemy groups to spawn for the wave. */
        public List<EnemyGroup> enemyGroups;

        /** The total number of enemies to spawn each wave. */
        public int waveQuota;

        /** The interval at which to spawn the wave. */
        public float spawnInterval;

        /** The number of enemies to spawn for the wave. */
        public int spawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
	{
        /** The name of the enemy. */
        public string enemeyName;

        /** The number of enemies to spawn each wave. */
        public int enemyCount;

        /** The number of enemies already spawned this wave. */
        public int spawnCount;

        /** The prefab of the enemy. */
        public GameObject enemyPrefab;
	}

    /** A list of all waves to spawn in the game. */
    public List<Wave> waves;

    /** Index of the current wave. */
    public int currentWaveCount;

    /** Timer used to determine when to spawn the next enemy. */
    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;

    /** The location of the player. */
    Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if ( currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 )
		{
            StartCoroutine(BeginNextWave());
		}

        /** Spawn enemies. */
        spawnTimer += Time.deltaTime;
        if ( spawnTimer >= waves[currentWaveCount].spawnInterval )
		{
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
	{
        /** Wait for 'waveInterval' seconds before starting next wave. */
        yield return new WaitForSeconds(waveInterval);

        /** If there are more waves to start after the current wave, move onto next wave. */
        if ( currentWaveCount < waves.Count - 1 )
		{
            currentWaveCount++;
            CalculateWaveQuota();
		}
	}

    void CalculateWaveQuota()
	{
        int currentWaveQuota = 0;
        foreach ( var enemyGroup in waves[currentWaveCount].enemyGroups )
		{
            currentWaveQuota += enemyGroup.enemyCount;
		}

        waves[currentWaveCount].waveQuota = currentWaveQuota;
	}

    void SpawnEnemies()
    {
        /** Check if the minimum number of enemies for the wave have been spawned. */
        if ( waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota  && !maxEnemiesReached )
		{
            /** Spawn each enemy type until the quota is filled. */
            foreach ( var enemyGroup in waves[currentWaveCount].enemyGroups )
			{
                /** Check if the minimum number of enemies of this type have been spawned. */
                if ( enemyGroup.spawnCount < enemyGroup.enemyCount )
				{
                    /** Check if maximum number of enemies have been spawned. */
                    if ( enemiesAlive >= maxEnemiesAllowed )
                    { 
                        maxEnemiesReached = true;
                        return;
					}

                    /** Spawn Enemy. */
                    Instantiate(enemyGroup.enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);

                    /** Update variables. */
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
				}
			}
		}

        /** Reset flag to allow enemies to spawn again. */
        if ( enemiesAlive < maxEnemiesAllowed )
		{
            maxEnemiesReached = false;
		}
	}

    /** Call this function when an enemy is killed. */
    public void OnEnemyKilled()
	{
        /** Decrement the number of enemies alive. */
        enemiesAlive--;

    }

    public Vector2 GetRandomSpawnPosition()
	{

        /** Generate random position offscreen. */
        float xPosition = Random.Range(-0.2f, 1.2f);
        float yPosition = Random.Range(-0.2f, 1.2f);
        switch (Random.Range(1, 5))
        {
            case 1: // Left
                xPosition = -0.2f;
                break;
            case 2: // Right
                xPosition = 1.2f;
                break;
            case 3: // Up
                yPosition = -0.2f;
                break;
            case 4: // Down
                yPosition = 1.2f;
                break;
        }

        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(xPosition, yPosition, 0f));
        return spawnPosition;
    }
}
