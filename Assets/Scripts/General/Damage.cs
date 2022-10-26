using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public void DealDamage(Collider other, float dmg, float knockback)
    {
        //Deal Damage
        if(other.gameObject.TryGetComponent(out Health objectHealth))
        {
            objectHealth.health -= dmg;
            print("Hit Health");
        }

        //Deal knockback

    }
    
    public void SpawnExplosion(Vector3 position, Quaternion rotation, float radius)
    {
        GameObject explode = Instantiate(explosion, position, rotation);
        explode.transform.localScale = Vector3.one * radius;
        Destroy(explode, 2);
    }

    public void CreateExplosion(float radius, float dmg, float explosionForce, bool longRangeWeapon, int falloffDistance, Vector3 spawn)
    {
        Vector3 explosionCenter = transform.position;
        SpawnExplosion(explosionCenter, Quaternion.identity, radius);
        //Instantiate some sort of explosion visual
        Collider[] collisions = Physics.OverlapSphere(explosionCenter, radius);
        foreach(Collider hit in collisions)
        {
            Vector3 hitPosition = hit.transform.position;
            Vector3 distFromCenter = hitPosition - explosionCenter;
            //Deal Damage
            if (hit.gameObject.TryGetComponent(out Health objectHealth))
            {
                float distFromOrigin = Mathf.Abs(Vector3.Distance(spawn, hit.transform.position));
                float dmgMulti = 1;
                if (distFromOrigin > falloffDistance)
                {
                    for (int j = 0; j < distFromOrigin - falloffDistance; j++)
                    {
                        if (longRangeWeapon) dmgMulti *= 1.04f;
                        else dmgMulti *= 0.96f;
                    }
                    if(dmgMulti > 1) dmgMulti = Mathf.Min(dmgMulti, 1.5f);
                    if(dmgMulti < 1) dmgMulti = Mathf.Max(dmgMulti, 0.1f);
                }
                //Gotta distance scale
                print(dmgMulti);
                objectHealth.health = Mathf.Round(objectHealth.health - dmg * dmgMulti);
            }

            //Deal Knockback
            if (hit.gameObject.TryGetComponent(out Rigidbody rb))
            {
                //might already Lerp? Gotta look into it
                print(rb.gameObject.name);
                rb.AddExplosionForce(explosionForce, explosionCenter, radius);
            }
            else if(hit.gameObject.TryGetComponent(out ImpactReceiver ir))
            {
                //gotta Lerp this shit
                ir.AddImpact(hitPosition - explosionCenter, explosionForce);
            }
        }
    }
}
