using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool crouching;
    private float playerSpeed = 2.5f;

    public float tapSpeed = 0.5f;
    private float lastTapTimeW = 0;
    private float lastTapTimeA = 0;
    private float lastTapTimeS = 0;
    private float lastTapTimeD = 0;

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
        move = Vector3.ClampMagnitude(move, 1f);
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

        if(Input.GetKeyDown(KeyCode.W))
        {
            if((Time.time - lastTapTimeW) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeW = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            if((Time.time - lastTapTimeA) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeA = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if((Time.time - lastTapTimeS) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeS = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            if((Time.time - lastTapTimeD) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeD = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DisableParameters();
            playerSpeed = 5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DisableParameters();
            playerSpeed = 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DisableParameters();
            playerSpeed = 2f;
            controller.center = crouchCenter;
            controller.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            DisableParameters();
            playerSpeed = 2.5f;
            controller.center = standardCenter;
            controller.height = standardHeight;
        }
    }

    public void DoubleTap()
    {
        transform.position += transform.forward * 1.25f;

        // Debug.Log("OK");
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


