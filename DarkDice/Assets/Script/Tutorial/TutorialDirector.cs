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
            StartCoroutine(Typing(Step_text[0], "�Ʊ� ����ٽ��� �� �ֻ��� �����̾�"));
        }else if(sub_count == 1)
        {
            StartCoroutine(Typing(Step_text[0], "������ ���̷����� ���δ�!! \n����� �ֻ����� ������ �ѹ� �ο�����".Replace("\\n", "\n")));
        }
        else if (sub_count == 2)
        {
            Step[0].SetActive(false);
            Step[1].SetActive(true);
            StartCoroutine(Typing(Step_text[1], "���� ���� ��ư�� Ŭ���ؼ� ������ �������ڱ�!"));
            Button_flag = true;
        }
        else if (sub_count == 3)
        {
            StartCoroutine(Typing(Step_text[1], "¥����! �ֻ����� ���Դٱ�!\n������ ���� �� ����!".Replace("\\n", "\n")));
        }else if(sub_count == 4)
        {
            StartCoroutine(Typing(Step_text[1], "���� ��...����!!"));
        }else if(sub_count == 5)
        {
            Step[1].SetActive(false);
            Step[2].SetActive(true);
            StartCoroutine(Typing(Step_text[2], "�����ϱ� ���� ������ �ѹ� ��������".Replace("\\n", "\n")));
        }else if(sub_count == 6)
        {
            StartCoroutine(Typing(Step_text[2], "�������� �ϸ��� 1ȸ ��� ������!\n������ ��ư �ѹ� �� ������".Replace("\\n", "\n")));
        }
        else if(sub_count == 7)
        {
            Button_flag = false;
            Step[2].SetActive(false);
            Step[3].SetActive(true);
            StartCoroutine(Typing(Step_text[3], "�����ϱ� ����, ������ �����ϴ� ���� ����".Replace("\\n", "\n")));
        }else if(sub_count == 8)
        {
            Button_flag = true;
            Step[3].SetActive(false);
            Step[1].SetActive(true);
            StartCoroutine(Typing(Step_text[1], "���� �����غ���!!".Replace("\\n", "\n")));
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
        guide = 0; // playerpref�� �޾ƿ� ����
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

    public void Skip_Button() // ��ŵâ �����
    {
        guide++;
        Tutorial_UI.SetActive(false);
    }
}