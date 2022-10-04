using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float jumpForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            Debug.Log("Collide");
            PlayerController player = other.GetComponent<PlayerController>();
            player.canDoubleJump = true;
            player.Jump(jumpForce, false);
        }
    }
}
