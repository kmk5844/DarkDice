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
}
