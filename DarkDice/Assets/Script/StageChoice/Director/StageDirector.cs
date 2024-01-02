using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour {

    public GameObject StageObject; //스테이지 정보를 불러올 오브젝트(최초 클리어 계산 데이터)
    Stage_Scripter stageData;
    public GameObject playerObject; //플레이어 정보를 불러올 오브젝트
    Player_Scritable player;
    public DataTable Data; // 스테이지 정보가 담긴 데이터테이블 불러오기

    public Button[] StageButton; // 로비 화면에 있는 스테이지 버튼
    public Button[] Stage_Road; // 버튼 길

    public GameObject stageBar; //메인 스테이지 바
    public GameObject stageHideButton; //메인 스테이지 바 바깥을 눌렀을 때, 닫히는 버튼
    Animator Bar_ani; //메인 스테이지 바 애니메이션

    public TextMeshProUGUI stageName; // 스테이지 이름
    public TextMeshProUGUI stageStory; // 스테이지 스토리    
    public TextMeshProUGUI roundText; // 라운드 텍스트   
    public Image[] item_Image;  //플레이어 장착된 아이템
    public Image[] reward_Image; // 스테이지 보상
    public TextMeshProUGUI reward_coin_text; //스테이지 보상 중, 코인 텍스트

    public GameObject[] ItemLock; //스테이지에 따라 풀리는 잠금
    public GameObject[] WeaponLock;
    /*public GameObject[] ShieldLock;*/

    public Button[] Monster_inf_Button; //몬스터 정보 버튼
    public Transform[] Monster_inf_Group; // 몬스터 정보 창
    AudioSource Sound_BGM;

    int stageNum; //버튼을 클릭했을 때 나오는 숫자
    int lockOffStage;// 최초 클리어하지 않은 스테이지

    void Start()
    {
        stageNum = 0;

        stageData = StageObject.GetComponent<Stage_Scripter>();
        lockOffStage = stageData.final_stageNum; // 최초 클리어하지 않은 스테이지
        player = playerObject.GetComponent<Player_Scritable>();
        Bar_ani = stageBar.GetComponent<Animator>();
        Sound_BGM = GameObject.Find("Bgm").GetComponent<AudioSource>();

        for (int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }

        for (int i = 0; i < lockOffStage; i++)
        {
            if (i == StageButton.Length) // 인덱스 초과 방지
            {
                break;
            }

            StageButton[i].interactable = true;
        }
        
        for(int i = 0; i < Stage_Road.Length; i++)
        {
            if(i == lockOffStage - 1)
            {
                break;
            }
            Stage_Road[i].interactable = true;
        }

        ItemLockOff();
        WeaponLockOff();
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
        stageBar.SetActive(true);
        stageHideButton.SetActive(true);

        // 스테이지 정보 갱신
        stageName.text = "STAGE" + Num + " : " + Data.stage_Data[Num-1].stage_fullname;
        stageStory.text = Data.stage_Data[Num - 1].stage_info.Replace("\\n", "\n");
        reward_coin_text.text = Data.stage_Data[Num - 1].reward_coin.ToString() + "G";

        if(Num == 5)
        {
            roundText.text = "15라운드";
        }
        else
        {
            roundText.text = "10라운드";
        }

        // 몬스터 정보 갱신
        for (int i = 0; i < Monster_inf_Button.Length; i++)
        {
            Monster_inf_Button[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("monsterImage/default");

            if (i < Data.stage_Data[Num-1].enemy_count)
            {
                Monster_inf_Button[i].interactable = true;
            }
            else {
                Monster_inf_Button[i].interactable = false;
            }
        }
        string str = "";
        for (int i = 0; i < Monster_inf_Group.Length; i++)
        {
            if (i == Data.stage_Data[Num - 1].enemy_count)
            {
                break;
            }

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
            Monster_inf_Button[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(Data.monster_Data[Enemy(str)].image);
            Monster_inf_Group[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "이름 : " + Data.monster_Data[Enemy(str)].name;
            Monster_inf_Group[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "체력 : " + Data.monster_Data[Enemy(str)].hp.ToString();
            Monster_inf_Group[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "공격력 : " + Data.monster_Data[Enemy(str)].atk.ToString();
            Monster_inf_Group[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "방어력 : " + Data.monster_Data[Enemy(str)].def.ToString();
            Monster_inf_Group[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "스토리 : " + Data.monster_Data[Enemy(str)].enemy_info.Replace("\\n", "\n");
        }

        for(int i = 0; i < reward_Image.Length; i++) //두번째 보상칸과 세번째 보상칸만 이미지 바뀐다.
        {
            if (Num == lockOffStage) // 최초 클리어 하지 않을 경우
            {
                if (Data.stage_Data[Num - 1].reward_point == 3)
                {
                    reward_Image[0].sprite = Resources.Load<Sprite>("Reward/icon_stat3");
                }
                else if (Data.stage_Data[Num - 1].reward_point == 4)
                {
                    reward_Image[0].sprite = Resources.Load<Sprite>("Reward/icon_stat4");
                }

                if (Data.stage_Data[Num - 1].reward_hp == 0)
                {
                    reward_Image[1].sprite = Resources.Load<Sprite>("Reward/default");
                }
                else
                {
                    reward_Image[1].sprite = Resources.Load<Sprite>("Reward/icon_hp");
                }
            }
            else // 아닐 경우
            {
                reward_Image[0].sprite = Resources.Load<Sprite>("Reward/default");
                reward_Image[1].sprite = Resources.Load<Sprite>("Reward/default");
            }
        }
        
        Bar_ani.SetBool("StageBar", true);
    }

    public int Enemy(string str) //stirng으로 쓰여진 인덱스 값을 찾을 때.
    {
        int index = Data.monster_Data.FindIndex(x => x.name.Equals(str));
        return index;
    }

    public void ItemLockOff()
    {
        for (int i = 0; i < ItemLock.Length; i++)
        {
            if (lockOffStage >= 3) // 2스테이지 클리어 이후, 열림
            {
                ItemLock[i].SetActive(false);
            }
        }
    }

    public void WeaponLockOff()
    {
        if (lockOffStage >= 4)
        {
            WeaponLock[2].SetActive(false);
        }
        if (lockOffStage >= 3)
        {
            WeaponLock[1].SetActive(false);
        }
        if (lockOffStage >= 2)
        {
            WeaponLock[0].SetActive(false);
        }
    }

/*    public void ShieldLockOff()
    {

    }*/

    public void OnClickHide()
    {
        Bar_ani.SetBool("StageBar", false);
    }

    public void OnClickFight()
    {
        if(stageNum == 1)
        {
            SceneManager.LoadScene("Story_Stage");
        }
        else
        {
            SceneManager.LoadScene("Default_Stage");
        }
        Stage_Change_Bgm();
        stageData.ClickNum_Stage(stageNum); // 최초클리어 여부
    }

    public void Stage_Change_Bgm()
    {
        if (stageNum == 5)
        {
            Sound_BGM.clip = Resources.Load<AudioClip>("Sound/BGM/Boss_BGM");
            Sound_BGM.Play();
        }
        else {
            Sound_BGM.clip = Resources.Load<AudioClip>("Sound/BGM/Battle_BGM");
            Sound_BGM.Play();
        }
    }

    public void OnTestMaxStage()
    {
        stageData.test_MaxStage();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnInitButton()
    {
        stageData.Init_Stage();
        PlayerPrefs.SetInt("Guide_Count", 0);
        SceneManager.LoadScene("0.Start_Screen"); //플레이어 초기화도 되어야 함.
    }
}