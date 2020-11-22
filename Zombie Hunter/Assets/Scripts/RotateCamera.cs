using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private float turnSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (!playerController.isGameOver)
        {
            //Rotate the focal point around the y-axis on left and right arrow key press
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

            //Make the focal point follow the player as the player moves
            transform.position = player.transform.position;
        }
    }
}
