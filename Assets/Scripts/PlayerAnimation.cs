using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{   
    public PlayerController playerController;
    public Animator animator;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift)) 
        {
            animator.SetBool("isWalking", true);
            transform.position += transform.forward * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            playerController.movementSpeed = 4;
            transform.position += transform.forward * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", false);
            playerController.movementSpeed = 2;
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftControl)) 
        {
            animator.SetBool("isWalkingBackward", true);
            playerController.movementSpeed = 1;
            transform.position -= transform.forward * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.S)) 
        {
            animator.SetBool("isWalkingBackward", false);
            playerController.movementSpeed = 2;
        }

        if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isCrouching", true);
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isSneaking", true);
            playerController.movementSpeed = 1.5f;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isSneaking", false);
            animator.SetBool("isWalking", true);
            playerController.movementSpeed = 2;
        }
        
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isSneaking", false);
            animator.SetBool("isCrouching", true);
            playerController.movementSpeed = 2;
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isStrafingLeft", true);
            playerController.movementSpeed = 1.5f;
            transform.position += -transform.right * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isStrafingLeft", false);
            playerController.movementSpeed = 2;
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isStrafingRight", true);
            playerController.movementSpeed = 1.5f;
            transform.position += transform.right * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isStrafingRight", false);
            playerController.movementSpeed = 2;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isStrafingLeft", false);
            animator.SetBool("isStrafingRight", false);
            playerController.movementSpeed = 2;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isStrafingLeft", true);
            animator.SetBool("isWalking", true);
            transform.position += ((transform.forward + -transform.right) / 2) * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isStrafingLeft", false);
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("isWalking", false);
            }
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isStrafingRight", true);
            animator.SetBool("isWalking", true);
            transform.position += ((transform.forward + transform.right) / 2) * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isStrafingRight", false);
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("isWalking", false);
            }
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) 
        {
            playerController.movementSpeed = 1;
            transform.position += ((-transform.forward + -transform.right) / 2) * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isWalkingBackward", false);
            playerController.movementSpeed = 2;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) 
        {
            playerController.movementSpeed = 1;
            transform.position += ((-transform.forward + transform.right) / 2) * playerController.movementSpeed * Time.deltaTime;
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isWalkingBackward", false);
            playerController.movementSpeed = 2;
        }
    }
}
