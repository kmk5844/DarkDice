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
    public GameObject[] ItemObject_Data;
    Item_Scritable[] item;
    public ItemData default_item;

    public Button[] ItemStock;
    public Button[] ItemEquip;

    void Start()
    {
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        item = new Item_Scritable[ItemObject_Data.Length];

        for (int i = 0; i < Item_Count.Length; i++)
        {
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
            
        }

        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            ItemStock[i].GetComponent<Image>().sprite = item[i].ItemImage;
        }

        for(int i = 0; i < ItemObject_Data.Length; i++)
        {
            ItemEquip[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

    }

    private void Update()
    {
        for (int i = 0; i < Item_Count.Length; i++)
        {
            Item_Count[i].text = "X " + item[i].itemcount;
        }

        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            ItemEquip[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }
    }

    public void OnEquipItem(int i) {
        playerData.EquipItem(item[i].itemData);
    }

    public void OnDeleteItem(int i)
    {
        playerData.DeleteItem(default_item, i);
    }
}
