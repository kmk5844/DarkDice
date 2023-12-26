using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector_Stage : MonoBehaviour
{
    public GameObject PlayerObject; //플레이어 데이터를 가지고 옴
    Player_Scritable playerData;
    
    public GameObject[] ItemObject; //아이템 데이터를 가지고 옴
    Item_Scritable[] itemData_Object;

    public TextMeshProUGUI[] Equip_Item_Count; //장착할 때 볼 수 있는 아이템 갯수
    public ItemData default_item;

    int[] Sub_Count; // 아이템 데이터를 받아 임시로 저장하는 갯수
    int Equip_Max_Count; //장착 갯수 -> 3개까지 셀 수 있도록

    //아이템 장착 버튼
    public Button[] ItemStock; // 소지하고 있는 아이템 버튼
    public Button[] ItemEquip; // 전투 들어가기 전의 아이템 버튼

    void Start()
    {
        itemData_Object = new Item_Scritable[ItemObject.Length];
        Equip_Max_Count = 0;
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        Sub_Count = new int[Equip_Item_Count.Length];

        for (int i = 0; i < ItemObject.Length; i++)
        {
            itemData_Object[i] = ItemObject[i].GetComponent<Item_Scritable>();
        }
        
        for (int i = 0; i < ItemEquip.Length; i++)
        {
            playerData.DeleteItem_Player(default_item, i); // 씬으로 돌아갈때 필요, 남아있으면 안됨.
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
    }

     private void Update()
    {
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
    }

    public void OnUpdate()
    {
        for(int i = 0; i < ItemObject.Length; i++)
        {
            Sub_Count[i] = itemData_Object[i].itemcount;
        }
    }
    public void OnEquipItem(int i)
    {
        playerData.EquipItem_Player(itemData_Object[i].itemData);
        Equip_Max_Count++;
        Sub_Count[i]--;
    }

    public void OnDeleteItem(int i)
    {
        if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "Chance")
        {
            Sub_Count[0]++;
        }
        else if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "Heal")
        {
            Sub_Count[1]++;
        }
        else if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "DoubleAtk")
        {
            Sub_Count[2]++;
        }
        else if (ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "DoubleDef")
        {
            Sub_Count[3]++;
        }else if(ItemEquip[i].GetComponentInChildren<Image>().sprite.name == "window_04")
        {
            Sub_Count[4]++;
        }

        if (Equip_Max_Count > 0)
        {
            Equip_Max_Count--;
        }

        playerData.DeleteItem_Player(default_item, i);
    }
}
