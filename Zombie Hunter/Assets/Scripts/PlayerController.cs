using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    private float bottomBound = -24.0f;
    private float projectileDelay = 0.6f;
    private float enemyKnockBackStrength = 500;
    private int playerHealth = 6;
    private bool canFireProjectile = true;

    public GameObject projectilePrefab;
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        restrictPlayerBounds();

        //Instantiate the projectile if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && canFireProjectile)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            canFireProjectile = false;
            StartCoroutine(projectileCountDownRoutine());
        }

    }

    //Move the player based on arrow key input
    void movePlayer()
    {
        //Get references to the vertical axis
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //Add forces to make player move 
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    //Restrict the player's movement on the z-axis
    void restrictPlayerBounds()
    {
        if (transform.position.z < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBound);
        }
    }

    //Avoid spamming of projectiles by player, by introducing a delay
    IEnumerator projectileCountDownRoutine()
    {
        yield return new WaitForSeconds(projectileDelay);
        canFireProjectile = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If collision occurs between enemy and player
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Calculate knockback force to be applied to the player
            Vector3 knockBackVector = (gameObject.transform.position - collision.gameObject.transform.position).normalized;
            playerRb.AddForce(knockBackVector * enemyKnockBackStrength, ForceMode.Impulse);

            //Decrease player's health with each hit from the enemy
            playerHealth--;

            //If player's health reaches zero, destroy the player
            if (playerHealth == 0)
            {
                Destroy(gameObject); //Game ends here
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player has picked up a powerup
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
