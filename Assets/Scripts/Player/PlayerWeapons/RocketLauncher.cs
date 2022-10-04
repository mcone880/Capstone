using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    [SerializeField] float fireTime;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = fireTime;
    }
}
