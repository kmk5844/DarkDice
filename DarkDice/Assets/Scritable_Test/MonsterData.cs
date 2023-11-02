using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "MonsterData", order = int.MinValue)]

public class MonsterData : ScriptableObject
{
    [SerializeField]
    private string monsterName;
    public string MonsterName { get { return monsterName; } }
    [SerializeField]
    private float hp;
    public float Hp { get { return hp; } }
    [SerializeField]
    private int atk;
    public int Atk { get { return atk; } }
    [SerializeField]
    private int def;
    public int Def { get {  return def; } }
}
