using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public Vector3[] positions;
    public Vector3[] rotations;

    void Start()
    {
       offset = transform.position; 
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
