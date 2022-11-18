using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] public float hitRange;
    [SerializeField] float hitDamage;
    [SerializeField] BoxCollider attackHitbox;

    public override void MeleeAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void RangedAttack()
    {
    }

    public override void Start()
    {
        base.Start();
    }
}
