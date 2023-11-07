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
    Weapon_Scritable defaultWeapon;
    Weapon_Scritable Weapon1;
    Weapon_Scritable Weapon2;
    Weapon_Scritable Weapon3;
    Weapon_Scritable equipWeapon;


    private void Start()
    {
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        defaultWeapon = WeaponObject_Data[0].GetComponent<Weapon_Scritable>();
        Weapon1 = WeaponObject_Data[1].GetComponent<Weapon_Scritable>();
        Weapon2 = WeaponObject_Data[2].GetComponent<Weapon_Scritable>();
        Weapon3 = WeaponObject_Data[3].GetComponent<Weapon_Scritable>();
        equipWeapon = playerData.waepon;
        if(equipWeapon == Weapon1)
        {
            Toggle_Weapon[0].isOn = true;
        }else if(equipWeapon == Weapon2)
        {
            Toggle_Weapon[1].isOn = true;
        }else if (equipWeapon == Weapon3)
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
        if (Weapon1.storeflag)
        {
            Toggle_Weapon[0].gameObject.SetActive(true);
        }

        if (Weapon2.storeflag)
        {
            Toggle_Weapon[1].gameObject.SetActive(true);
        }

        if (Weapon3.storeflag)
        {
            Toggle_Weapon[2].gameObject.SetActive(true);
        }

        if (Toggle_Weapon[0].isOn)
        {
            equipWeapon = Weapon1;
        }
        else if (Toggle_Weapon[1].isOn)
        {
            equipWeapon = Weapon2;
        }
        else if (Toggle_Weapon[2].isOn)
        {
            equipWeapon = Weapon3;
        }
        else
        {
            equipWeapon = defaultWeapon;
        }

        playerData.ChangeWeapon(equipWeapon);
        equipWeaponImage.sprite = equipWeapon.weaponimage;
        Status_Weapon_Atk_Text.text = "+ 무기 공격력 : " + equipWeapon.weapon_atk.ToString();
    }

}
