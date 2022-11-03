using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] public float hitRange;
    [SerializeField] float hitDamage;

    public void Start()
    {
        isRanged = false;
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
    }
}
