using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Data", menuName = "ShieldData", order = 4)]
public class ShieldData : ScriptableObject
{
    [SerializeField]
    private string shieldName;
    public string ShieldName { get { return shieldName; } }

    [SerializeField]
    private int shieldDfs;
    public int ShieldDfs { get { return shieldDfs; } }

    [SerializeField]
    private int shieldPride;
    public int ShieldPride { get {  return shieldPride; } }

    [SerializeField]
    private int storeFlag;
    public int StoreFlag { get { return storeFlag; } }

    [SerializeField]
    private Sprite shieldImage;
    public Sprite ShieldImage { get {  return shieldImage; } }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(shieldName))
        {
            PlayerPrefs.SetInt(shieldName, 0);
        }
        storeFlag = PlayerPrefs.GetInt(shieldName);
    }

    public void ChangeStorFlag()
    {
        storeFlag = 1;
        PlayerPrefs.SetInt(shieldName, 1);
    }

    public void InitWeapon() //테스트 전용 코드
    {
        storeFlag = 0;
        PlayerPrefs.SetInt(shieldName, 0);
    }
}