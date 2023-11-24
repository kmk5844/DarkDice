using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "PlayerData", order = 1)]

public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string playerName;
    public string PayerName { get { return playerName; } }
    [SerializeField]
    private float hp;
    public float Hp { get { return hp; } }
    [SerializeField]
    private int atk;
    public int Atk { get { return atk; } }
    [SerializeField]
    private WeaponData weapon;
    public WeaponData Weapon { get { return weapon; } }
    [SerializeField]
    private int def;
    public int Def { get {  return def; } }
    [SerializeField]
    private int coin;
    public int Coin { get { return coin; } }
    [SerializeField]
    private int status;
    public int Status {  get { return status; } }
    [SerializeField]
    private ItemData[] item;
    public ItemData[] Item { get { return item; } }



    public void PlusStatus(int C_atk, int C_def, int num)
    {
        atk += C_atk;
        def += C_def;
        status -= num;
    }

    public void ChangeWeaponATK(WeaponData C_Weapon)
    {
        weapon = C_Weapon;
    }

    public void testPlusCoin()
    {
        coin += 100;
    }

    public void testMinusCoin(int buyCoin)
    {
        coin -= buyCoin;
    }

    public void RewardCoin(int coinNum)
    {
        coin += coinNum;
    }

    public void RewardStatus(int num)
    {
        status += num;
    }

    public void RewardHp(int hpNum)
    {
        hp += hpNum;
    }

    public void EquipItem_Data(ItemData itemData, int num)
    {
        item[num] = itemData;
    }
    public void DeleteItem_Data(ItemData itemData, int ButtonNum)
    {
        item[ButtonNum] = itemData;
    }

    public void Init()
    {
        hp = 2;
        atk = 10;
        def = 10;
        coin = 0;
        status = 0;
    }
}
