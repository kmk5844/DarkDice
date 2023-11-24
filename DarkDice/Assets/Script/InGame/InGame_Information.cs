using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class InGame_Information : MonoBehaviour
{
    public GameObject Player;
    Player_Scritable playerData;
    TextMeshPro playerHP;

    public Transform monsterGroup;
    GameObject[] monster;
    MonsterData[] monsterData;
    TextMeshPro[] monsterHP;
    int monsterGroup_childCount;
    // Start is called before the first frame update
    void Start()
    {
        monsterGroup_childCount = monsterGroup.childCount;
        monster = new GameObject[monsterGroup_childCount];
        monsterData = new MonsterData[monsterGroup_childCount];
        monsterHP = new TextMeshPro[monsterGroup_childCount];

        playerData = Player.GetComponent<Player_Scritable>();
        playerHP = playerData.GetComponentInChildren<TextMeshPro>();

        for (int i = 0; i < monsterGroup.childCount; i++)
        {
            monster[i] = monsterGroup.GetChild(i).gameObject;
            monsterData[i] = monster[i].GetComponent<MonsterData>();
            monsterHP[i] = monster[i].GetComponentInChildren<TextMeshPro>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.text = playerData.hp.ToString();

        for (int i = 0; i < monsterGroup_childCount; i++)
        {
            monsterHP[i].text = monsterData[i].hp.ToString();
        }
    }
}
