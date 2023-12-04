using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour {

    public GameObject StageObject;
    Stage_Scripter stageData;
    public Button[] StageButton;

    public TextMeshProUGUI stageName;
    public TextMeshProUGUI stageStory;

    public GameObject stageBar;
    public GameObject stageHideButton;
    public Image[] item_Image;
    public Image[] reward_Image;

    public GameObject[] ItemLock;
    public GameObject[] WeaponLock;

    public GameObject playerObject;
    Player_Scritable player;

    public DataTable Data;
    public Transform[] Monster_inforGroup;
    public Button[] Monster_inf_Button;

    Animator Bar_ani;

    int stageNum;
    int lockOffStage;

    void Start()
    {
        stageNum = 0;
        stageData = StageObject.GetComponent<Stage_Scripter>();
        player = playerObject.GetComponent<Player_Scritable>();
        Bar_ani = stageBar.GetComponent<Animator>();

        for (int i = 0; i < item_Image.Length; i++)
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
    }

    public void OnClickStage(int Num)
    {
        stageNum = Num;
        stageBar.SetActive(true);
        stageHideButton.SetActive(true);

        stageName.text = "STAGE" + Num + " : " + Data.stage_Data[Num-1].stage_fullname;
        stageStory.text = Data.stage_Data[Num - 1].stage_info;
        
        for(int i = 0; i < Monster_inf_Button.Length; i++)
        {
            if (i < Data.stage_Data[Num -1].enemy_count)
            {
                Monster_inf_Button[i].interactable = true;

            }
            else {
                Monster_inf_Button[i].interactable = false;
            }
        }


        for (int i = 0; i < Monster_inforGroup.Length; i++)
        {
            if (i == Data.stage_Data[Num - 1].enemy_count)
            {
                break;
            }
            string str = "";
            if (i == 0)
            {
                str = Data.stage_Data[Num - 1].enemy_unit1;
            }
            else if (i == 1)
            {
                str = Data.stage_Data[Num - 1].enemy_unit2;
            }
            else if (i == 2)
            {
                str = Data.stage_Data[Num - 1].enemy_unit3;
            }
            Monster_inforGroup[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "이름 : " + Data.monster_Data[Enemy(str)].name;
            Monster_inforGroup[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "체력 : " + Data.monster_Data[Enemy(str)].hp.ToString();
            Monster_inforGroup[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "공격력 : " + Data.monster_Data[Enemy(str)].atk.ToString();
            Monster_inforGroup[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "방어력 : " + Data.monster_Data[Enemy(str)].def.ToString();
            Monster_inforGroup[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "정보 : " + Data.monster_Data[Enemy(str)].enemy_info;
        }
        Bar_ani.SetBool("StageBar", true);
    }

    public int Enemy(string str)
    {
        int index = Data.monster_Data.FindIndex(x => x.name.Equals(str));
        return index;
    }

    public void OnClickHide()
    {
        Bar_ani.SetBool("StageBar", false);
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
