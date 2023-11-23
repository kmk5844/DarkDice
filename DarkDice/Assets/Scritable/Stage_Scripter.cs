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

    }

    // Update is called once per frame
    void Update()
    {
        stageNum = stageData.StageNum;
        if (guid != 1)
        {
            guid = stageData.Guid;
        }
    }
    public void Win()
    {
        Debug.Log(curretStageNum);
        Debug.Log(stageNum);
        if (curretStageNum + 1 == stageNum)
        {
            stageData.winStage();
        }
    }
}
