using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected GameObject ammunition;
    protected float fireRate;
    private GameObject muzzleObject;
    public float fireTimer = 0;
    public FireType fireType;

    public enum FireType
    {
        AUTOMATIC,
        SINGLE_SHOT
    }

    public void Update()
    {
        if (fireTimer > 0) fireTimer -= Time.deltaTime;
        if (muzzleObject != null)
        {
            muzzleObject.transform.position = muzzle.transform.position;
            muzzleObject.transform.rotation = muzzle.transform.rotation;
        }
    }

    public void Fire()
    {
        if (fireTimer <= 0)
        {
            Instantiate(ammunition, Camera.main.transform.position + Camera.main.transform.forward * 0.2f, Camera.main.transform.rotation);
            if (muzzleFlash != null)
            {
                muzzleObject = Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
                Destroy(muzzleObject, 1.5f);
            }
            fireTimer = fireRate;
        }
    }
}
