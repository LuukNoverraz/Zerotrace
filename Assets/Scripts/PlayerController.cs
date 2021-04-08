using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public Animator animator;
    public Collider capsuleCollider;
    public Rigidbody rb;

    private Vector3 standardCenter = new Vector3(0, 82.8f, 0);
    private float standardHeight = 167.8f;

    private Vector3 crouchCenter = new Vector3(0, 73.7f, 0);
    private float crouchHeight = 147.3f;

    public float movementSpeed = 4f;
    public float movementMultiplier = 60f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public float rotationSpeed = 30f;
    public float deadZoneDegrees = 15f;

    private bool isGrounded = true;

    private Vector3 cameraDirection;
    private Vector3 playerDirection;
    private Quaternion targetRotation;

    void FixedUpdate()
    {
        if (!Input.anyKeyDown)
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

    public void MovementStates()
    {   
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            DisableParameters();
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isStrafingLeft", true);
                animator.SetBool("isStrafingRight", true);
            }
            rb.velocity = transform.forward * movementSpeed * movementMultiplier * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }

        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
        {
            DisableParameters();
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = -transform.right * 2 + transform.forward * (movementSpeed * 1.75f) * movementMultiplier * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = transform.right * 2 + transform.forward * (movementSpeed * 1.75f) * movementMultiplier * Time.deltaTime;
            }
            else
            {
                rb.velocity = transform.forward * (movementSpeed * 1.75f) * movementMultiplier * Time.deltaTime;
            }
            animator.SetBool("isRunning", true);
        }

        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            DisableParameters();
            animator.SetBool("isWalkingBackward", true);
            rb.velocity = transform.forward * (movementSpeed * -0.75f) * movementMultiplier * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftControl))
        {
            DisableParameters();
            animator.SetBool("isStrafingLeft", true);
            rb.velocity = -transform.right * (movementSpeed * 0.75f) * movementMultiplier * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftControl))
        {
            DisableParameters();
            animator.SetBool("isStrafingRight", true);
            rb.velocity = transform.right * (movementSpeed * 0.75f) * movementMultiplier * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.W))
        {
            DisableParameters();
            animator.SetBool("isCrouching", true);
            CrouchCollider();
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isStrafingLeft", true);
                rb.velocity = -transform.right * (movementSpeed * 0.5f) * movementMultiplier * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                animator.SetBool("isStrafingRight", true);
                rb.velocity = transform.right * (movementSpeed * 0.5f) * movementMultiplier * Time.deltaTime;
            }
        }

        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            DisableParameters();
            CrouchCollider();
            animator.SetBool("isSneaking", true);
            rb.velocity = transform.forward * (movementSpeed * 0.75f) * movementMultiplier * Time.deltaTime;
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            DisableParameters();
            rb.velocity = transform.up * 1000 * Time.deltaTime;
        }
    }
    
    public void StandardCollider()
    {
        capsuleCollider.GetComponent<CapsuleCollider>().center = standardCenter;
        capsuleCollider.GetComponent<CapsuleCollider>().height = standardHeight;
    }

    public void CrouchCollider()
    {
        capsuleCollider.GetComponent<CapsuleCollider>().center = crouchCenter;
        capsuleCollider.GetComponent<CapsuleCollider>().height = crouchHeight;
    }

    public void DisableParameters()
    {
        rb.velocity = Vector3.zero;

        StandardCollider();
        
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    #endregion
}
