using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] AudioSource bounceSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            bounceSound.Play();
            PlayerController player = other.GetComponent<PlayerController>();
            player.canDoubleJump = true;
            player.Jump(jumpForce, transform.up);
        }
    }
}
