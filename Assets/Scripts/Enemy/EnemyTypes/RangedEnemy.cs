using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] public float minPlayerDist;
    [SerializeField] public float maxPlayerDist;
    public void Start()
    {
        isRanged = true;
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
    }
}
