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

    public void Buy()
    {
        itemCount++;
    }

    public void Use()
    {
        itemCount--;
    }
}
