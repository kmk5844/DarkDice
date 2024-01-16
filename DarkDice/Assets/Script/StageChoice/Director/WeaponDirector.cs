using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDirector : MonoBehaviour
{
    public GameObject PlayerObject; //플레이어 데이터를 가져온다.
    Player_Scritable playerData;
    
    [SerializeField]
    WeaponData equipWeapon_A;
    [SerializeField]
    D_WeaponData equipWeapon_D;

    [Header("공격 관련 데이터")]
    public GameObject[] Atk_WeaponObject_Data; //공격 무기의 정보를 가져온다.
    Weapon_Scritable[] Atk_Weapon;
    public Toggle[] Toggle_AtkWeapon;

    [Header("방어 관련 데이터")]
    public GameObject[] Def_WeaponObject_Data; //방어 무기의 정보를 가져온다.
    D_Weapon_Scritable[] Def_Weapon;
    public Toggle[] Toggle_DefWeapon;

    [Space(10)]
    public TextMeshProUGUI Status_Weapon_Atk_Text;
    public TextMeshProUGUI Status_Weapon_Def_Text;
    private void Start()
    {
        int weaponLoadData = PlayerPrefs.GetInt("Player_Weapon_A");
        int D_weaponLoadData = PlayerPrefs.GetInt("Player_Weapon_D");
        Atk_Weapon = new Weapon_Scritable[Atk_WeaponObject_Data.Length];
        Def_Weapon = new D_Weapon_Scritable[Def_WeaponObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>();

        for(int i = 0; i < Atk_WeaponObject_Data.Length; i++)
        {
            Atk_Weapon[i] = Atk_WeaponObject_Data[i].GetComponent<Weapon_Scritable>();
            Def_Weapon[i] = Def_WeaponObject_Data[i].GetComponent<D_Weapon_Scritable>();
        } //등록하는 단계

        equipWeapon_A = playerData.weapon;
        equipWeapon_D = playerData.d_weapon;

        equipWeapon_A = Atk_Weapon[weaponLoadData].weaponData;
        equipWeapon_D = Def_Weapon[D_weaponLoadData].weaponData;
        Toggle_AtkWeapon[weaponLoadData].isOn = true;
        Toggle_DefWeapon[D_weaponLoadData].isOn = true;
    }

    private void Update()
    {
        for (int i = 1; i < Atk_Weapon.Length; i++)
        {
            if (Atk_Weapon[i].storeflag == 1)
            {
                Toggle_AtkWeapon[i].interactable = true;
            }
            else if (Atk_Weapon[i].storeflag == 0) //--------------------------------------
            {
                Toggle_AtkWeapon[i].isOn = false;
                Toggle_AtkWeapon[i].interactable = false; //개발자 초기화 전용 코드
            }
            //-------------------------------------------------------------------------

            if(Def_Weapon[i].storeflag == 1)
            {
                Toggle_DefWeapon[i].interactable = true;
            }else if(Def_Weapon[i].storeflag == 0)
            {
                Toggle_DefWeapon[i].isOn = false;
                Toggle_DefWeapon[i].interactable = false;
            }
        }

        if (Toggle_AtkWeapon[0].isOn)
        {
            equipWeapon_A = Atk_Weapon[0].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_A", 0);
            if (Atk_Weapon[0].weaponData.StoreFlag == 0)
            {
                equipWeapon_A = Atk_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_A", 0);
            }
        }
        else if (Toggle_AtkWeapon[1].isOn)
        {
            equipWeapon_A = Atk_Weapon[1].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_A", 1);
            if (Atk_Weapon[1].weaponData.StoreFlag == 0)
            {
                equipWeapon_A = Atk_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_A", 0);
            }
        }
        else if (Toggle_AtkWeapon[2].isOn)
        {
            equipWeapon_A = Atk_Weapon[2].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_A", 2);
            if (Atk_Weapon[2].weaponData.StoreFlag == 0)
            {
                equipWeapon_A = Atk_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_A", 0);
            }
        }
        else if (Toggle_AtkWeapon[3].isOn)
        {
            equipWeapon_A = Atk_Weapon[3].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_A", 3);
            if (Atk_Weapon[3].weaponData.StoreFlag == 0)
            {
                equipWeapon_A = Atk_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_A", 0);
            }
        }
        else if (Toggle_AtkWeapon[4].isOn)
        {
            equipWeapon_A = Atk_Weapon[4].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_A", 4);
            if (Atk_Weapon[4].weaponData.StoreFlag == 0)
            {
                equipWeapon_A = Atk_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_A", 0);
            }
        }
        else
        {
            Toggle_AtkWeapon[0].isOn = true;
            equipWeapon_A = Atk_Weapon[0].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_A", 0);
        }

        if (Toggle_DefWeapon[0].isOn)
        {
            equipWeapon_D = Def_Weapon[0].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_D", 0);
            if (Def_Weapon[0].weaponData.StoreFlag == 0)
            {
                equipWeapon_D = Def_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_D", 0);
            }
        }
        else if (Toggle_DefWeapon[1].isOn)
        {
            equipWeapon_D = Def_Weapon[1].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_D", 1);
            if (Def_Weapon[1].weaponData.StoreFlag == 0)
            {
                equipWeapon_D = Def_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_D", 0);
            }
        }
        else if (Toggle_DefWeapon[2].isOn)
        {
            equipWeapon_D = Def_Weapon[2].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_D", 2);
            if (Def_Weapon[2].weaponData.StoreFlag == 0)
            {
                equipWeapon_D = Def_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_D", 0);
            }
        }
        else if (Toggle_DefWeapon[3].isOn)
        {
            equipWeapon_D = Def_Weapon[3].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_D", 3);
            if (Def_Weapon[3].weaponData.StoreFlag == 0)
            {
                equipWeapon_D = Def_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_D", 0);
            }
        }
        else if (Toggle_DefWeapon[4].isOn)
        {
            equipWeapon_D = Def_Weapon[4].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_D", 4);
            if (Def_Weapon[4].weaponData.StoreFlag == 0)
            {
                equipWeapon_D = Def_Weapon[0].weaponData;
                PlayerPrefs.SetInt("Player_Weapon_D", 0);
            }
        }
        else
        {
            Toggle_DefWeapon[0].isOn = true;
            equipWeapon_D = Def_Weapon[0].weaponData;
            PlayerPrefs.SetInt("Player_Weapon_D", 0);
        }

        playerData.ChangeWeapon_Player(equipWeapon_A);
        playerData.ChangeShield_Player(equipWeapon_D);
        Status_Weapon_Atk_Text.text = "+ 무기 공격력 : " + equipWeapon_A.WeaponAtk.ToString();
        Status_Weapon_Def_Text.text = "+ 무기 방어력 : " + equipWeapon_D.WeaponDef.ToString();
    }
}
