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
    public Button[] StageButton;

    public TextMeshProUGUI stageName;

    public GameObject stageBar;
    public GameObject stageHideButton;
    public Image[] item_Image;
    public Image[] reward_Image;

    public GameObject[] ItemLock;
    public GameObject[] WeaponLock;

    public GameObject playerObject;
    Player_Scritable player;

    int stageNum = 0;
    int lockOffStage;

    void Start()
    {
        stageData = StageObject.GetComponent<Stage_Scripter>();
        player = playerObject.GetComponent<Player_Scritable>();
        

        for(int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }
    }

    void Update()
    {
        

        lockOffStage = stageData.stageNum;
        for(int i = 0; i < lockOffStage; i++)
        {
            StageButton[i].interactable = true;
        }

        for (int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }

        for (int i = 0; i < ItemLock.Length; i++)
        {
            if (lockOffStage >= 3)
            {
                ItemLock[i].SetActive(false);
            }
        }

        for(int i = 2; i < lockOffStage; i++)
        {
            if(lockOffStage > i)
            {
                WeaponLock[i-2].SetActive(false);
            }
        }

     /*   if(lockOffStage >= 2)
        {
            WeaponLock[0].SetActive(false);

        }
        if(lockOffStage >= 3)
        {
            WeaponLock[1].SetActive(false);
        }
        if (lockOffStage >= 4)
        {
            WeaponLock[2].SetActive(false);
        }

*/
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
