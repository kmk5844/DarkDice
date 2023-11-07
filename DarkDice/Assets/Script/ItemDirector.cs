using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector : MonoBehaviour
{
    public TextMeshProUGUI Item1_Count;
    public TextMeshProUGUI Item2_Count;
    public TextMeshProUGUI Item3_Count;
    public GameObject[] ItemObject_Data;
    Item_Scritable item1;
    Item_Scritable item2;
    Item_Scritable item3;

    void Start()
    {
        item1 = ItemObject_Data[0].GetComponent<Item_Scritable>();
        item2 = ItemObject_Data[1].GetComponent<Item_Scritable>();
        item3 = ItemObject_Data[2].GetComponent<Item_Scritable>();
    }

    private void Update()
    {
        Item1_Count.text = "X " + item1.itemcount;
        Item2_Count.text = "X " + item2.itemcount;
        Item3_Count.text = "X " + item3.itemcount;
    }
}
