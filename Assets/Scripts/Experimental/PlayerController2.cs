using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public GameObject speedLines;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool crouching;
    private float playerSpeed = 3.5f;
    private float jumpHeight = 1.75f;
    private float gravityValue = -9.81f * 3.5f;

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
            // DisableParameters();
            // animator.SetBool("isJumping", true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            SpeedLines();
        }

        // if (playerVelocity.y < 0)
        // {
        //     DisableParameters()
        //     animator.SetBool("isFalling", true);
        // }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DisableParameters();
            playerSpeed = 6f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DisableParameters();
            playerSpeed = 3f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DisableParameters();
            playerSpeed = 2f;
            // controller.center = crouchCenter;
            // controller.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            DisableParameters();
            playerSpeed = 3f;
            // controller.center = standardCenter;
            // controller.height = standardHeight;
        }
    }

    public void SpeedLines()
    {
        GameObject newSpeedLines = Instantiate(speedLines, transform.position, Quaternion.identity);
        Destroy(newSpeedLines, 2f);
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


