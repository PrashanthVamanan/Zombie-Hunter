using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private AudioSource playerAudio;

    private float bottomBound = -24.0f;
    private float rightBound = 18.0f;
    private float topBound = -15.0f;

    private float projectileDelay = 0.6f;
    private float enemyKnockBackStrength = 200;

    private bool canFireProjectile = true;

    public GameObject projectilePrefab;

    public float speed = 10.0f;
    public float playerMisses = 1;
    public bool isGameOver = false;

    //Audio clips
    public AudioClip gameOverSound;
    public AudioClip projectileFiredSound;
    public AudioClip playerHurtSound;
    public AudioClip powerUpSound;

    //Particle Effects
    public ParticleSystem gameOverParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        restrictPlayerBounds();

        //Instantiate the projectile if spacebar is pressed and game is not over
        if (Input.GetKeyDown(KeyCode.Space) && canFireProjectile && !isGameOver)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            canFireProjectile = false;
            playerAudio.PlayOneShot(projectileFiredSound);
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
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    //Restrict the player's movement on the z-axis
    void restrictPlayerBounds()
    {
        if (transform.position.z < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBound);
        }

        if (transform.position.z > topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }

        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);

        }

        if (transform.position.x < -rightBound)
        {
            transform.position = new Vector3(-rightBound, transform.position.y, transform.position.z);

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

            playerAudio.PlayOneShot(playerHurtSound);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player has picked up a powerup
        if (other.gameObject.CompareTag("Powerup"))
        {
            playerAudio.PlayOneShot(powerUpSound);
            Destroy(other.gameObject);
            updatePlayerMisses(0.5f, true);
        }
    }

    //Keep track of number of enemies the player has missed
    public void updatePlayerMisses(float missValue, bool isPowerUp)
    {
        if (!isPowerUp)
        {
            playerMisses -= missValue;
        }
        else
        {
            playerMisses += missValue;
        }

        if (playerMisses <= 0)
        {
            isGameOver = true;
            Instantiate(gameOverParticle, transform.position, gameOverParticle.transform.rotation);
            playerAudio.PlayOneShot(gameOverSound);

            //Destroy the player after the game over sound has finished playing
            Destroy(gameObject, gameOverSound.length); 
        }

    }
}
