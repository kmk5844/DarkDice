using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector : MonoBehaviour
{
    public GameObject PlayerObject;
    Player_Scritable playerData;

    public TextMeshProUGUI[] Item_Count;
    public TextMeshProUGUI[] Equip_Item_Count;
    public GameObject[] ItemObject_Data;
    Item_Scritable[] itemData;
    public ItemData default_item;

    int[] Sub_Count;
    int Equip_Max_Count = 0;
    public Button[] ItemStock;
    public Button[] ItemEquip;

    void Start()
    {
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        itemData = new Item_Scritable[ItemObject_Data.Length];
        Sub_Count = new int[Equip_Item_Count.Length];

        for (int i = 0; i < Item_Count.Length; i++)
        {
            itemData[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
        }

        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            ItemStock[i].GetComponent<Image>().sprite = itemData[i].ItemImage;
        }

        for(int i = 0; i < ItemEquip.Length; i++)
        {
            ItemEquip[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

        for (int i = 0; i < Equip_Item_Count.Length; i++)
        {
            Sub_Count[i] = itemData[i].itemcount;
            Equip_Item_Count[i].text = Sub_Count[i].ToString();
        }

        for(int i = 0; i < ItemEquip.Length; i++)
        {
            playerData.DeleteItem(default_item, i);
        }

    }

    private void Update()
    {
        for (int i = 0; i < Item_Count.Length; i++)
        {
            Item_Count[i].text = "X " + itemData[i].itemcount;
        }

        for (int i = 0; i < ItemEquip.Length; i++)
        {
            ItemEquip[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

        for (int i = 0; i < Equip_Item_Count.Length; i++)
        {
            Equip_Item_Count[i].text = Sub_Count[i].ToString();
            if (Sub_Count[i] == 0 || Equip_Max_Count == 3)
            {
                ItemStock[i].interactable = false;
            }
            else
            {
                ItemStock[i].interactable = true;
            }
        }

        for (int i = 0; i < Equip_Item_Count.Length; i++)
        {
            Sub_Count[i] = itemData[i].itemcount;
            Equip_Item_Count[i].text = Sub_Count[i].ToString();
        }
    }
    public void OnEquipItem(int i) {
        playerData.EquipItem(itemData[i].itemData);
        Equip_Max_Count++;
        Sub_Count[i]--;
    }

    public void OnDeleteItem(int i)
    {
        if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "DoubleAtk")
        {
            Sub_Count[0]++;
        }
        else if(ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "Heal")
        {
            Sub_Count[1]++;
        }else if(ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "Chance")
        {
            Sub_Count[2]++;
        }else if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "DoubleDef")
        {
            Sub_Count[3]++;
        }
        if (Equip_Max_Count > 0)
        {
            Equip_Max_Count--;
        }

        playerData.DeleteItem(default_item, i);
    }
}
