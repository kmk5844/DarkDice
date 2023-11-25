using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDirector : MonoBehaviour
{
    public Image equipWeaponImage; //현재 장착중인 웨폰 이미지

    public GameObject PlayerObject; //플레이어 데이터를 가져온다.
    public GameObject[] WeaponObject_Data; //웨폰의 정보를 가져온다.
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
        }else
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
                Toggle_Weapon[i - 1].gameObject.SetActive(true);
            }else if(Weapon[i].storeflag == 0) //--------------------------------------
            {
                Toggle_Weapon[i - 1].gameObject.SetActive(false); //초기화 전용 코드
            }
            //-------------------------------------------------------------------------
        }

        if (Toggle_Weapon[0].isOn)
        {
            equipWeapon = Weapon[1].weaponData;
            if (Weapon[1].weaponData.StoreFlag == 0)
            {
                equipWeapon = Weapon[0].weaponData;
            }
        }
        else if (Toggle_Weapon[1].isOn)
        {
            equipWeapon = Weapon[2].weaponData;
            if (Weapon[2].weaponData.StoreFlag == 0)
            {
                equipWeapon = Weapon[0].weaponData;
            }
        }
        else if (Toggle_Weapon[2].isOn)
        {
            equipWeapon = Weapon[3].weaponData;
            if (Weapon[3].weaponData.StoreFlag == 0)
            {
                equipWeapon = Weapon[0].weaponData;
            }
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
