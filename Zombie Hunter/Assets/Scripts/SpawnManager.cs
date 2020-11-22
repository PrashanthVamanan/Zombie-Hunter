using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 playerStartPos = new Vector3(0.43f, 0.7f, -24);

    private GameObject player;
    private GameObject currentPowerUp;

    private int waveNumber = 1;

    //Spawn variables
    private float spawnXRange = 20.0f;
    private float spawnZPos = 20.0f;
    private float spawnYPos = 0.7f;
    private float powerUpSpawnYPos = 0.5f;

    //Power up related variables
    private int powerUpCoolDownDelay = 10;
    private bool isPowerUpPresent = false;

    public GameObject[] enemyPrefabs;
    public GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Get the number of enemies currently present in the scene
        int noOfCurrentEnemies = FindObjectsOfType<EnemyController>().Length;

        //Get the powerup currently present int the scene
        currentPowerUp = GameObject.FindGameObjectWithTag("Powerup");

        //If there is no powerup already present spawn a new powerup
        if (currentPowerUp == null && !isPowerUpPresent)
        {
            spawnPowerUp();
        }

        //If all enemies have been destroyed in a wave, spawn the next wave
        if (noOfCurrentEnemies == 0)
        {
            resetPlayerPositionBeforeNextWave();
            spawnRandomEnemiesInWaves(waveNumber);
        }

    }

    //Spawn random enemies at random positions
    void spawnRandomEnemiesInWaves(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);

            GameObject enemyToSpawn = enemyPrefabs[enemyIndex];

            Instantiate(enemyToSpawn, returnRandomSpawnPosition(true), enemyToSpawn.transform.rotation);
        }

        waveNumber++;
    }

    //Spawn a powerup at a random position if a powerup is not already present
    void spawnPowerUp()
    {
        if (!isPowerUpPresent)
        {
                Instantiate(powerUpPrefab, returnRandomSpawnPosition(false), powerUpPrefab.transform.rotation);
                isPowerUpPresent = true;
                StartCoroutine(powerUpCoolDownRoutine());
            
        }
    }

    //Co-routine to introduce delay between spawning of powerups
    IEnumerator powerUpCoolDownRoutine()
    {
        yield return new WaitForSeconds(powerUpCoolDownDelay);

        //If player didn't pick up the powerup after x amount of seconds
        //automatically destroy the powerup
        if (currentPowerUp != null)
        {
            Destroy(currentPowerUp);
            yield return new WaitForSeconds(powerUpCoolDownDelay);
        }

        isPowerUpPresent = false;
    }

    //Return a random spawn position for the enemy and powerup
    Vector3 returnRandomSpawnPosition(bool isEnemy)
    {
        float spawnXPos = Random.Range(-spawnXRange, spawnXRange);
        float zSpawnPos = Random.Range(-spawnZPos, spawnZPos);

        Vector3 spawnPos;

        if (isEnemy)
        {
            spawnPos = new Vector3(spawnXPos, spawnYPos, spawnZPos);
        }
        else
        {
            spawnPos = new Vector3(spawnXPos, powerUpSpawnYPos, zSpawnPos);
        }

        return spawnPos;
    }

    //Reset the player position to his start position before the next wave
    void resetPlayerPositionBeforeNextWave()
    {
        player.transform.position = playerStartPos;
    }
}
