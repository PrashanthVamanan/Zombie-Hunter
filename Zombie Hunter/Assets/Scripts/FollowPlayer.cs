using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private PlayerController playerController;
    private AudioSource cameraAudio;

    private Vector3 offset = new Vector3(0, 11, -25);

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraAudio = GetComponent<AudioSource>();
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
        //If game is over stop playing background music
        else
        {
            cameraAudio.Stop();
        }
    }
}
