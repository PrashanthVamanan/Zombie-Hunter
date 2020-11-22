﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;
    public int enemyHealth = 2;

    private Rigidbody enemyRb;
    private GameObject player;

    private float bottomBound = -22.0f;
    private float projectileKnockBackStrength = 750;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Make the enemy follow the player
        Vector3 enemyDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(enemyDirection * speed);

        //Restrict enemy bounds in the play area
        restrictEnemyBounds();

        //If enemy health reaches zero, destroy the enemy from the game
        if(enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    //Restrict the movement of the enemy on the z-axis
    void restrictEnemyBounds()
    {
        if (transform.position.z < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If enemy is hit by a projectile
        if (other.gameObject.CompareTag("Projectile"))
        {
            //Calculate the knockback force to be applied to the enemy on hit by a projectile
            Vector3 knockBackForce = (gameObject.transform.position - other.gameObject.transform.position).normalized;
            enemyRb.AddForce(knockBackForce * projectileKnockBackStrength, ForceMode.Impulse);

            Destroy(other.gameObject);

            //Reduce enemy's health with each projectile hit
            enemyHealth--;
          
        }

    }
}