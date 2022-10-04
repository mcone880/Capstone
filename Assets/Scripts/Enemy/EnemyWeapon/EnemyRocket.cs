using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Projectile
{
    [SerializeField] float travelSpeed;
    [SerializeField] float rocketDmg;
    [SerializeField] float destroyTime;
    [SerializeField] float damageFalloff;

    public void Start()
    {
        DamageScript = gameObject.GetComponent<Damage>();
        ProjType = ProjectileType.CONT_MOVEMENT;
        DmgFalloff = damageFalloff;
        DmgType = DamageType.AOE;
        Speed = travelSpeed;
        Damage = rocketDmg;
        DestTime = destroyTime;
    }
}
