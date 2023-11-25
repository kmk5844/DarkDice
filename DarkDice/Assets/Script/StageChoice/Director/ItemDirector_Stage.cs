using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector_Stage : MonoBehaviour
{
    public GameObject PlayerObject;
    Player_Scritable playerData;

    public TextMeshProUGUI[] Inventory_Item_Count; //현재 가지고 있는 아이템 갯수
    public TextMeshProUGUI[] Equip_Item_Count; //장착 아이템 표시 갯수
    public ItemData default_item;
    public GameObject[] ItemObject_Data;
    Item_Scritable[] itemData;

    int[] Equip_Sub_Count;
    int Equip_Max_Count;
    public Button[] ItemStock; // 장착 아이템에서 인벤토리에서 클릭할 경우
    public Button[] ItemEquip; // 전투시, 장착 되는 버튼

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
            playerData.DeleteItem(default_item, i); // 씬으로 돌아갈 때 필요, 남아있으면 안됨.
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
