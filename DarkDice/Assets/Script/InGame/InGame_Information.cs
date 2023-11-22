using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGame_Information : MonoBehaviour
{
    public GameObject Player;
    Player_Scritable playerData;
    TextMeshPro playerHP;

    public Transform monsterGroup;
    GameObject[] monster;
    Monster_Scritable[] monsterData;
    TextMeshPro[] monsterHP;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = monsterGroup.childCount;
        monster = new GameObject[count];
        monsterData = new Monster_Scritable[count];
        monsterHP = new TextMeshPro[count];

        playerData = Player.GetComponent<Player_Scritable>();
        playerHP = playerData.GetComponentInChildren<TextMeshPro>();

        for (int i = 0; i < monsterGroup.childCount; i++)
        {
            monster[i] = monsterGroup.GetChild(i).gameObject;
            monsterData[i] = monster[i].GetComponent<Monster_Scritable>();
            monsterHP[i] = monster[i].GetComponentInChildren<TextMeshPro>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.text = playerData.hp.ToString();

        for (int i = 0; i < count; i++)
        {
            monsterHP[i].text = monsterData[i].hp.ToString();
        }
    }
}
