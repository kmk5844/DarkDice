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
    public Button[] StageButton; // �������� ��ư

    public TextMeshProUGUI stageName;

    public GameObject stageBar; // UI �� �ؿ� �ٰ� �������� �Ѵ�.
    public GameObject stageHideButton; // ȭ�� ���� Ŭ������ ��, �ٸ� ���� �� �ֵ��� �Ѵ�.
    public Image[] item_Image;
    public Image[] reward_Image; //�߰� ����

    public GameObject[] ItemLock; //���� �������� Ŭ�����, �ر�
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
                Sub_StageTitle = "������ ����";
                break;
            case 2 :
                Sub_StageTitle = "���а��� ����";
                break;
            case 3 :
                Sub_StageTitle = "������ ����� ����";
                break;
            case 4:
                Sub_StageTitle = "������ ���� ����";
                break;
            case 5:
                Sub_StageTitle = "���� ����";
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