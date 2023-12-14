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
    public GameObject[] Dont_touch_Panel;
    public GameObject part;

    int guide;
    int sub_count;
    bool text_flag;
    bool Button_flag;

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
        if (guide == 0 && SceneManager.GetActiveScene().name.Equals("Stage1"))
        {
            if (sub_count == 0)
            {
                Step[0].SetActive(true);
                StartCoroutine(Typing(Step_text[0], "아까 들었다시피 난 주사위 요정이야"));
            }
            else if (sub_count == 1)
            {
                StartCoroutine(Typing(Step_text[0], "나약한 스켈레톤이 보인다!! \n행운의 주사위를 가지고 한번 싸워보자".Replace("\\n", "\n")));
            }
            else if (sub_count == 2)
            {
                Step[0].SetActive(false);
                Step[1].SetActive(true);
                StartCoroutine(Typing(Step_text[1], "전투 개시 버튼을 클릭해서 전투를 시작하자구!"));
                Button_flag = true;
            }
            else if (sub_count == 3)
            {
                Step[1].SetActive(false);
                Step[0].SetActive(true);
                StartCoroutine(Typing(Step_text[0], "짜자잔! 주사위가 나왔다구!\n굴리고 멈출 수 있지!".Replace("\\n", "\n")));
            }
            else if (sub_count == 4)
            {
                Step_text[0].text = null;

                StartCoroutine(Typing(Step_text[0], "이제 멈...멈춰!!"));
            }
            else if (sub_count == 5)
            {
                Step[0].SetActive(false);
                Step[2].SetActive(true);
                StartCoroutine(Typing(Step_text[2], "공격하기 전에 아이템 한번 눌러보자".Replace("\\n", "\n")));
            }
            else if (sub_count == 6)
            {
                StartCoroutine(Typing(Step_text[2], "아이템은 턴마다 1회 사용 가능해!\n아이템 버튼 한번 더 누르자".Replace("\\n", "\n")));
            }
            else if (sub_count == 7)
            {
                Button_flag = false;
                Step[2].SetActive(false);
                Step[3].SetActive(true);
                StartCoroutine(Typing(Step_text[3], "공격하기 전에, 설명서를 참고하는 것이 좋아".Replace("\\n", "\n")));
            }
            else if (sub_count == 8)
            {
                Button_flag = true;
                Step[3].SetActive(false);
                Step[1].SetActive(true);
                StartCoroutine(Typing(Step_text[1], "이제 공격해보자!!".Replace("\\n", "\n")));
            }
            else if (sub_count == 9)
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
                StartCoroutine(Typing(Step_text[0], "이기고 온건지 도망치고 온건지 모르겠지만!\n일단 어서와!".Replace("\\n", "\n")));
            }else if (sub_count == 1)
            {
                part.SetActive(true);
                StartCoroutine(Typing(Step_text[0], "위에 있는 플레이어 버튼을 눌러보자"));
                Button_flag = true;
            }
            else if(sub_count == 2)
            {
                part.SetActive(false);
                Button_flag = false;
                StartCoroutine(Typing(Step_text[0], "잘 따라오고 있어!\n스테이지마다 최초 클리어하는 경우, 스텟이 주어져".Replace("\\n", "\n")));
            }else if(sub_count == 3)
            {
                StartCoroutine(Typing(Step_text[0], "너가 원하는 공격력과 방어력을 강화할 수 있어!".Replace("\\n", "\n")));
            }else if(sub_count == 4)
            {
                StartCoroutine(Typing(Step_text[0], "내 역할은 여기까지야.\n앞으로 더 나아가 몬스터를 무찔러줘!".Replace("\\n", "\n")));
            }else if(sub_count == 5)
            {
                StartCoroutine(Typing(Step_text[0], "아참! 참고로 물음표 버튼이 보일텐데 그것도 설명서야!\n숙지 잘 하도록 하자!!".Replace("\\n", "\n")));
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
        if (guide == 0 || guide == 1)
        {
            if (guide == 0 && SceneManager.GetActiveScene().name.Equals("1.StageChoice"))
            {

            }
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

        text_UI.text = null;
        for(int i = 0; i < talk.Length; i++)
        {
            text_UI.text += talk[i];
            yield return new WaitForSeconds(0.03f);
        }

        text_flag = true;
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
        PlayerPrefs.SetInt("Guide_Count", guide);
        On_DontTouchPanel(false);
        Tutorial_UI.SetActive(false);
    }
}