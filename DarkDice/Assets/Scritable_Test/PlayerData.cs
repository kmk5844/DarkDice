using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "PlayerData", order = int.MinValue)]

public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string playerName;
    public string PayerName { get { return playerName; } }
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
