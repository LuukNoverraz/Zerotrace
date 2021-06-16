using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public CharacterController controller;
    public Rigidbody rb;
    public Animator animator;
    public Animation dashbarAnimation;
    public Animation caughtAnimation;
    public Button[] caughtButtons;
    public LayerMask playerMask;

    private bool crouching;
    private bool warpReady = true;
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

        if(Input.GetKeyDown(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if((Time.time - lastTapTimeW) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeW = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if((Time.time - lastTapTimeA) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeA = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            if((Time.time - lastTapTimeS) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeS = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            if((Time.time - lastTapTimeD) < tapSpeed)
            {
                DoubleTap();
            }
            
            lastTapTimeD = Time.time;
        }
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, -0.275f, transform.position.z);
    }

    public void DoubleTap()
    {
        RaycastHit hit;
        if (warpReady && !Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            dashbarAnimation.Play();
            controller.enabled = false;
            transform.position += transform.forward * 2f;
            controller.enabled = true;
            warpReady = false;
            StartCoroutine("Cooldown");
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds (5);
        warpReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            caughtAnimation.Play();
            caughtButtons[0].interactable = true;
            caughtButtons[1].interactable = true;
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Key") 
        {
            gameController.GotKey();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Enemy Vision")
        {
            other.gameObject.transform.parent.gameObject.GetComponent<GuardController>().chasing = true;
            Debug.Log(other.gameObject.transform.parent.gameObject.GetComponent<GuardController>().chasing);
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


