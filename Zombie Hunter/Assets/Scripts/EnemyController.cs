using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody enemyRb;

    private PlayerController playerController;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Make the enemy move down the screen if the game is not over yet 
        if (!playerController.isGameOver)
        {
            enemyRb.AddForce(Vector3.forward * -speed);
        }

        //If game ends, destroy the remaining enemies in the game
        if(playerController.isGameOver)
        {
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            destroyAllEnemies(enemies);
        }


    }
    void destroyAllEnemies(EnemyController[] enemies)
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If enemy is hit by a projectile
        if (other.gameObject.CompareTag("Projectile"))
        {
            spawnManager.enemyDestroyed(gameObject);
            Destroy(other.gameObject);
        }

    }
}
