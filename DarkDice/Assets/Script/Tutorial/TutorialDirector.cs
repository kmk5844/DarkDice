using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDirector : MonoBehaviour
{
    public GameObject Tutorial_UI;
    public GameObject[] Step;
    public TextMeshProUGUI[] Step_text;

    int guide;
    int sub_count;
    bool text_flag;
    bool Button_flag;

    void Start()
    {

    }

    void Update()
    {
        if (Tutorial_UI.activeSelf && Input.GetMouseButtonDown(0) && !Button_flag)
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
        if (sub_count == 0)
        {
            Step[0].SetActive(true);
            StartCoroutine(Typing(Step_text[0], "아까 들었다시피 난 주사위 요정이야"));
        }else if(sub_count == 1)
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
            StartCoroutine(Typing(Step_text[1], "짜자잔! 주사위가 나왔다구!\n굴리고 멈출 수 있지!".Replace("\\n", "\n")));
        }else if(sub_count == 4)
        {
            StartCoroutine(Typing(Step_text[1], "이제 멈...멈춰!!"));
        }else if(sub_count == 5)
        {
            Step[1].SetActive(false);
            Step[2].SetActive(true);
            StartCoroutine(Typing(Step_text[2], "공격하기 전에 아이템 한번 눌러보자".Replace("\\n", "\n")));
        }else if(sub_count == 6)
        {
            StartCoroutine(Typing(Step_text[2], "아이템은 턴마다 1회 사용 가능해!\n아이템 버튼 한번 더 누르자".Replace("\\n", "\n")));
        }
        else if(sub_count == 7)
        {
            Button_flag = false;
            Step[2].SetActive(false);
            Step[3].SetActive(true);
            StartCoroutine(Typing(Step_text[3], "공격하기 전에, 설명서를 참고하는 것이 좋아".Replace("\\n", "\n")));
        }else if(sub_count == 8)
        {
            Button_flag = true;
            Step[3].SetActive(false);
            Step[1].SetActive(true);
            StartCoroutine(Typing(Step_text[1], "이제 공격해보자!!".Replace("\\n", "\n")));
        }else if(sub_count == 9)
        {
            Button_flag = false;
            sub_count = 0;
            guide++;
            Tutorial_UI.SetActive(false);
        }
       
    }

    public void Check_Guide()
    {
        guide = 0; // playerpref로 받아올 예정
        sub_count = 0;
        if (guide == 0)
        {
            Tutorial_UI.SetActive(true);
            Tutorial_Talk();
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
            yield return new WaitForSeconds(0.05f);
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

    public void Skip_Button() // 스킵창 만들기
    {
        guide++;
        Tutorial_UI.SetActive(false);
    }
}