using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool crouching;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private Vector3 standardCenter = new Vector3(0, 90f, 0);
    private float standardHeight = 170f;

    private Vector3 crouchCenter = new Vector3(0, 65f, 0);
    private float crouchHeight = 110f;

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move == Vector3.zero)
        {
            DisableParameters();
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("isCrouching", true);
            }
        }

        if (move != Vector3.zero)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            {
                DisableParameters();
                animator.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                DisableParameters();
                animator.SetBool("isRunning", true);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                DisableParameters();
                animator.SetBool("isSneaking", true);
            }
            transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            DisableParameters();
            animator.SetBool("isJumping", true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DisableParameters();
            playerSpeed *= 2f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DisableParameters();
            playerSpeed /= 2f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DisableParameters();
            playerSpeed /= 1.5f;
            controller.center = crouchCenter;
            controller.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            DisableParameters();
            playerSpeed *= 1.5f;
            controller.center = standardCenter;
            controller.height = standardHeight;
        }
    }

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


