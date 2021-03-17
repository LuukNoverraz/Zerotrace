using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public Animator animator;
    public Rigidbody rb;

    public float movementSpeed = 4f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public float rotationSpeed = 30f;
    public float deadZoneDegrees = 15f;

    private int keyCount = 0;
    private bool isGrounded = true;

    private Vector3 cameraDirection;
    private Vector3 playerDirection;
    private Quaternion targetRotation;

    void FixedUpdate()
    {
        if(!Input.anyKeyDown)
        {
            DisableParameters();
        }

        MovementStates();
    
        cameraDirection = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z);
        playerDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);

        if (Vector3.Angle(cameraDirection, playerDirection) > 15)
        {
            targetRotation = Quaternion.LookRotation(cameraDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    #region MovementStates;
    void OnCollisionStay(Collision col)
    {      
        if (col.collider.gameObject.CompareTag("Ground"))
        {
            DisableParameters();
            animator.SetBool("hasLanded", true);
            isGrounded = true;
        }
    }
    
    public void MovementStates()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            DisableParameters();
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isStrafingLeft", true);
                animator.SetBool("isStrafingRight", true);
            }
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
        {
            DisableParameters();
            transform.position += transform.forward * (movementSpeed * 1.5f) * Time.deltaTime;
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftControl))
        {
            DisableParameters();
            animator.SetBool("isWalkingBackward", true);
            transform.position -= transform.forward * (movementSpeed * 0.75f) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            DisableParameters();
            animator.SetBool("isStrafingLeft", true);
            transform.position += -transform.right * (movementSpeed * 0.75f) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            DisableParameters();
            animator.SetBool("isStrafingRight", true);
            transform.position += transform.right * (movementSpeed * 0.75f) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.W))
        {
            DisableParameters();
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("isStrafingLeft", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isStrafingRight", true);
            }
            animator.SetBool("isCrouching", true);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            DisableParameters();
            animator.SetBool("isSneaking", true);
            transform.position += transform.forward * (movementSpeed * 0.75f) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            DisableParameters();
            animator.SetBool("isJumping", true);
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    #endregion

    public void DisableParameters()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }
}
