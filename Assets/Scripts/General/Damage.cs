using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public void DealDamage(Collider other, float dmg)
    {
        //Deal Damage
        if(other.gameObject.TryGetComponent(out Health objectHealth))
        {
            objectHealth.health -= dmg;
        }

        //Deal knockback

    }
    
    public void SpawnExplosion(Vector3 position, Quaternion rotation, float radius)
    {
        GameObject explode = Instantiate(explosion, position, rotation);
        explode.transform.localScale = Vector3.one * radius;
        Destroy(explode, 2);
    }

    public void CreateExplosion(float radius, float dmg, bool longRangeWeapon, int falloffDistance, Vector3 spawn)
    {
        Vector3 explosionCenter = transform.position;
        SpawnExplosion(explosionCenter, Quaternion.identity, radius);
        //Instantiate some sort of explosion visual
        Collider[] collisions = Physics.OverlapSphere(explosionCenter, radius);
        foreach(Collider hit in collisions)
        {
            Vector3 hitPosition = hit.transform.position;
            float distFromCenter = Vector3.Distance(hitPosition, explosionCenter);
            //Deal Damage
            if (hit.gameObject.TryGetComponent(out Health objectHealth))
            {
                float distFromOrigin = Mathf.Abs(Vector3.Distance(spawn, hit.transform.position));
                float dmgMulti = 1;
                if (distFromOrigin > falloffDistance)
                {
                    for (int j = 0; j < distFromOrigin - falloffDistance; j++)
                    {
                        if (longRangeWeapon) dmgMulti *= 1.0f;
                        else dmgMulti *= 0.96f;
                    }
                    if(dmgMulti > 1) dmgMulti = Mathf.Min(dmgMulti, 1.5f);
                    if(dmgMulti < 1) dmgMulti = Mathf.Max(dmgMulti, 0.1f);
                }

                //Gotta distance scale
                for(int i = 0; i < distFromCenter; i++)
                {
                    dmgMulti *= 0.99f;
                }
                objectHealth.health = Mathf.Round(objectHealth.health - dmg * dmgMulti);
            }
        }
    }
}
