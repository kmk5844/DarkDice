using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreDirector : MonoBehaviour
{
    public TextMeshProUGUI CoinCount;
    public TextMeshProUGUI Item1_Count;
    public TextMeshProUGUI Item2_Count;
    public TextMeshProUGUI Item3_Count;
    public Button Item1_Button;
    public Button Item2_Button;
    public Button Item3_Button;
    public Button Weapon1_Button;
    public Button Weapon2_Button;
    public Button Weapon3_Button;

    public GameObject[] ItemObject_Data;
    Item_Scritable item1;
    Item_Scritable item2;
    Item_Scritable item3;

    public GameObject[] WeaponObject_Data;
    Weapon_Scritable weapon1;
    Weapon_Scritable weapon2;
    Weapon_Scritable weapon3;

    public GameObject playerObject;
    Player_Scritable player;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Player_Scritable>();
        item1 = ItemObject_Data[0].GetComponent<Item_Scritable>();
        item2 = ItemObject_Data[1].GetComponent<Item_Scritable>();
        item3 = ItemObject_Data[2].GetComponent<Item_Scritable>();
        weapon1 = WeaponObject_Data[0].GetComponent<Weapon_Scritable>();
        weapon2 = WeaponObject_Data[1].GetComponent<Weapon_Scritable>();
        weapon3 = WeaponObject_Data[2].GetComponent<Weapon_Scritable>();
    }

    // Update is called once per frame
    void Update()
    {
        CoinCount.text = player.coin.ToString();
        Item1_Count.text = item1.itemcount.ToString();
        Item2_Count.text = item2.itemcount.ToString();
        Item3_Count.text = item3.itemcount.ToString();

        if(player.coin < item1.pride)
        {
            Item1_Button.interactable = false;
        }
        else
        {
            Item1_Button.interactable = true;
        }

        if (player.coin < item2.pride) {
            Item2_Button.interactable = false;
        }
        else
        {
            Item2_Button.interactable = true;
        }

        if (player.coin < item3.pride)
        {
            Item3_Button.interactable = false;
        }
        else
        {
            Item3_Button.interactable = true;
        }

        if(player.coin < weapon1.weapon_pride || weapon1.storeflag)
        {
            Weapon1_Button.interactable = false;
            if (weapon1.storeflag)
            {
                Weapon1_Button.GetComponentInChildren<TextMeshProUGUI>().text = "보유 중";
            }
        }
        else
        {
            Weapon1_Button.interactable = true;
        }

        if (player.coin < weapon2.weapon_pride || weapon2.storeflag)
        {
            Weapon2_Button.interactable = false;
            if (weapon2.storeflag)
            {
                Weapon2_Button.GetComponentInChildren<TextMeshProUGUI>().text = "보유 중";
            }
        }
        else
        {
            Weapon2_Button.interactable = true;
        }

        if (player.coin < weapon3.weapon_pride || weapon3.storeflag)
        {
            Weapon3_Button.interactable = false;
            if (weapon3.storeflag)
            {
                Weapon3_Button.GetComponentInChildren<TextMeshProUGUI>().text = "보유 중";
            }
        }
        else
        {
            Weapon3_Button.interactable = true;
        }

    }

    public void OnItem1Buy()
    {
        item1.BuyItem();
        player.TestMinusCoinData(item1.pride);
    }

    public void OnItem2Buy()
    {
        item2.BuyItem();
        player.TestMinusCoinData(item2.pride);
    }

    public void OnItem3Buy()
    {
        item3.BuyItem();
        player.TestMinusCoinData(item3.pride);
    }

    public void OnWeapon1Buy()
    {
        weapon1.BuyWeapon();
        player.TestMinusCoinData(weapon1.weapon_pride);
    }

    public void OnWeapon2Buy()
    {
        weapon2.BuyWeapon();
        player.TestMinusCoinData(weapon2.weapon_pride);
    }

    public void OnWeapon3Buy()
    {
        weapon3.BuyWeapon();
        player.TestMinusCoinData(weapon3.weapon_pride);
    }

    public void OnTestCoinButton()
    {
        player.TestPlusCoinData();
    }
}
