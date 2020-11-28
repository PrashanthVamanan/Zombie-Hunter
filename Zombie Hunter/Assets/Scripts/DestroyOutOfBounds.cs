using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private PlayerController playerControllerScript;

    private float bottomBound = -25.0f;
    private float topBound = 24.0f;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyTargetIfOutOfBounds();
    }

    void destroyTargetIfOutOfBounds()
    {
        if (target.transform.position.z < bottomBound && target.CompareTag("Enemy"))
        {
            Destroy(target);
            playerControllerScript.updatePlayerMisses(1, false);
        }

        if(target.transform.position.z > topBound && target.CompareTag("Projectile"))
        {
            Destroy(target);
        }
    }
}
