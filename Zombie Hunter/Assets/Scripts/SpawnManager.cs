using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject currentPowerUp;

    public float spawnRate = 2.0f;
    private float startDelay = 1.0f;

    //Enemy Spawn variables
    private float spawnXRange = 18.0f;
    private float spawnZPos = 20.0f;
    private float spawnYPos = 0.7f;

    //Power up Spawn variables
    private float powerUpSpawnZStartPos = -15.0f;
    private float powerUpSpawnZEndPos = -23.0f;
    private float powerUpSpawnYPos = 0.5f;

    //Power up related variables
    private int powerUpCoolDownDelay = 15;
    private bool isPowerUpPresent = false;

    public GameObject[] enemyPrefabs;
    public GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log(playerController.isGameOver);

        InvokeRepeating("spawnRandomEnemies", startDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {

        //Get the powerup currently present int the scene
        currentPowerUp = GameObject.FindGameObjectWithTag("Powerup");

        //If there is no powerup already and game is not over, present spawn a new powerup
        if (currentPowerUp == null && !isPowerUpPresent && !playerController.isGameOver)
        {
            spawnPowerUp();
        }
    }

    //Spawn random enemies at random positions
    void spawnRandomEnemies()
    {
        if (!playerController.isGameOver)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[enemyIndex];

            Instantiate(enemyToSpawn, returnRandomSpawnPosition(true), enemyToSpawn.transform.rotation);
        }
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
        float zPowerupSpawnPos = Random.Range(powerUpSpawnZStartPos, powerUpSpawnZEndPos);

        Vector3 spawnPos;

        if (isEnemy)
        {
            spawnPos = new Vector3(spawnXPos, spawnYPos, spawnZPos);
        }
        else
        {
            spawnPos = new Vector3(spawnXPos, powerUpSpawnYPos, zPowerupSpawnPos);
        }
        return spawnPos;
    }
}
