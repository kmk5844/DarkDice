using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "WeaponData", order = 3)]

public class NewBehaviourScript : ScriptableObject
{
    [SerializeField]
    private string weaponName;
    public string WeaponName { get { return weaponName; } }

    [SerializeField]
    private int weaponAtk;
    public int WeaponAtk { get { return weaponAtk; } }
}
