using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "WeaponData", order = 3)]

public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string weaponName;
    public string WeaponName { get { return weaponName; } }

    [SerializeField]
    private int weaponAtk;
    public int WeaponAtk { get { return weaponAtk; } }

    [SerializeField]
    private int weaponPride;
    public int WeaponPride { get {  return weaponPride; } }

    [SerializeField]
    private bool storeFlag = false;
    public bool StoreFlag { get { return storeFlag; } }

    [SerializeField]
    private Sprite weaponImage;
    public Sprite WeaponImage { get {  return weaponImage; } }

    public void ChangeStorFlag()
    {
        storeFlag = true;
    }
}