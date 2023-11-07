using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Scritable : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;
    public int weapon_atk;
    public int weapon_pride;
    public bool storeflag;
    public bool equipweaponflag;
    public Sprite weaponimage;

    private void Awake()
    {
        weapon_atk = weaponData.WeaponAtk;
        weapon_pride = weaponData.WeaponPride;
        storeflag = weaponData.StoreFlag;
        weaponimage = weaponData.WeaponImage;
    }

    private void Update()
    {
        weapon_atk = weaponData.WeaponAtk;
        weapon_pride = weaponData.WeaponPride;
        storeflag = weaponData.StoreFlag;
        weaponimage = weaponData.WeaponImage;
    }

    public void BuyWeapon()
    {
        weaponData.ChangeStorFlag();
    }

}
