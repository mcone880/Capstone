using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] int weaponNum;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WeaponsController>(out WeaponsController controller))
        {
            controller.weaponsList[weaponNum].GetComponent<Weapon>().isUsable = true;
            if (controller.currentWeapon == null)
            {
                controller.currentWeapon = controller.weaponsList[weaponNum];
                controller.EnableWeapon(controller.weaponsList[weaponNum].transform);
            } else
            {
                controller.DisableWeapon(controller.weaponsList[weaponNum].transform);
            }
            Destroy(gameObject);
        }
    }
}
