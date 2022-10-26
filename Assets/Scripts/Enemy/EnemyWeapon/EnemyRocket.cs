using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Projectile
{
    [SerializeField] float travelSpeed;
    [SerializeField] float rocketDmg;
    [SerializeField] float destroyTime;

    public void Start()
    {
        DamageScript = gameObject.GetComponent<Damage>();
        ProjType = ProjectileType.CONT_MOVEMENT;
        DmgType = DamageType.AOE;
        Speed = travelSpeed;
        Damage = rocketDmg;
        DestTime = destroyTime;
    }
}
