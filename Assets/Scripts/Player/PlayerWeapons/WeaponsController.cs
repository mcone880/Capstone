using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] public Transform weaponTransform;

    public List<GameObject> weaponsList = new List<GameObject>();
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

    public GameObject GetWeaponFromInventory(int weaponNum)
    {
        if (CheckWeapons(weaponNum)) return weaponsList[weaponNum];
        return null;
    }

    public bool CheckWeapons(int weaponNum)
    {
        return weaponsList[weaponNum].GetComponent<Weapon>().isUsable;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && CheckWeapons(0))
        {   
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(prevWeapon.GetComponent<Weapon>().weaponNum));
            currentWeapon = GetWeaponFromInventory(0);
            EnableWeapon(weaponTransform.GetChild(0));
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && CheckWeapons(1))
        {
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(prevWeapon.GetComponent<Weapon>().weaponNum));
            currentWeapon = GetWeaponFromInventory(1);
            EnableWeapon(weaponTransform.GetChild(1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponsList.Count >= 3 && currentWeapon != null)
        {
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(prevWeapon.GetComponent<Weapon>().weaponNum));
            currentWeapon = GetWeaponFromInventory(2);
            EnableWeapon(weaponTransform.GetChild(2));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && weaponsList.Count >= 4 && currentWeapon != null)
        {
            prevWeapon = currentWeapon;
            DisableWeapon(weaponTransform.GetChild(prevWeapon.GetComponent<Weapon>().weaponNum));
            currentWeapon = GetWeaponFromInventory(3);
            EnableWeapon(weaponTransform.GetChild(3));
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
