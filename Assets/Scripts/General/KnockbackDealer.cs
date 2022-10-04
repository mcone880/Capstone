using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackDealer : MonoBehaviour
{
    [SerializeField] float knockback;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 dir = other.transform.position - transform.position;
        if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(dir * (knockback / dir.magnitude));
        }
        else if (other.TryGetComponent<ImpactReceiver>(out ImpactReceiver ir))
        {
            ir.AddImpact(dir, knockback / dir.magnitude);
        }
        Destroy(gameObject);
    }
}
