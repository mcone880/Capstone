using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float Speed;
    protected float Damage;
    protected float ExplosionRad;
    protected ProjectileType ProjType;
    protected DamageType DmgType;
    protected Damage DamageScript;
    protected float DmgFalloff;
    protected float Knockback;
    protected float DestTime;
    
    public enum ProjectileType
    {
        CONT_MOVEMENT,
        ONE_SHOT
    }

    public enum DamageType
    {
        AOE,
        SINGLE
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (DmgType == DamageType.AOE) DamageScript.CreateExplosion(ExplosionRad, Damage, DmgFalloff, Knockback);
        else DamageScript.DealDamage(other, Damage, Knockback);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(ProjType == ProjectileType.CONT_MOVEMENT)transform.position += transform.forward * Speed * Time.deltaTime;
        Destroy(gameObject, DestTime);
    }
}
