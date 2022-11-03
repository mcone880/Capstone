using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;

    [HideInInspector]public bool canMove = true;
    
    private GameObject player;
    private CharacterController controller;
    private Enemy enemyScript;
    private float playerDist;
    public void Start()
    {
        player = GameObject.Find("FPSPlayer");
        controller = GetComponent<CharacterController>();
        enemyScript = GetComponent<Enemy>();
    }

    public void Move()
    {
        playerDist = Vector3.Distance(transform.position, player.transform.position);
        if (enemyScript.isRanged) RangeMovement();
        else MeleeMovement();

    }

    private void RangeMovement()
    {
        RangedEnemy me = (RangedEnemy)enemyScript;
        if (playerDist < me.minPlayerDist)
        {
            //Run away from the player
            Vector3 awayDir = (transform.position - player.transform.position).normalized;
            awayDir.y = 0;
            controller.Move(awayDir * speed * Time.deltaTime);
        }
        else if (playerDist > me.maxPlayerDist)
        {
            Vector3 towardsDir = (player.transform.position - transform.position).normalized;
            towardsDir.y = 0;
            controller.Move(towardsDir * speed * Time.deltaTime);
        }
        else
        {
            //Call Attack mode or something
            print("RangedAttack");
        }
    }

    private void MeleeMovement()
    {
        MeleeEnemy me = (MeleeEnemy)enemyScript;
        if (playerDist > me.hitRange)
        {
            Vector3 towardsDir = (player.transform.position - transform.position).normalized;
            towardsDir.y = 0;
            controller.Move(towardsDir * speed * Time.deltaTime);
        } else
        {
            //ATTACK
            print("MeleeAttack");
        }
    }


}
