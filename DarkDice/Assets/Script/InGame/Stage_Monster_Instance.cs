using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Monster_Instance : MonoBehaviour
{
    public Transform monsterGroup;
    public DataTable_Test Data;
    int stage;
    void Awake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Test_Stage1":
                stage = 0;
                FindEnemy(stage);
                break;
            case "Test_Stage2":
                stage = 1;
                FindEnemy(stage);
                break;
            case "Test_Stage3":
                stage = 2;
                FindEnemy(stage);
                break;
            case "Test_Stage4":
                stage = 3;
                FindEnemy(stage);
                break;
            case "Test_Stage5":
                stage = 4;
                FindEnemy(stage);
                break;
        }
    }

    public void FindEnemy(int stage)
    {
        string str = "";
        for (int i = 0; i < Data.stage_Data[stage].enemy_count; i++)
        {
            if (i == 0)
            {
                str = Data.stage_Data[stage].enemy_unit1;
            }else if(i == 1)
            {
                str = Data.stage_Data[stage].enemy_unit2;
            }else if(i == 2)
            {
                str = Data.stage_Data[stage].enemy_unit3;
            }
            var obj = Instantiate((GameObject)Resources.Load("Monster", typeof(GameObject)), monsterGroup);
            obj.GetComponent<MonsterData>().hp = Data.monster_Data[Enemy(str)].health;
            obj.GetComponent<MonsterData>().atk = Data.monster_Data[Enemy(str)].atk;
            obj.GetComponent<MonsterData>().def = Data.monster_Data[Enemy(str)].def;
            obj.name = Data.monster_Data[Enemy(str)].name;
        }
    }

    public string enemy_unit_num(int i)
    {
        return "enemy_unit" + i + 1;
    }

    public int Enemy(string str)
    {
        int index = Data.monster_Data.FindIndex(x => x.name.Equals(str));
        return index;
    }
}
