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

    private void Awake()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        weapon = playerData.Weapon;
        def = playerData.Def;
        coin = playerData.Coin;
    }

    private void Update()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        weapon = playerData.Weapon;
        def = playerData.Def;
        coin = playerData.Coin;
    }

    public void ChangePlayerData(int C_hp, int C_atk, int C_def)
    {
        playerData.PlusStatus(C_hp, C_atk, C_def);
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
}
