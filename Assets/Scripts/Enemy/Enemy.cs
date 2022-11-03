using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Movement movement;
    [HideInInspector]public bool isRanged;
    protected Health health;

    public void Update()
    {
        movement.Move();
    }
}
