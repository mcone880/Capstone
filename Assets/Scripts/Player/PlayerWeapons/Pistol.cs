using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] float fireTime;
    [SerializeField] int RayDamage;
    [SerializeField] int RayNum;
    [SerializeField] List<Vector2> deviations;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = fireTime;
        dmgPerRay = RayDamage;
        rayNum = RayNum;
        deviationList = deviations;
    }
}
