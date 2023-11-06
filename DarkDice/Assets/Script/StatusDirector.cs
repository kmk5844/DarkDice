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
    private TextMeshProUGUI Amout_Hp;
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

    public Button Plus_HP_Button;
    public Button Minus_HP_Button;
    public Button Plus_ATK_Button;
    public Button Minus_ATK_Button;
    public Button Plus_DEF_Button;
    public Button Minus_DEF_Button;

    public GameObject playerObject;
    Player player;
    int Rest_Status = 0;
    int Sum_hp = 0;
    int Sum_atk = 0;
    int Sum_def = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Stat_Hp.text = player.hp.ToString();
        Stat_Atk.text =  player.atk.ToString();
        Stat_Def.text = player.def.ToString();
        Amout_Hp.text = "+" + Sum_hp;
        Amout_Atk.text = "+" + Sum_atk;
        Amout_Def.text = "+" + Sum_def;
        Rest_Status_Num.text = "" + Rest_Status;

        if (Rest_Status == 0)
        {
            Plus_HP_Button.interactable = false;
            Plus_ATK_Button.interactable = false;
            Plus_DEF_Button.interactable = false;

            if (Sum_hp == 0)
            {
                Minus_HP_Button.interactable = false;
            }
            else if (Sum_hp == 1)
            {
                Minus_HP_Button.interactable = true;
            }

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
        else
        {
            if (Sum_hp > 1)
            {
                Plus_HP_Button.interactable = false;
            }
            else if (Sum_hp == 0)
            {
                Plus_HP_Button.interactable = true;
                Minus_HP_Button.interactable = false;
            }
            else
            {
                Plus_HP_Button.interactable = true;
                Minus_HP_Button.interactable = true;
            }

            if (Sum_atk > 1)
            {
                Plus_ATK_Button.interactable = false;
            }
            else if (Sum_atk == 0)
            {
                Plus_ATK_Button.interactable = true;
                Minus_ATK_Button.interactable = false;
            }
            else
            {
                Plus_ATK_Button.interactable = true;
                Minus_ATK_Button.interactable = true;
            }

            if (Sum_def > 1)
            {
                Plus_DEF_Button.interactable = false;
            }
            else if (Sum_def == 0)
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

    public void OnPlusHP(bool flag)
    {
        if (flag)
        {
            Sum_hp++;
            Rest_Status--;
        }
        else
        {
            Sum_hp--;
            Rest_Status++;
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
        player.ChangePlayerData(Sum_hp, Sum_atk, Sum_def);
        Sum_hp = 0;
        Sum_atk = 0;
        Sum_def = 0;
    }

    public void OntestPlusButton()
    {
        Rest_Status += 2;
    }
}
