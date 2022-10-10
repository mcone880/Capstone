using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected GameObject ammunition;
    public int weaponNum;
    protected float fireRate;
    private GameObject muzzleObject;
    public float fireTimer = 0;
    public FireType fireType;
    
    [HideInInspector]public bool isUsable = false;

    public enum FireType
    {
        AUTOMATIC,
        SINGLE_SHOT
    }

    public void Update()
    {
        if (fireTimer > 0) fireTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (fireTimer <= 0)
        {
            Instantiate(ammunition, muzzle.transform.position, Camera.main.transform.rotation);
            if (muzzleFlash != null)
            {
                muzzleObject = Instantiate(muzzleFlash, muzzle.transform);
                Destroy(muzzleObject, 1.5f);
            }
            fireTimer = fireRate;
        }
    }
}
