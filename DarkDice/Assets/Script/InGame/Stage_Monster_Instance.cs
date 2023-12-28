using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Monster_Instance : MonoBehaviour
{
    public Transform monsterGroup;
    public DataTable Data;
    public StageData stageData;
    void Awake()
    {
        FindEnemy(stageData.CurretStageNum - 1);

/*        switch (SceneManager.GetActiveScene().name) // 스테이지 데이터를 불러와서 현재 진행중인 스테이지만 불러오면 됨
        {
            case "Stage1":
                stage = 0;
                FindEnemy(stage);
                break;
            case "Stage2":
                stage = 1;
                FindEnemy(stage);
                break;
            case "Stage3":
                stage = 2;
                FindEnemy(stage);
                break;
            case "Stage4":
                stage = 3;
                FindEnemy(stage);
                break;
            case "Stage5":
                stage = 4;
                FindEnemy(stage);
                break;
        }*/
    }

    public void FindEnemy(int stage) // 데이터 테이블에 있는 몬스터 정보를 가져오기 위해서
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
            var obj = Instantiate((GameObject)Resources.Load("monster/"+Data.monster_Data[Enemy(str)].name, typeof(GameObject)), monsterGroup);
            obj.GetComponent<MonsterData>().hp = Data.monster_Data[Enemy(str)].hp;
            obj.GetComponent<MonsterData>().atk = Data.monster_Data[Enemy(str)].atk;
            obj.GetComponent<MonsterData>().def = Data.monster_Data[Enemy(str)].def;
            obj.name = Data.monster_Data[Enemy(str)].name;
            obj.SetActive(false);
        }
    }
    public int Enemy(string str)
    {
        int index = Data.monster_Data.FindIndex(x => x.name.Equals(str));
        return index;
    }
}
