using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Scripter : MonoBehaviour
{
    [SerializeField]
    public StageData stageData;
    public int final_stageNum;
    public int curretStageNum;

    private void Awake()
    {
        final_stageNum = stageData.Final_StageNum;
        curretStageNum = stageData.CurretStageNum;
    }

    // Update is called once per frame
    void Update()
    {
        final_stageNum = stageData.Final_StageNum;
        curretStageNum = stageData.CurretStageNum;
    }

    public void WinStage_Stage()
    {
        if (curretStageNum == final_stageNum)
        {
            stageData.WinStage();
        }
    }
    public void ClickNum_Stage(int button_NUM)
    {
        stageData.ClickNum(button_NUM);
    }
    public void Init_Stage()
    {
        stageData.init();
    }
}
