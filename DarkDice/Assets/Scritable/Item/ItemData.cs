using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "ItemData", order = 4)]

public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField]
    private int pride;
    public int Pride { get { return pride; } }

    [SerializeField]
    private int itemCount;
    public int ItemCount { get { return itemCount;} }

    [SerializeField]
    private Sprite itemImage;
    public Sprite ItemImage { get { return itemImage; } }

    string str;

    private void Awake()
    {
        str = itemName + "_count";
        if (!PlayerPrefs.HasKey(str)){
            PlayerPrefs.SetInt(str, 0);
        }
        itemCount = PlayerPrefs.GetInt(str);
    }

    public void Buy()
    {
        itemCount++;
        PlayerPrefs.SetInt(str, itemCount);
    }

    public void Use()
    {
        itemCount--;
        PlayerPrefs.SetInt(str, itemCount);
    }

    public void Init()
    {
        itemCount = 0;
        PlayerPrefs.SetInt(str, itemCount);
    }
}
