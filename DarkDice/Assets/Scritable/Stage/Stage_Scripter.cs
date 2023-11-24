using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Scripter : MonoBehaviour
{
    [SerializeField]
    public StageData stageData;
    public int stageNum;
    public int guid;
    public int curretStageNum;

    private void Awake()
    {
        stageNum = stageData.StageNum;
        guid = stageData.Guid;
        curretStageNum = stageData.CurretStageNum;
    }

    // Update is called once per frame
    void Update()
    {
        stageNum = stageData.StageNum;
        if (guid != 1)
        {
            guid = stageData.Guid;
        }
        curretStageNum = stageData.CurretStageNum;
    }

    public void  fightClickButton(int button_NUM)
    {
        stageData.ClickNum(button_NUM);
    }
    public void Win()
    {
        if (curretStageNum == stageNum)
        {
            stageData.winStage();
        }
    }

    public void stageInit()
    {
        stageData.init();
    }
}
