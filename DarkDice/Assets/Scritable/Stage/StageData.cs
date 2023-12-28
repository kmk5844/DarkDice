using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "StageData", order = 5)]
public class StageData : ScriptableObject
{
    [SerializeField]
    private int Final_stageNum;
    public int Final_StageNum { get { return Final_stageNum; } }

    [SerializeField]
    private int curretstageNum;
    public int CurretStageNum { get { return curretstageNum; } }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("stageNum"))
        {
            PlayerPrefs.SetInt("stageNum", 1);
        }

        Final_stageNum = PlayerPrefs.GetInt("stageNum");
        curretstageNum = 0;
    }

    public void WinStage()
    {
        Final_stageNum++;
        PlayerPrefs.SetInt("Final_stageNum", Final_stageNum);
    }

    public void ClickNum(int num)
    {
        curretstageNum = num;
    }

    public void testStage()
    {
        Final_stageNum = 20;
        PlayerPrefs.SetInt("stageNum", 20);
    }
    public void init()
    {
        Final_stageNum = 1;
        curretstageNum = 0;
        PlayerPrefs.SetInt("stageNum", 1);
    }
}