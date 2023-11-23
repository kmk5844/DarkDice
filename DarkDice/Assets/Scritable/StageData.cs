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
    private int guid;
    public int Guid { get { return guid; } }

    public void winStage()
    {
        stageNum++;
    }

    public void guidPlus()
    {
        guid++;
    }

}