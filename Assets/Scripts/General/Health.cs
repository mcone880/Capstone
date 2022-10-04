using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxhealth;

    public void Start()
    {
        health = maxhealth;
    }

    public void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Death animation?
        //Other code
        //Drop Health??
        //Might need the way i was killed
        Destroy(gameObject);
    }
}
