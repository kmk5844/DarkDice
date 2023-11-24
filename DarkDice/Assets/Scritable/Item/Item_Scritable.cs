using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Scritable : MonoBehaviour
{
    [SerializeField]
    public ItemData itemData;
    public string item_name;
    public int pride;
    public int itemcount;
    public Sprite ItemImage;

    private void Awake()
    {
        item_name = itemData.ItemName;
        pride = itemData.Pride;
        itemcount = itemData.ItemCount;
        ItemImage = itemData.ItemImage;
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

    public void ItemInit()
    {
        itemData.Init();
    }
}