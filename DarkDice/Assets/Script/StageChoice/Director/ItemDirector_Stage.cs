using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector_Stage : MonoBehaviour
{
    public GameObject PlayerObject;
    Player_Scritable playerData;

    public TextMeshProUGUI[] Inventory_Item_Count; //���� ������ �ִ� ������ ����
    public TextMeshProUGUI[] Equip_Item_Count; //���� ������ ǥ�� ����
    public ItemData default_item;
    public GameObject[] ItemObject_Data;
    Item_Scritable[] itemData;

    int[] Equip_Sub_Count;
    int Equip_Max_Count;
    public Button[] ItemStock; // ���� �����ۿ��� �κ��丮���� Ŭ���� ���
    public Button[] ItemEquip; // ������, ���� �Ǵ� ��ư

    void Start()
    {
        Equip_Max_Count = 0;
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        itemData = new Item_Scritable[ItemObject_Data.Length];
        Equip_Sub_Count = new int[Equip_Item_Count.Length];

        for (int i = 0; i < Inventory_Item_Count.Length; i++)
        {
            itemData[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
        }

        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            ItemStock[i].GetComponent<Image>().sprite = itemData[i].ItemImage;
        }

        for (int i = 0; i < ItemEquip.Length; i++)
        {
            ItemEquip[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
            playerData.DeleteItem(default_item, i); // ������ ���ư� �� �ʿ�, ���������� �ȵ�.
        }

        for (int i = 0; i < Equip_Item_Count.Length; i++)
        {
            Equip_Sub_Count[i] = itemData[i].itemcount;
            Equip_Item_Count[i].text = Equip_Sub_Count[i].ToString();
        }
    }

    private void Update()
    {
        for (int i = 0; i < Inventory_Item_Count.Length; i++)
        {
            Inventory_Item_Count[i].text = "X " + itemData[i].itemcount;
            Equip_Sub_Count[i] = itemData[i].itemcount;
        }

        for (int i = 0; i < ItemEquip.Length; i++)
        {
            ItemEquip[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

        for (int i = 0; i < Equip_Item_Count.Length; i++)
        {
            Equip_Item_Count[i].text = Equip_Sub_Count[i].ToString();
            if (Equip_Sub_Count[i] == 0 || Equip_Max_Count == 3)
            {
                ItemStock[i].interactable = false;
            }
            else
            {
                ItemStock[i].interactable = true;
            }
        }
    }

    public void OnEquipItem(int i)
    {
        playerData.EquipItem(itemData[i].itemData);
        Equip_Max_Count++;
        Equip_Sub_Count[i]--;
    }

    public void OnDeleteItem(int i)
    {
        for(int j = 0; j < itemData.Length; j++)
        {
            if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == itemData[j].item_name)
            {
                Equip_Sub_Count[j]++;
            }
        }

        if (Equip_Max_Count > 0)
        {
            Equip_Max_Count--;
        }

        playerData.DeleteItem(default_item, i);
    }
}
