using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "StageData", order = 5)]
public class StageData : ScriptableObject
{
    [SerializeField]
    private int stageNum;
    public int StageNum { get { return stageNum; } }

    [SerializeField]
    private int curretstageNum;
    public int CurretStageNum { get { return curretstageNum; } }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("stageNum"))
        {
            PlayerPrefs.SetInt("stageNum", 1);
        }


        stageNum = PlayerPrefs.GetInt("stageNum");
        curretstageNum = 0;
    }

    public void winStage()
    {
        stageNum++;
        PlayerPrefs.SetInt("stageNum", stageNum);
    }

    public void ClickNum(int num)
    {
        curretstageNum = num;
    }

    public void init()
    {
        stageNum = 1;
        curretstageNum = 0;
        PlayerPrefs.SetInt("stageNum", 1);
    }
}