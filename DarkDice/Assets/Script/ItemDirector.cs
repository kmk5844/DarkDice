using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector : MonoBehaviour
{
    public TextMeshProUGUI[] Item_Count;
    public GameObject[] ItemObject_Data;
    Item_Scritable[] item = new Item_Scritable[3];

    void Start()
    {
        for(int i = 0; i < Item_Count.Length; i++)
        {
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
        }
    }

    private void Update()
    {
        for (int i = 0; i < Item_Count.Length; i++)
        {
            Item_Count[i].text = "X " + item[i].itemcount;
        }
    }
}
