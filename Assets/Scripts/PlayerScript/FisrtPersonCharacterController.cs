using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;

public class FisrtPersonCharacterController : MonoBehaviour
{
    // The GameObject is made to bounce using the space key.
    // Also the GameOject can be moved forward/backward and left/right.
    // Add a Quad to the scene so this GameObject can collider with a floor.

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 9.810f;
    public int playerNumber;
    public int joystickNumber;
    public int health = 100;
    private Animator animator;

    private Vector3 moveDirection = Vector3.zero;
    //private NavMeshAgent agent;
    private CharacterController controller;

    [SerializeField]
    private GameObject bulletsEffect;
    [SerializeField]
    private GameObject hitEffect;

    private Camera viewPort;

    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //agent.updatePosition = true;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        viewPort = GetComponentInChildren<Camera>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        Debug.Log("playerNumber: " + playerNumber + " -- joystickNumber: " + joystickNumber);
        // let the gameObject fall down
        //gameObject.transform.position = new Vector3(0, 5, 0);
    }

    void Update()
    {

        if (Input.GetAxis("Cancel") > 0 )
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate()
    {
        CalculateFireByPlayer(playerNumber);
        CalculateCharacterMovementByPlayer(playerNumber);
    }
    void CalculateCharacterMovementByPlayer(int playerNumber)
    {
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            float HorizontalMovement;
            float VerticalMovement;
            bool JumpButton;

            if (playerNumber == 1)
            {
                HorizontalMovement = Input.GetAxis("Horizontal");
                VerticalMovement = Input.GetAxis("Vertical");
                JumpButton = Input.GetButton("Jump");

                if(HorizontalMovement == 0)
                {
                    HorizontalMovement = Input.GetAxis("Horizontal_Joystick" + joystickNumber);
                }
                if(VerticalMovement == 0)
                {
                    VerticalMovement = Input.GetAxis("Vertical_Joystick" + joystickNumber);
                }
                if(!JumpButton)
                {
                    JumpButton = Input.GetButton("Jump_Joystick" + joystickNumber);
                }
            }
            else
            {
                HorizontalMovement = Input.GetAxis("Horizontal_Joystick" + joystickNumber);
                VerticalMovement = Input.GetAxis("Vertical_Joystick" + joystickNumber);
                JumpButton = Input.GetButton("Jump_Joystick" + joystickNumber);
            }
            
            moveDirection = new Vector3(HorizontalMovement, 0.0f, VerticalMovement);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            animator.SetFloat("Speed", Mathf.Abs(VerticalMovement * speed));

            if (JumpButton)
            {
                moveDirection.y = jumpSpeed;
                animator.SetTrigger("isJumping");
            }

        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

    }

    void CalculateFireByPlayer(int playerNumber)
    {
        bool FireButton = Input.GetButton("Fire1");

        if(playerNumber == 1)
        {
            if(!FireButton)
            {
                FireButton = Input.GetButton("Fire1_Joystick" + joystickNumber);
            }
        }
        else
        {
            FireButton = Input.GetButton("Fire1_Joystick" + joystickNumber);
        }

        if (FireButton)
        {
            Ray rayOrigin = viewPort.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            bulletsEffect.SetActive(true);

            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
                GameObject hitEffectTemp = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                hitEffectTemp.SetActive(true);
                Destroy(hitEffectTemp, 1.0f);

                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                gameManager.HitManager(this.gameObject, hitInfo.transform.name);
            }
        }
        else
        {
            bulletsEffect.SetActive(false);
        }
    }
}
