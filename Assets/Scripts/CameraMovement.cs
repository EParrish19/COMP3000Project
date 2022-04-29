using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerCamera;

    [SerializeField]
    private GameObject player;

    private Vector3 newCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //camera is moved every frame to follow player position
        newCameraPos = player.transform.position;
        newCameraPos.y += 23.75f;

        playerCamera.position = newCameraPos;
    }
}
