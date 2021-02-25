using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;

    public float movementSpeed = 10f;
    public float rotationSpeed = 30f;
    public float deadZoneDegrees = 15f;

    private Vector3 cameraDirection;
    private Vector3 playerDirection;
    private Quaternion targetRotation;

    void Update()
    {
        cameraDirection = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z);
        playerDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);

        if (Vector3.Angle(cameraDirection, playerDirection) > 15)
        {
            targetRotation = Quaternion.LookRotation(cameraDirection, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
