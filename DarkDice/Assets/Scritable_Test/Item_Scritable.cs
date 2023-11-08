using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Scritable : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;
    public string item_name;
    public int pride;
    public int itemcount;

    private void Awake()
    {
        item_name = itemData.ItemName;
        pride = itemData.Pride;
        itemcount = itemData.ItemCount;
    }

    private void Update()
    {
        itemcount = itemData.ItemCount;
    }

    public void BuyItem()
    {
        itemData.Buy();
    }

    public void UseItem()
    {
        itemData.Use();
    }
}