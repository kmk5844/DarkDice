using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData monsterData;
    public float hp;
    public int atk;
    public int def;

    private void Awake()
    {
        hp = monsterData.Hp;
        atk = monsterData.Atk;
        def = monsterData.Def;
    }
}
