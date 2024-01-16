using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Weapon_Scritable : MonoBehaviour
{
    [SerializeField]
    public D_WeaponData weaponData;
    public int weapon_def;
    public int weapon_pride;
    public int storeflag;
    public Sprite weaponimage;

    private void Awake()
    {
        weapon_def = weaponData.WeaponDef;
        weapon_pride = weaponData.WeaponPride;
        storeflag = weaponData.StoreFlag;
        weaponimage = weaponData.WeaponImage;
    }

    private void Update()
    {
        weapon_def = weaponData.WeaponDef;
        weapon_pride = weaponData.WeaponPride;
        storeflag = weaponData.StoreFlag;
        weaponimage = weaponData.WeaponImage;
    }

    public void BuyWeapon_Weapon()
    {
        weaponData.ChangeStorFlag();
    }
}
