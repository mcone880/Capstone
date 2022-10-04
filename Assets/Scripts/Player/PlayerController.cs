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
    [HideInInspector] public bool canDoubleJump = false;
    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public float directionY;
    private float rotationX = 0f;

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


        direction = (forward * curSpeedX) + (right * curSpeedY);
        
        if(character.isGrounded)
        {
            canDoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Jump(jumpSpeed, false);
            }
        } 
        else
        {
            if(Input.GetButtonDown("Jump") && canDoubleJump)
            {
                directionY = 0;
                Jump(jumpSpeed, true);
                canDoubleJump = false;
            }
        }

        if(gravityOn) directionY -= gravity * Time.deltaTime;
        direction.y = directionY;

        if(!isDashing) character.Move(direction * moveSpeed * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    public void Jump(float jumpForce, bool isDouble)
    {
        if (isDouble) directionY += jumpForce;
        else directionY = jumpForce;
    }
}
