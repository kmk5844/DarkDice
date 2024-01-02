using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour {

    public GameObject StageObject; //�������� ������ �ҷ��� ������Ʈ(���� Ŭ���� ��� ������)
    Stage_Scripter stageData;
    public GameObject playerObject; //�÷��̾� ������ �ҷ��� ������Ʈ
    Player_Scritable player;
    public DataTable Data; // �������� ������ ��� ���������̺� �ҷ�����

    public Button[] StageButton; // �κ� ȭ�鿡 �ִ� �������� ��ư
    public Button[] Stage_Road; // ��ư ��

    public GameObject stageBar; //���� �������� ��
    public GameObject stageHideButton; //���� �������� �� �ٱ��� ������ ��, ������ ��ư
    Animator Bar_ani; //���� �������� �� �ִϸ��̼�

    public TextMeshProUGUI stageName; // �������� �̸�
    public TextMeshProUGUI stageStory; // �������� ���丮    
    public TextMeshProUGUI roundText; // ���� �ؽ�Ʈ   
    public Image[] item_Image;  //�÷��̾� ������ ������
    public Image[] reward_Image; // �������� ����
    public TextMeshProUGUI reward_coin_text; //�������� ���� ��, ���� �ؽ�Ʈ

    public GameObject[] ItemLock; //���������� ���� Ǯ���� ���
    public GameObject[] WeaponLock;
    /*public GameObject[] ShieldLock;*/

    public Button[] Monster_inf_Button; //���� ���� ��ư
    public Transform[] Monster_inf_Group; // ���� ���� â
    AudioSource Sound_BGM;

    int stageNum; //��ư�� Ŭ������ �� ������ ����
    int lockOffStage;// ���� Ŭ�������� ���� ��������

    void Start()
    {
        stageNum = 0;

        stageData = StageObject.GetComponent<Stage_Scripter>();
        lockOffStage = stageData.final_stageNum; // ���� Ŭ�������� ���� ��������
        player = playerObject.GetComponent<Player_Scritable>();
        Bar_ani = stageBar.GetComponent<Animator>();
        Sound_BGM = GameObject.Find("Bgm").GetComponent<AudioSource>();

        for (int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }

        for (int i = 0; i < lockOffStage; i++)
        {
            if (i == StageButton.Length) // �ε��� �ʰ� ����
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

        // �������� ���� ����
        stageName.text = "STAGE" + Num + " : " + Data.stage_Data[Num-1].stage_fullname;
        stageStory.text = Data.stage_Data[Num - 1].stage_info.Replace("\\n", "\n");
        reward_coin_text.text = Data.stage_Data[Num - 1].reward_coin.ToString() + "G";

        if(Num == 5)
        {
            roundText.text = "15����";
        }
        else
        {
            roundText.text = "10����";
        }

        // ���� ���� ����
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
            Monster_inf_Group[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�̸� : " + Data.monster_Data[Enemy(str)].name;
            Monster_inf_Group[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "ü�� : " + Data.monster_Data[Enemy(str)].hp.ToString();
            Monster_inf_Group[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "���ݷ� : " + Data.monster_Data[Enemy(str)].atk.ToString();
            Monster_inf_Group[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "���� : " + Data.monster_Data[Enemy(str)].def.ToString();
            Monster_inf_Group[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "���丮 : " + Data.monster_Data[Enemy(str)].enemy_info.Replace("\\n", "\n");
        }

        for(int i = 0; i < reward_Image.Length; i++) //�ι�° ����ĭ�� ����° ����ĭ�� �̹��� �ٲ��.
        {
            if (Num == lockOffStage) // ���� Ŭ���� ���� ���� ���
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
            else // �ƴ� ���
            {
                reward_Image[0].sprite = Resources.Load<Sprite>("Reward/default");
                reward_Image[1].sprite = Resources.Load<Sprite>("Reward/default");
            }
        }
        
        Bar_ani.SetBool("StageBar", true);
    }

    public int Enemy(string str) //stirng���� ������ �ε��� ���� ã�� ��.
    {
        int index = Data.monster_Data.FindIndex(x => x.name.Equals(str));
        return index;
    }

    public void ItemLockOff()
    {
        for (int i = 0; i < ItemLock.Length; i++)
        {
            if (lockOffStage >= 3) // 2�������� Ŭ���� ����, ����
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
        stageData.ClickNum_Stage(stageNum); // ����Ŭ���� ����
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
        SceneManager.LoadScene("0.Start_Screen"); //�÷��̾� �ʱ�ȭ�� �Ǿ�� ��.
    }
}