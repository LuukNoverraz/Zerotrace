using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    private Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
        // player.transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * sensitivity);
    }
}
