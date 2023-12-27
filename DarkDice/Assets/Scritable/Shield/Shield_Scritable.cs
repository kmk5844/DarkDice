using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Scritable : MonoBehaviour
{
    [SerializeField]
    public ShieldData shieldData;
    public int shield_dfs;
    public int shield_pride;
    public int storeflag;
    public Sprite shieldimage;

    private void Awake()
    {
        shield_dfs = shieldData.ShieldDfs;
        shield_pride = shieldData.ShieldPride;
        storeflag = shieldData.StoreFlag;
        shieldimage = shieldData.ShieldImage;
    }

    private void Update()
    {
        shield_dfs = shieldData.ShieldDfs;
        shield_pride = shieldData.ShieldPride;
        storeflag = shieldData.StoreFlag;
        shieldimage = shieldData.ShieldImage;
    }

    public void BuyWeapon_Weapon()
    {
        shieldData.ChangeStorFlag();
    }
}
