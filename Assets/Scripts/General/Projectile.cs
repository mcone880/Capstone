using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected bool longRangeWeapon;
    [SerializeField] protected int falloffDistance;

    protected float Speed;
    protected float Damage;
    protected float ExplosionRad;
    protected ProjectileType ProjType;
    protected DamageType DmgType;
    protected Damage DamageScript;
    protected float DestTime;

    public Vector3 spawnPoint;
    
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
        if (DmgType == DamageType.AOE) DamageScript.CreateExplosion(ExplosionRad, Damage, longRangeWeapon, falloffDistance, spawnPoint);
        else DamageScript.DealDamage(other, Damage);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(ProjType == ProjectileType.CONT_MOVEMENT)transform.position += transform.forward * Speed * Time.deltaTime;
        Destroy(gameObject, DestTime);
    }
}
