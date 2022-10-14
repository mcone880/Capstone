using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpSpeed = 3.5f;
    [SerializeField] Camera playerCamera;
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    [HideInInspector] public CharacterController character;
    [HideInInspector] public bool gravityOn = true;
    [HideInInspector] public Vector3 direction = Vector3.zero;
    [HideInInspector] public Vector3 inputDir = Vector3.zero;
    [HideInInspector] public bool canDoubleJump = false;
    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public bool camControl = true;
    private float rotationX = 0f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        character = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = Input.GetAxis("Vertical");
        float curSpeedY = Input.GetAxis("Horizontal");

        inputDir = (forward * curSpeedX) + (right * curSpeedY);

        if(character.isGrounded)
        {
            canDoubleJump = true;
            if (Input.GetButtonDown("Jump")) Jump(jumpSpeed);
        } 
        else
        {
            if(Input.GetButtonDown("Jump") && canDoubleJump)
            {
                Jump(jumpSpeed);
                canDoubleJump = false;
            }
        }

        if (character.isGrounded && direction.y < 0) direction.y = 0;
        if(gravityOn) direction.y -= gravity * Time.deltaTime;
        if(!isDashing) character.Move(moveSpeed * Time.deltaTime * (direction + inputDir));

        if (direction.y > 0)
        {
            if (Physics.Raycast(transform.position + (Vector3.up * character.height * 0.5f), Vector3.up, out RaycastHit hitinfo, 2f))
            {
                print("Raycast");
                direction.y = 0;
                inputDir.y = 0;
            }
        }

        if(camControl)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void Jump(float jumpForce)
    {
        Jump(jumpForce, Vector3.up);
    }

    public void Jump(float jumpForce, Vector3 dir)
    {
        direction = dir * jumpForce;
    }
}
