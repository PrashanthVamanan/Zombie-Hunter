using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private PlayerController playerController;

    private Vector3 offset = new Vector3(0, 11, -25);

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerController.isGameOver)
        {
            Vector3 desiredPosition = target.transform.position + offset;
            transform.position = desiredPosition;
            transform.LookAt(target);
        }
    }
}
