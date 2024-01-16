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
    public D_WeaponData d_weapon;
    public int coin;
    public int status;
    public ItemData[] item;

    public bool StatusFlag = false;

    private void Awake()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        weapon = playerData.Weapon;
        def = playerData.Def;
        d_weapon = playerData.D_Weapon;
        coin = playerData.Coin;
        item = playerData.Item;
        status = playerData.Status;
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
        status = playerData.Status;
        weapon = playerData.Weapon;
        d_weapon = playerData.D_Weapon;
        coin = playerData.Coin;
        item = playerData.Item;
    }

    public void PlusStatus_Player(int C_atk, int C_def, int num)
    {
        playerData.PlusStatus(C_atk, C_def, num);
        StatusFlag = true;
    }

    public void ChangeWeapon_Player(WeaponData C_Weapon)
    {
        playerData.ChangeWeapon(C_Weapon);
    }

    public void ChangeShield_Player(D_WeaponData C_D_Weapon)
    {
        playerData.ChangeD_Weapon(C_D_Weapon);
    }

    public void BuyCoin_Player(int buyCoin)
    {
        playerData.BuyCoin(buyCoin);
    }

    public void RewardCoin_Player(int coin)
    {
        playerData.RewardCoin(coin);
    }

    public void RewardStatus_Player(int num)
    {
        playerData.RewardStatus(num);
    }

    public void RewardHp_Player(int hp)
    {
        playerData.RewardHp(hp);
    }

    public void ApplyStatus_Player(int stat)
    {
        playerData.ApplyStatus(stat);
    }

    public void EquipItem_Player(ItemData clickitem)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].name == "Item_Default") //스크립터블 아이템 "파일" 이름여야함
            {
                playerData.EquipItem(clickitem, i);
                break;
            }
        }
    }
    public void DeleteItem_Player(ItemData default_item, int ButtonNum)
    {
        playerData.DeleteItem(default_item, ButtonNum);
    }


    public void ItemUse_Init_Player()
    {
        playerData.ItemUse_Init();
        StatusFlag = true;
    }

    // 개발자 전용
    public void TestPlusCoinData()
    {
        playerData.testPlusCoin();
    }

    public void playerInit()
    {
        playerData.Init();
    }
}
