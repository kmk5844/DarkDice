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
    private Weapon_Scritable weapon;
    public Weapon_Scritable Weapon { get { return weapon; } }
    [SerializeField]
    private int def;
    public int Def { get {  return def; } }
    [SerializeField]
    private int coin;
    public int Coin { get { return coin; } }

    public void PlusHP(int C_hp)
    {
        hp += (float)C_hp;
    }

    public void PlusATK(int C_atk)
    {
        atk += C_atk;
    }

    public void ChangeWeaponATK(Weapon_Scritable C_WeaponAtk)
    {
        weapon = C_WeaponAtk;
    }

    public void PlusDEF(int C_def)
    {
        def += C_def;
    }

    public void testPlusCoin()
    {
        coin += 100;
    }

    public void testMinusCoin(int buyCoin)
    {
        coin -= buyCoin;
    }
}
