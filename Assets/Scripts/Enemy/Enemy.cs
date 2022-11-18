using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float sightDist;
    [SerializeField] protected Transform eyes;
    [SerializeField] protected NavMeshAgent navMesh;
    protected int agressiveness;
    protected Area currentArea;
    protected Health health;
    protected GameObject player;

    [HideInInspector] public bool canMove = true;

    public virtual void Start()
    {
        player = GameObject.Find("FPSPlayer");
        health = GetComponent<Health>();

        Collider[] collisions = Physics.OverlapSphere(transform.position, 1);
        foreach(var collision in collisions)
        {
            if(collision.gameObject.TryGetComponent<Area>(out Area area))
            {
                currentArea = area;
            }
        }
    }

    public abstract void MeleeAttack();
    public abstract void RangedAttack();
}
