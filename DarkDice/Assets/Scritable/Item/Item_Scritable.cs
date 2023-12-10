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

    public void Buy_Item()
    {
        itemData.Buy();
    }

    public void Use_Item()
    {
        itemData.Use();
    }

    public void Init_Item()
    {
        itemData.Init();
    }
}