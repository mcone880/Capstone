using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected GameObject ammunition;
    [SerializeField] protected AudioSource gunshot;
    [SerializeField] protected bool IsHitscan = false;
    [SerializeField] bool longRangeWeapon;
    [SerializeField] int falloffDistance;
    [SerializeField] bool isLaser = false;
    [SerializeField] public string AnimatorName;
    [SerializeField] public float weaponDrawSpeed;
    //[SerializeField] public int maxAmmo;
    //[SerializeField] public int ammo;
    private GameObject muzzleObject;

    public int weaponNum;
    public FireType fireType;

    protected GameObject Player;
    protected List<Vector2> deviationList;
    protected float fireRate;
    //How many rays will be cast
    protected int rayNum;
    //The amount of damage each raycast will deal
    protected float dmgPerRay;
    //Distance rays will go
    protected const int RAY_DIST = 9999;

    protected LineRenderer line;
    protected float LaserDur;
    
    [HideInInspector]public bool isUsable = false;
    [HideInInspector]public float fireTimer = 0;


    public enum FireType
    {
        AUTOMATIC,
        SINGLE_SHOT
    }

    public void Update()
    {
        if (fireTimer > 0) fireTimer -= Time.deltaTime;
    }

    public void Fire(Animator animator)
    {
        if (fireTimer <= 0)
        {
            animator.SetTrigger(AnimatorName + "Fired");
            gunshot.Play();
            if (IsHitscan)
            {
                for(int i = 0; i < rayNum; i++)
                {
                    if (isLaser) line.SetPosition(0, muzzle.position);
                    Vector3 baseDir = Camera.main.transform.forward;
                    baseDir = (baseDir * 10 + Camera.main.transform.right * deviationList[i].x + Camera.main.transform.up * deviationList[i].y).normalized;
                    if(Physics.Raycast(Camera.main.transform.position, baseDir, out RaycastHit hit, RAY_DIST, 11))
                    {
                        if(isLaser)line.SetPosition(1, hit.point);
                        print(hit.collider.gameObject.name);
                        if (hit.collider.gameObject.TryGetComponent(out Health objectHealth))
                        {
                            float distFromPlayer = Mathf.Abs(Vector3.Distance(Camera.main.transform.position, hit.collider.gameObject.transform.position));
                            float dmgMulti = 1;
                            if (distFromPlayer > falloffDistance)
                            {
                                for (int j = 0; j < distFromPlayer - falloffDistance; j++)
                                {
                                    if (longRangeWeapon) dmgMulti *= 1.04f;
                                    else dmgMulti *= 0.96f;
                                }
                            }
                            objectHealth.health = Mathf.Round(objectHealth.health - dmgPerRay * dmgMulti);
                        }
                    }
                    else
                    {
                        if(isLaser)line.SetPosition(1, baseDir + (Camera.main.transform.forward * RAY_DIST));
                    }
                    Debug.DrawRay(Camera.main.transform.position, baseDir * 100, Color.cyan, 10);
                }
                if(isLaser)StartCoroutine(ShootLaser());
            }
            else
            {
                Instantiate(ammunition, muzzle.transform.position, Camera.main.transform.rotation).GetComponent<Projectile>().spawnPoint = muzzle.transform.position;
            }
            if (muzzleFlash != null)
            {
                muzzleObject = Instantiate(muzzleFlash, muzzle.transform);
                Destroy(muzzleObject, 1.5f);
            }
            fireTimer = fireRate;
            //ammo--;
        }
    }


    IEnumerator ShootLaser()
    {
        line.enabled = true;
        yield return new WaitForSeconds(LaserDur);
        line.enabled = false;
    }
}
