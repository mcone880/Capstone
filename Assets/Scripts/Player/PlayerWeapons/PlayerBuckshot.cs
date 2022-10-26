using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuckshot : Projectile
{
    [SerializeField] float travelSpeed;
    [SerializeField] float pelletDmg;
    [SerializeField] float destroyTime;

    private void Start()
    {
        DamageScript = gameObject.GetComponent<Damage>();
        ProjType = ProjectileType.CONT_MOVEMENT;
        DmgType = DamageType.SINGLE;
        Speed = travelSpeed;
        Damage = pelletDmg;
        DestTime = destroyTime;
    }
}
