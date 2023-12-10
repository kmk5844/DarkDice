using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDirector : MonoBehaviour
{
    public GameObject PlayerObject; //�÷��̾� �����͸� �����´�.
    Player_Scritable playerData;
    
    WeaponData equipWeapon;
    Weapon_Scritable[] Weapon;

    public Image equipWeaponImage; //���� �������� ���� �̹���

    public GameObject[] WeaponObject_Data; //������ ������ �����´�.
    public Toggle[] Toggle_Weapon;
    public TextMeshProUGUI Status_Weapon_Atk_Text;


    private void Start()
    {
        int weaponLoadData = PlayerPrefs.GetInt("Player_Weapon");
        Weapon = new Weapon_Scritable[WeaponObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>();

        for(int i = 0; i < WeaponObject_Data.Length; i++)
        {
            Weapon[i] = WeaponObject_Data[i].GetComponent<Weapon_Scritable>();
        }

        equipWeapon = playerData.weapon;

        if (weaponLoadData == 1)
        {
            equipWeapon = Weapon[1].weaponData;
            Toggle_Weapon[0].isOn = true;
        }
        else if(weaponLoadData == 2)
        {
            equipWeapon = Weapon[2].weaponData;
            Toggle_Weapon[1].isOn = true;
        }else if (weaponLoadData == 3)
        {
            equipWeapon = Weapon[3].weaponData;
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
                Toggle_Weapon[i - 1].isOn = false;
                Toggle_Weapon[i - 1].gameObject.SetActive(false); //������ �ʱ�ȭ ���� �ڵ�
            }
            //-------------------------------------------------------------------------
        }

        if (Toggle_Weapon[0].isOn)
        {
            equipWeapon = Weapon[1].weaponData;
            PlayerPrefs.SetInt("Player_Weapon", 1);
            if (Weapon[1].weaponData.StoreFlag == 0)
            {
                equipWeapon = Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon", 0);
            }
        }
        else if (Toggle_Weapon[1].isOn)
        {
            equipWeapon = Weapon[2].weaponData;
            PlayerPrefs.SetInt("Player_Weapon", 2);
            if (Weapon[2].weaponData.StoreFlag == 0)
            {
                equipWeapon = Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon", 0);
            }
        }
        else if (Toggle_Weapon[2].isOn)
        {
            equipWeapon = Weapon[3].weaponData;
            PlayerPrefs.SetInt("Player_Weapon", 3);
            if (Weapon[3].weaponData.StoreFlag == 0)
            {
                equipWeapon = Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon", 0);
            }
        }
        else
        {
            equipWeapon = Weapon[0].weaponData;
            PlayerPrefs.SetInt("Player_Weapon", 0);
        }

        playerData.ChangeWeapon_Player(equipWeapon);
        equipWeaponImage.sprite = equipWeapon.WeaponImage;
        Status_Weapon_Atk_Text.text = "+ ���� ���ݷ� : " + equipWeapon.WeaponAtk.ToString();
    }
}
