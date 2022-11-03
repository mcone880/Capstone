using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : Projectile
{
    [SerializeField] float travelSpeed;
    [SerializeField] float rocketDmg;
    [SerializeField] float destroyTime;
    [SerializeField] float explosionRadius;

    private void Start()
    {
        DamageScript = gameObject.GetComponent<Damage>();
        Speed = travelSpeed;
        ProjType = ProjectileType.CONT_MOVEMENT;
        DmgType = DamageType.AOE;
        Damage = rocketDmg;
        DestTime = destroyTime;
        ExplosionRad = explosionRadius;
    }
}
