using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TutorialDirector : MonoBehaviour
{
    public GameObject Tutorial_UI;
    public GameObject[] Step;
    public TextMeshProUGUI[] Step_text;
    public GameObject Skip_Panel;
    public GameObject[] Dont_touch_Panel; //1스테이지에 0번은 DicePanel로 되어있다.
    public GameObject part;
    public StageData stageData;

    int guide;
    int sub_count;
    bool text_flag;
    bool Button_flag;
    bool Dice_flag;

    void Update()
    {
        if (Tutorial_UI.activeSelf && Input.GetMouseButtonDown(0) && !Button_flag && Skip_Panel.activeSelf == false)
        {
            if (text_flag)
            {
                sub_count++;
                Tutorial_Talk();
            }
        }
    }

    public void Tutorial_Talk()
    {
        if (guide == 0 && SceneManager.GetActiveScene().name.Equals("Story_Stage") && stageData.CurretStageNum == 1)
        {
            if (sub_count == 0)
            {
                Step[0].SetActive(true);
                StartCoroutine(Typing(Step_text[0], "아까 들었다시피 난 주사위 정령인 \"랑이\"라고 해."));
            }
            else if (sub_count == 1)
            {
                StartCoroutine(Typing(Step_text[0], "실제 전투에 나가기 전에, 훈련을 하는게 좋겠지?\n나약한 슬라임이 보인다!! 행운의 주사위를 가지고 한 번 싸워보자.".Replace("\\n", "\n")));
                Dice_flag = true;
            }
            else if (sub_count == 2)
            {
                Step[0].SetActive(false);
                Step[1].SetActive(true);
                StartCoroutine(Typing(Step_text[1], "전투 개시 버튼을 클릭해서 전투를 시작하자구."));
                Button_flag = true;
            }
            else if (sub_count == 3)
            {
                Step[1].SetActive(false);
                Step[0].SetActive(true);
                StartCoroutine(Typing(Step_text[0], "짜자잔~ 주사위가 나타났어!\n오른쪽 주사위는 너의 공격력을, 왼쪽은 너의 방어력을 높여줄거야.\n오른쪽 버튼을 눌러 주사위를 굴려!".Replace("\\n", "\n")));
            }
            else if (sub_count == 4)
            {
                Step_text[0].text = null;
                StartCoroutine(Typing(Step_text[0], "이제 멈...멈춰!!"));
            }
            else if (sub_count == 5)
            {
                Dice_flag = false;
                Button_flag = false;
                Step[0].SetActive(false);
                Step[2].SetActive(true);
                StartCoroutine(Typing(Step_text[2], "잘했어, 여기까지 잘 따라왔구나!\n공격하기 전에 라운드마다\n아이템을 한 번만 사용할 수 있어.".Replace("\\n", "\n")));
            }
            else if (sub_count == 6)
            {
                Step[2].SetActive(false);
                Step[3].SetActive(true);
                StartCoroutine(Typing(Step_text[3], "마지막으로, 공격하기 전에 참고사항을 꼼꼼히 읽어봐.\n이것만 잘 본다면 승리할 수 있을거야!".Replace("\\n", "\n")));
            }
            else if (sub_count == 7)
            {
                Button_flag = true;
                Step[3].SetActive(false);
                Step[1].SetActive(true);
                StartCoroutine(Typing(Step_text[1], "이제 공격해보자!".Replace("\\n", "\n")));
            }
            else if (sub_count == 8)
            {
                Button_flag = false;
                sub_count = 0;
                guide++;
                On_DontTouchPanel(false);
                PlayerPrefs.SetInt("Guide_Count", guide);
                Tutorial_UI.SetActive(false);
            }
        }else if(guide == 1 && SceneManager.GetActiveScene().name.Equals("1.StageChoice"))
        {
            if (sub_count == 0)
            {
                Step[0].SetActive(true);
                StartCoroutine(Typing(Step_text[0], "오 드디어 왔구나!\n내가 많이 기다렸다고! 이번에도 설명해줄테니 잘 들어.".Replace("\\n", "\n")));
            }else if (sub_count == 1)
            {
                part.SetActive(true);
                StartCoroutine(Typing(Step_text[0], "먼저 위에 있는 플레이어 버튼을 눌러보자."));
                Button_flag = true;
            }
            else if(sub_count == 2)
            {
                part.SetActive(false);
                Button_flag = false;
                StartCoroutine(Typing(Step_text[0], "좋아, 잘 따라오고 있어!\n스테이지를 최초로 클리어하는 경우, 스탯 포인트를 받을 수 있어.".Replace("\\n", "\n")));
            }else if(sub_count == 3)
            {
                StartCoroutine(Typing(Step_text[0], "너가 원하는대로 공격력과 방어력을 강화할 수 있지.".Replace("\\n", "\n")));
            }else if(sub_count == 4)
            {
                StartCoroutine(Typing(Step_text[0], "내 역할은 여기까지야.\n앞으로 계속 나아가 몬스터를 무찌르고 왕국을 구해줘!".Replace("\\n", "\n")));
            }else if(sub_count == 5)
            {
                StartCoroutine(Typing(Step_text[0], "아참! 이용방법을 더 자세하게 알고싶다면\n오른쪽 상단의 물음표를 눌러봐. 아이템 장착도 잊지 말고!".Replace("\\n", "\n")));
            }
            else if(sub_count == 6)
            {
                sub_count = 0;
                guide++;
                On_DontTouchPanel(false);
                PlayerPrefs.SetInt("Guide_Count", guide);
                Tutorial_UI.SetActive(false);
            }
        }
    }

    public void Check_Guide()
    {
        guide = PlayerPrefs.GetInt("Guide_Count", 0); // playerpref로 받아올 예정
        sub_count = 0;
        Dice_flag = false;
        if (guide == 0 || guide == 1)
        {
            if (guide == 0 && SceneManager.GetActiveScene().name.Equals("1.StageChoice")) { }
            else
            {
                On_DontTouchPanel(true);
                Tutorial_UI.SetActive(true);
                Tutorial_Talk();
            }
        }
        else
        {
            Tutorial_UI.SetActive(false);
        }
    }

    IEnumerator Typing(TextMeshProUGUI text_UI, string talk)
    {
        text_flag = false;

        if (Dice_flag)
        {
            Dont_touch_Panel[0].SetActive(true);
        }
        //-----------------------------------------------------

        text_UI.text = null;
        for(int i = 0; i < talk.Length; i++)
        {
            text_UI.text += talk[i];
            yield return new WaitForSeconds(0.03f);
        }

        text_flag = true;
        //----------------------------------------------------
        if (Dice_flag)
        {
            Dont_touch_Panel[0].SetActive(false);
        }
    }

    public void Button_Count()
    {
        if (Button_flag)
        {
            sub_count++;
            Tutorial_Talk();
        }
    }

    public void On_DontTouchPanel(bool flag)
    {
        if (flag)
        {
            for (int i = 0; i < Dont_touch_Panel.Length; i++)
            {
                Dont_touch_Panel[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < Dont_touch_Panel.Length; i++)
            {
                Dont_touch_Panel[i].SetActive(false);
            }
        }
    }

    public void Skip_Button()
    {
        guide++;
        if(SceneManager.GetActiveScene().name.Equals("1.StageChoice") && part.activeSelf == true)
        {
            part.SetActive(false);
        }
        PlayerPrefs.SetInt("Guide_Count", guide);
        On_DontTouchPanel(false);
        Tutorial_UI.SetActive(false);
    }
}