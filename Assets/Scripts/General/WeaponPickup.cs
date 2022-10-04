using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WeaponsController>(out WeaponsController controller))
        {
            GameObject actualWeapon = Instantiate(weapon, controller.weaponTransform);
            controller.weaponsObject.Add(weapon);
            if (controller.currentWeapon == null)
            {
                controller.currentWeapon = actualWeapon;
                controller.currentWeaponIndex = 0;
                controller.EnableWeapon(controller.weaponTransform.GetChild(controller.currentWeaponIndex));
            } else
            {
                controller.DisableWeapon(controller.weaponTransform.GetChild(controller.currentWeaponIndex + 1));
            }
            Destroy(gameObject);
        }
    }
}
