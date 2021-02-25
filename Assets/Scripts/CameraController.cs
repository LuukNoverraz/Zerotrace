using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 2.0f;
    void Update()
    {
        player.transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * sensitivity);
    }
}
