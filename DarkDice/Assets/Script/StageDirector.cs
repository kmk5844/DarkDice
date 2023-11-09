using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageDirector : MonoBehaviour { 

    public TextMeshProUGUI stageName;
    public GameObject stageBar;

    void Start()
    {
        
    }

    void Update()
    {
 
    }


    public void OnClickStage(int stageNum)
    {
        stageName.text = "Stage-" + stageNum;
        stageBar.SetActive(true);
    }
}
