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
    private int storeFlag;
    public int StoreFlag { get { return storeFlag; } }

    [SerializeField]
    private Sprite weaponImage;
    public Sprite WeaponImage { get {  return weaponImage; } }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(weaponName))
        {
            PlayerPrefs.SetInt(weaponName, 0);
        }
        storeFlag = PlayerPrefs.GetInt(weaponName);
    }

    public void ChangeStorFlag()
    {
        storeFlag = 1;
        PlayerPrefs.SetInt(weaponName, 1);
    }

    public void InitWeapon() //테스트 전용 코드
    {
        storeFlag = 0;
        PlayerPrefs.SetInt(weaponName, 0);
    }
}