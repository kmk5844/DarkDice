using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusDirector : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Stat_Hp;
    [SerializeField]
    private TextMeshProUGUI Stat_Atk;
    [SerializeField]
    private TextMeshProUGUI Amout_Atk;
    [SerializeField]
    private TextMeshProUGUI Stat_Def;
    [SerializeField]
    private TextMeshProUGUI Amout_Def;
    [SerializeField]
    private TextMeshProUGUI Rest_Status_Num;

    public Button Plus_ATK_Button;
    public Button Plus_DEF_Button;
    public Button Minus_ATK_Button;
    public Button Minus_DEF_Button;

    public GameObject playerObject;
    Player_Scritable player;
    int Rest_Status;
    int Init_Status; //초기화 전용 스테이터스
    int Sum_atk;
    int Sum_def;

    void Start()
    {
        Sum_atk = 0;
        Sum_def = 0;
        player = playerObject.GetComponent<Player_Scritable>();
        Rest_Status = player.status;
        Init_Status = player.status;
    }

    void Update()
    {
        if(Init_Status != player.status)
        {
            Rest_Status = player.status;
            Init_Status = player.status;
        }

        Stat_Hp.text = player.hp.ToString();
        Stat_Atk.text =  player.atk.ToString();
        Stat_Def.text = player.def.ToString();
        Amout_Atk.text = "+" + Sum_atk;
        Amout_Def.text = "+" + Sum_def;
        Rest_Status_Num.text = Rest_Status.ToString();

        if (Rest_Status == 0)
        {
            Plus_ATK_Button.interactable = false;
            Plus_DEF_Button.interactable = false;

            if (Sum_atk == 0)
            {
                Minus_ATK_Button.interactable = false;
            }
            else if (Sum_atk == 1)
            {
                Minus_ATK_Button.interactable = true;
            }

            if (Sum_def == 0)
            {
                Minus_DEF_Button.interactable = false;
            }
            else if (Sum_def == 1)
            {
                Minus_DEF_Button.interactable = true;
            }
        }
        else // 0이 아닌 구간
        {
            if (Sum_atk == 0)
            {
                Plus_ATK_Button.interactable = true;
                Minus_ATK_Button.interactable = false;
            }
            else
            {
                Plus_ATK_Button.interactable = true;
                Minus_ATK_Button.interactable = true;
            }

            if (Sum_def == 0)
            {
                Plus_DEF_Button.interactable = true;
                Minus_DEF_Button.interactable = false;
            }
            else
            {
                Plus_DEF_Button.interactable = true;
                Minus_DEF_Button.interactable = true;
            }
        }
    }
    public void OnPlusATK(bool flag)
    {
        if (flag)
        {
            Sum_atk++;
            Rest_Status--;
        }
        else
        {
            Sum_atk--;
            Rest_Status++;
        }
    }
    public void OnPlusDEF(bool flag)
    {
        if (flag)
        {
            Sum_def++;
            Rest_Status--;
        }
        else
        {
            Sum_def--;
            Rest_Status++;
        }
    }
    public void OnApplyButton()
    {
        player.PlusStatus_Player(Sum_atk, Sum_def, Rest_Status);
        player.ApplyStatus_Player(Rest_Status);
        Sum_atk = 0;
        Sum_def = 0;
    }

    //개발자 전용 스텟 추가
    public void OntestPlusButton()
    {
        player.RewardStatus_Player(4);
        Rest_Status = player.status;
    }
}
