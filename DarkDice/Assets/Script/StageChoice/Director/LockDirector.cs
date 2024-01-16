using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDirector : MonoBehaviour
{
    [Header("스테이지 정보")]
    public GameObject StageObject;
    Stage_Scripter stageData;
    int LastStage;

    [Header("잠금 화면")]
    public GameObject[] Item_Lock;
    public GameObject[] AtkWeapon_Lock;
    public GameObject[] DefWeapon_Lock;


    void Start()
    {
        stageData = StageObject.GetComponent<Stage_Scripter>();
        LastStage = stageData.final_stageNum;

        if(LastStage >= 5)
        {
            AtkWeapon_Lock[0].SetActive(false);
            DefWeapon_Lock[0].SetActive(false);
        }
        if (LastStage >= 10)
        {
            AtkWeapon_Lock[1].SetActive(false);
            DefWeapon_Lock[1].SetActive(false);
        }
        if (LastStage >= 11)
        {
            Item_Lock[0].SetActive(false);
        }
        if(LastStage >= 15)
        {
            Item_Lock[1].SetActive(false);
            AtkWeapon_Lock[2].SetActive(false);
            DefWeapon_Lock[2].SetActive(false);
        }
        if (LastStage >= 20)
        {
            AtkWeapon_Lock[3].SetActive(false);
            DefWeapon_Lock[3].SetActive(false);
        }
    }
}
