using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] public Transform weaponTransform;

    public List<GameObject> weaponsObject = new List<GameObject>();
    public GameObject currentWeapon;
    public GameObject prevWeapon;
    public int currentWeaponIndex;

    public void EnableWeapon(Transform weapon)
    {
        weapon.gameObject.SetActive(true);
        weapon.gameObject.GetComponent<Weapon>().fireTimer = 0;
    }

    public void DisableWeapon(Transform weapon)
    {
        weapon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && weaponsObject.Count >= 1 && currentWeapon != null)
        {
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(currentWeaponIndex));
            currentWeapon = weaponsObject[0];
            currentWeaponIndex = 0;
            EnableWeapon(weaponTransform.GetChild(currentWeaponIndex));
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && weaponsObject.Count >= 2 && currentWeapon != null)
        {
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(currentWeaponIndex));
            currentWeapon = weaponsObject[1];
            currentWeaponIndex = 1;
            EnableWeapon(weaponTransform.GetChild(currentWeaponIndex));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponsObject.Count >= 3 && currentWeapon != null)
        {
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(currentWeaponIndex));
            currentWeapon = weaponsObject[3];
            currentWeaponIndex = 2;
            EnableWeapon(weaponTransform.GetChild(currentWeaponIndex));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && weaponsObject.Count >= 4 && currentWeapon != null)
        {
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(currentWeaponIndex));
            currentWeapon = weaponsObject[3];
            currentWeaponIndex = 3;
            EnableWeapon(weaponTransform.GetChild(currentWeaponIndex));
        }
        
        if(currentWeapon)
        {
            switch (currentWeapon.GetComponent<Weapon>().fireType)
            {
                case Weapon.FireType.AUTOMATIC:
                    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
                    {
                        currentWeapon.GetComponent<Weapon>().Fire();
                    }
                    break;
                case Weapon.FireType.SINGLE_SHOT:
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        currentWeapon.GetComponent<Weapon>().Fire();
                    }
                    break;
            }
        }
    }
}
