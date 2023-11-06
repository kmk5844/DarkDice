using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "ItemData", order = 4)]

public class ItemData : ScriptableObject
{
    [SerializeField]
    private string monsterName;
}
