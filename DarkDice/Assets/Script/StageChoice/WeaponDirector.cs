using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDirector : MonoBehaviour
{
    public Image equipWeaponImage;

    public GameObject PlayerObject;
    public GameObject[] WeaponObject_Data;
    public Toggle[] Toggle_Weapon;
    public TextMeshProUGUI Status_Weapon_Atk_Text;

    Player_Scritable playerData;
    Weapon_Scritable[] Weapon;
    WeaponData equipWeapon;

    private void Start()
    {
        Weapon = new Weapon_Scritable[WeaponObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        for(int i = 0; i < WeaponObject_Data.Length; i++)
        {
            Weapon[i] = WeaponObject_Data[i].GetComponent<Weapon_Scritable>();
        }
        equipWeapon = playerData.weapon;

        if (equipWeapon == Weapon[1].weaponData)
        {
            Toggle_Weapon[0].isOn = true;
        }else if(equipWeapon == Weapon[2].weaponData)
        {
            Toggle_Weapon[1].isOn = true;
        }else if (equipWeapon == Weapon[3].weaponData)
        {
            Toggle_Weapon[2].isOn = true;
        }
        else
        {
            Toggle_Weapon[0].isOn = false;
            Toggle_Weapon[1].isOn = false;
            Toggle_Weapon[2].isOn = false;
        }
    }

    private void Update()
    { 
        for(int i = 1; i < Weapon.Length; i++)
        {
            if (Weapon[i].storeflag == 1)
            {
                Toggle_Weapon[i-1].gameObject.SetActive(true);
            }
        }

        if (Toggle_Weapon[0].isOn)
        {
            equipWeapon = Weapon[1].weaponData;
        }
        else if (Toggle_Weapon[1].isOn)
        {
            equipWeapon = Weapon[2].weaponData;
        }
        else if (Toggle_Weapon[2].isOn)
        {
            equipWeapon = Weapon[3].weaponData;
        }
        else
        {
            equipWeapon = Weapon[0].weaponData;
        }

        playerData.ChangeWeapon(equipWeapon);
        equipWeaponImage.sprite = equipWeapon.WeaponImage;
        Status_Weapon_Atk_Text.text = "+ 무기 공격력 : " + equipWeapon.WeaponAtk.ToString();
    }

}
