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

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Player_Hp"))
        {
            PlayerPrefs.SetInt("Player_Hp", 2); // 기본설정
        }
        if (!PlayerPrefs.HasKey("Player_Atk"))
        {
            PlayerPrefs.SetInt("Player_Atk", 10); // 기본 설정
        }
        if (!PlayerPrefs.HasKey("Player_Def"))
        {
            PlayerPrefs.SetInt("Player_Def", 10); // 기본 설정
        }
        if (!PlayerPrefs.HasKey("Player_Weapon"))
        {
            PlayerPrefs.SetInt("Player_Weapon", 0); // 기본 설정
        }
        if (!PlayerPrefs.HasKey("Player_Coin"))
        {
            PlayerPrefs.SetInt("Player_Coin", 0); // 기본 설정
        }
        if (!PlayerPrefs.HasKey("Player_Status"))
        {
            PlayerPrefs.SetInt("Player_Status", 0); // 기본 설정
        }
        hp = PlayerPrefs.GetInt("Player_Hp");
        atk = PlayerPrefs.GetInt("Player_Atk");
        def = PlayerPrefs.GetInt("Player_Def");
        coin = PlayerPrefs.GetInt("Player_Coin");
        status = PlayerPrefs.GetInt("Player_Status");
    }

    public void PlusStatus(int C_atk, int C_def, int num)
    {
        atk += C_atk;
        def += C_def;
        status -= num;
        PlayerPrefs.SetInt("Player_Atk", atk);
        PlayerPrefs.SetInt("Player_Def", def);
        PlayerPrefs.SetInt("Player_Status", status);
    }

    public void ChangeWeapon(WeaponData C_Weapon)
    {
        weapon = C_Weapon;
    }

    public void BuyCoin(int buyCoin)
    {
        coin -= buyCoin;
        PlayerPrefs.SetInt("Player_Coin", coin);
    }

    public void RewardCoin(int coinNum)
    {
        coin += coinNum;
        PlayerPrefs.SetInt("Player_Coin", coin);
    }

    public void RewardStatus(int num)
    {
        status += num;
        PlayerPrefs.SetInt("Player_Status", status);
    }

    public void RewardHp(int hpNum)
    {
        hp += hpNum;
        PlayerPrefs.SetInt("Player_Hp", (int)hp);
    }

    public void ApplyStatus(int num)
    {
        status = num;
        PlayerPrefs.SetInt("Player_Status", status);
    }

    public void EquipItem(ItemData itemData, int num)
    {
        item[num] = itemData;
    }

    public void DeleteItem(ItemData itemData, int ButtonNum)
    {
        item[ButtonNum] = itemData;
    }

    

    public void ItemUse_Init() //플레이어 전용 초기화
    {
        status = (atk - 9) + (def - 9) + status;
        atk = 9;
        def = 9;
        PlayerPrefs.SetInt("Player_Status", status);
        PlayerPrefs.SetInt("Player_Atk", atk);
        PlayerPrefs.SetInt("Player_Def", def);
    }

    //개발자 전용
    public void testPlusCoin()
    {
        coin += 100;
        PlayerPrefs.SetInt("Player_Coin", coin);
    }

    public void Init() //개발자 전용 초기화
    {
        hp = 2;
        atk = 8;
        def = 8;
        coin = 0;
        status = 0;
        PlayerPrefs.SetInt("Player_Hp", (int)hp);
        PlayerPrefs.SetInt("Player_Atk", atk);
        PlayerPrefs.SetInt("Player_Def", def);
        PlayerPrefs.SetInt("Player_Coin", coin);
        PlayerPrefs.SetInt("Player_Status", status);
    }
}
