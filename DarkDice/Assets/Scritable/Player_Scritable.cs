using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scritable : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    public float hp;
    public int atk;
    public WeaponData weapon;
    public int def;
    public int coin;
    public ItemData[] item;

    bool StatusFlag = false;

    private void Awake()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        weapon = playerData.Weapon;
        def = playerData.Def;
        coin = playerData.Coin;
        item = playerData.Item;
    }

    private void Update()
    {
        if (StatusFlag)
        {
            hp = playerData.Hp;
            atk = playerData.Atk;
            def = playerData.Def;
            StatusFlag = false;
        }
        weapon = playerData.Weapon;
        coin = playerData.Coin;
        item = playerData.Item;    
    }

    public void ChangePlayerData(int C_atk, int C_def)
    {
        playerData.PlusStatus(C_atk, C_def);
        StatusFlag = true;
    }

    public void ChangeWeapon(WeaponData C_Weapon)
    {
        playerData.ChangeWeaponATK(C_Weapon);
    }

    public void TestPlusCoinData()
    {
        playerData.testPlusCoin();
    }

    public void TestMinusCoinData(int buyCoin)
    {
        playerData.testMinusCoin(buyCoin);
    }

    public void EquipItem(ItemData clickitem)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].name == "Item_Default") //스크립터블 아이템 "파일" 이름여야함
            {
                playerData.EquipItem_Data(clickitem, i);
                break;
            }
        }
    }

    public void DeleteItem(ItemData default_item, int ButtonNum)
    {
        playerData.DeleteItem_Data(default_item, ButtonNum);
    }
}
