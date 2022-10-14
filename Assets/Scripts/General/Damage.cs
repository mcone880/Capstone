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

    public void CreateExplosion(float radius, float dmg, float dmgfalloff, float explosionForce)
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
                //Gotta distance scale
                objectHealth.health -= dmg;
            }

            //Deal Knockback
            if(hit.gameObject.TryGetComponent(out Rigidbody rb))
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
