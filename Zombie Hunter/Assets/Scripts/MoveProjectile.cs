using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    private GameObject focalPoint;

    private float topBound = 24.0f;
    private float rightBound = 24.0f;

    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        //Move the projectile in the direction the player is facing
        transform.Translate(focalPoint.transform.forward * speed * Time.deltaTime);

        //Destroy the projectile if it goes outside of the play area
        DestroyOutOfBounds();
        
    }

    //If projectile leaves the play area, destroy it
    void DestroyOutOfBounds()
    {
        if(transform.position.z > topBound || transform.position.z < -topBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.x > rightBound || transform.position.x < -rightBound)
        {
            Destroy(gameObject);
        }
    }
}
