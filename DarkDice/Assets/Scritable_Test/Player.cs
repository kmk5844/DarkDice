using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    public float hp;
    public int atk;
    public int def;

    private void Awake()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        def = playerData.Def;
    }

    private void Update()
    {
        hp = playerData.Hp;
        atk = playerData.Atk;
        def = playerData.Def;
    }

    public void ChangePlayerData(int C_hp, int C_atk, int C_def)
    {
        playerData.PlusHP(C_hp);
        playerData.PlusATK(C_atk);
        playerData.PlusDEF(C_def);
    }
}
