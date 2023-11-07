using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scritable : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    public float hp;
    public int atk;
    public Weapon_Scritable waepon;
    public int def;
    public int coin;

    private void Awake()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        waepon = playerData.Weapon;
        def = playerData.Def;
        coin = playerData.Coin;
    }

    private void Update()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        waepon = playerData.Weapon;
        def = playerData.Def;
        coin = playerData.Coin;
    }

    public void ChangePlayerData(int C_hp, int C_atk, int C_def)
    {
        playerData.PlusHP(C_hp);
        playerData.PlusATK(C_atk);
        playerData.PlusDEF(C_def);
    }

    public void ChangeWeapon(Weapon_Scritable C_WATK)
    {
        playerData.ChangeWeaponATK(C_WATK);

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
