using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class StageDirector : MonoBehaviour {

    public GameObject StageObject;
    Stage_Scripter stageData;
    public Button[] StageButton; // 스테이지 버튼

    public TextMeshProUGUI stageName;

    public GameObject stageBar; // UI 중 밑에 바가 나오도록 한다.
    public GameObject stageHideButton; // 화면 밖을 클릭했을 때, 바를 숨길 수 있도록 한다.
    public Image[] item_Image;
    public Image[] reward_Image; //추가 예정

    public GameObject[] ItemLock; //일정 스테이지 클리어시, 해금
    public GameObject[] WeaponLock;

    public GameObject playerObject;
    Player_Scritable player;

    int stageNum;
    int lockOffStage;

    void Start()
    {
        stageNum = 0;
        stageData = StageObject.GetComponent<Stage_Scripter>();
        player = playerObject.GetComponent<Player_Scritable>();
        lockOffStage = stageData.stageNum;

        for(int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }

        for (int i = 0; i < lockOffStage; i++)
        {
            StageButton[i].interactable = true;
        }

        for (int i = 0; i < ItemLock.Length; i++)
        {
            if (lockOffStage >= 3)
            {
                ItemLock[i].SetActive(false);
            }
        }

        for (int i = 2; i < lockOffStage; i++)
        {
            if (lockOffStage > i)
            {
                WeaponLock[i - 2].SetActive(false);
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }
    }

    public void OnClickStage(int Num)
    {
        stageNum = Num;
        stageHideButton.SetActive(true);
        string Sub_StageTitle = "";
        switch (Num){
            case 1:
                Sub_StageTitle = "여행을 떠나";
                break;
            case 2 :
                Sub_StageTitle = "어디론가를 향해";
                break;
            case 3 :
                Sub_StageTitle = "컴컴한 언덕을 향해";
                break;
            case 4:
                Sub_StageTitle = "마왕의 성을 향해";
                break;
            case 5:
                Sub_StageTitle = "끝을 향해";
                break;
        }
        stageName.text = "STAGE" + Num + " : " + Sub_StageTitle;
        stageBar.SetActive(true);
    }

    public void OnClickFight()
    {
        SceneManager.LoadScene("Test_Stage" + stageNum);
        stageData.fightClickButton(stageNum);
    }

    public void OnInitButton()
    {
        stageData.stageInit();
    }
}