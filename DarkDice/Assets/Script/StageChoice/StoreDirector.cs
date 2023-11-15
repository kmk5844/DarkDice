using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StoreDirector : MonoBehaviour
{
    public TextMeshProUGUI CoinCount;

    public GameObject playerObject;
    Player_Scritable player;

    public GameObject[] ItemObject_Data;
    Item_Scritable[] item;

    public GameObject[] WeaponObject_Data;
    Weapon_Scritable[] weapon;

    public TextMeshProUGUI[] Item_Count;
    public TextMeshProUGUI[] Item_Pride;
    public TextMeshProUGUI[] Weapon_Pride;

    public Button[] Item_Button;
    public Button[] Weapon_Button;

    void Start()
    {
        player = playerObject.GetComponent<Player_Scritable>();
        item = new Item_Scritable[ItemObject_Data.Length];
        weapon = new Weapon_Scritable[WeaponObject_Data.Length];
        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
            Item_Pride[i].text = item[i].pride + "G";
        }

        for (int i = 0; i <  WeaponObject_Data.Length; i++)
        {
            weapon[i] = WeaponObject_Data[i].GetComponent<Weapon_Scritable>();
            Weapon_Pride[i].text = weapon[i].weapon_pride + "G";
        }

    }

    void Update()
    {
        CoinCount.text = player.coin.ToString();
        for (int i = 0; i < item.Length; i++)
        {
            Item_Count[i].text = item[i].itemcount.ToString();
            if (player.coin < item[i].pride)
            {
                Item_Button[i].interactable = false;
            }
            else
            {
                Item_Button[i].interactable = true;
            }
        }

        for(int i = 0; i < weapon.Length; i++)
        {
            if (player.coin < weapon[i].weapon_pride || weapon[i].storeflag == 1)
            {
                Weapon_Button[i].interactable = false;
                if (weapon[i].storeflag == 1)
                {
                    Weapon_Button[i].GetComponentInChildren<TextMeshProUGUI>().text = "���� ��";
                }
            }
            else
            {
                Weapon_Button[i].interactable = true;
            }
        }
    }

    public void OnItemBuy(int i)
    {
        item[i].BuyItem();
        player.TestMinusCoinData(item[i].pride);
    }

    public void OnWeaponBuy(int i)
    {
        weapon[i].BuyWeapon();
        player.TestMinusCoinData(weapon[i].weapon_pride);
    }

    public void OnTestCoinButton()
    {
        player.TestPlusCoinData();
    }
}
