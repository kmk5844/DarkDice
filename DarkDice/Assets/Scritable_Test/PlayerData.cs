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
    private ItemData[] item;
    public ItemData[] Item { get { return item; } }



    public void PlusStatus(int C_hp, int C_atk, int C_def)
    {
        hp += (float)C_hp;
        atk += C_atk;
        def += C_def;
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

    public void EquipItem_Data(ItemData itemData, int num)
    {
        item[num] = itemData;
    }
    public void DeleteItem_Data(ItemData itemData, int ButtonNum)
    {
        item[ButtonNum] = itemData;
    }
}
