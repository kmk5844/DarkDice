using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector_Stage : MonoBehaviour
{
    public GameObject PlayerObject;
    Player_Scritable playerData;

    public TextMeshProUGUI[] Inventory_Item_Count;
    public TextMeshProUGUI[] Equip_Item_Count;
    public GameObject[] ItemObject;
    Item_Scritable[] itemData_Object;
    public ItemData default_item;

    int[] Sub_Count;
    int Equip_Max_Count;
    public Button[] ItemStock;
    public Button[] ItemEquip;

    void Start()
    {
        itemData_Object = new Item_Scritable[ItemObject.Length];
        Equip_Max_Count = 0;
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        Sub_Count = new int[Equip_Item_Count.Length];

        for (int i = 0; i < Inventory_Item_Count.Length; i++)
        {
            itemData_Object[i] = ItemObject[i].GetComponent<Item_Scritable>();
        }
        
        for (int i = 0; i < ItemEquip.Length; i++)
        {
            ItemEquip[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

        for (int i = 0; i < Equip_Item_Count.Length; i++)
        {
            Sub_Count[i] = itemData_Object[i].itemcount;
            Equip_Item_Count[i].text = Sub_Count[i].ToString();
        }

        for (int i = 0; i < ItemObject.Length; i++)
        {
            ItemStock[i].GetComponent<Image>().sprite = itemData_Object[i].ItemImage;
        }

        for (int i = 0; i < ItemEquip.Length; i++)
        {
            playerData.DeleteItem(default_item, i); // 씬으로 돌아갈때 필요, 남아있으면 안됨.
        }

    }

     private void Update()
    {
        for (int i = 0; i < Inventory_Item_Count.Length; i++)
        {
            Inventory_Item_Count[i].text = itemData_Object[i].itemcount.ToString();
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
            Equip_Item_Count[i].text = Sub_Count[i].ToString();
        }
    }

    public void OnUpdate()
    {
        for(int i = 0; i < Inventory_Item_Count.Length; i++)
        {
            Sub_Count[i] = itemData_Object[i].itemcount;
        }
    }
    public void OnEquipItem(int i)
    {
        playerData.EquipItem(itemData_Object[i].itemData);
        Equip_Max_Count++;
        Sub_Count[i]--;
    }

    public void OnDeleteItem(int i)
    {
        if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "DoubleAtk")
        {
            Sub_Count[0]++;
        }
        else if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "Heal")
        {
            Sub_Count[1]++;
        }
        else if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "Chance")
        {
            Sub_Count[2]++;
        }
        else if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "DoubleDef")
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
