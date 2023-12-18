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
    public GameObject[] Dont_touch_Panel; //1���������� 0���� DicePanel�� �Ǿ��ִ�.
    public GameObject part;

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
        if (guide == 0 && SceneManager.GetActiveScene().name.Equals("Stage1"))
        {
            if (sub_count == 0)
            {
                Step[0].SetActive(true);
                StartCoroutine(Typing(Step_text[0], "�Ʊ� ����ٽ��� �� �ֻ��� ������ \"����\"��� ��"));
            }
            else if (sub_count == 1)
            {
                StartCoroutine(Typing(Step_text[0], "������ ���̷����� ���δ�!!\n����� �ֻ����� ������ �ѹ� �ο�����".Replace("\\n", "\n")));
            }
            else if (sub_count == 2)
            {
                Step[0].SetActive(false);
                Step[1].SetActive(true);
                StartCoroutine(Typing(Step_text[1], "���� ���� ��ư�� Ŭ���ؼ� ������ �������ڱ�!"));
                Button_flag = true;
                Dice_flag = true;
            }
            else if (sub_count == 3)
            {
                Step[1].SetActive(false);
                Step[0].SetActive(true);
                StartCoroutine(Typing(Step_text[0], "¥����! �ֻ����� ��Ÿ����!\n������ �ֻ����� ���� ���ݷ���, ������ ���� ������ �����ٰž�!\n������ ��ư�� ���� �ֻ����� ����!!".Replace("\\n", "\n")));
            }
            else if (sub_count == 4)
            {
                Step_text[0].text = null;
                StartCoroutine(Typing(Step_text[0], "���� ��...����!!"));
            }
            else if (sub_count == 5)
            {
                Dice_flag = false;
                Step[0].SetActive(false);
                Step[2].SetActive(true);
                StartCoroutine(Typing(Step_text[2], "���߾�! ������� �� ����Գ�!\n�����ϱ� ���� ������ �ѹ� ��������".Replace("\\n", "\n")));
            }
            else if (sub_count == 6)
            {
                StartCoroutine(Typing(Step_text[2], "¥������!! �������� �ϸ��� 1ȸ ��� ������!\n������ ��ư �ѹ� �� ������ ���� �� �־�\n�ѹ� �ݾƺ���".Replace("\\n", "\n")));
            }
            else if (sub_count == 7)
            {
                Button_flag = false;
                Step[2].SetActive(false);
                Step[3].SetActive(true);
                StartCoroutine(Typing(Step_text[3], "���������� �����ϱ� ����, ������ �����ϴ� ���� ����\n���߿� ���ƿͼ� �������ٰ�".Replace("\\n", "\n")));
            }
            else if (sub_count == 8)
            {
                Button_flag = true;
                Step[3].SetActive(false);
                Step[1].SetActive(true);
                StartCoroutine(Typing(Step_text[1], "���� �����غ���!!".Replace("\\n", "\n")));
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
                StartCoroutine(Typing(Step_text[0], "�� ���� �Ա���!\n���� ���� ��ٷȴٰ�!! ������ �����״� �� ���!".Replace("\\n", "\n")));
            }else if (sub_count == 1)
            {
                part.SetActive(true);
                StartCoroutine(Typing(Step_text[0], "���� ���� �ִ� �÷��̾� ��ư�� ��������"));
                Button_flag = true;
            }
            else if(sub_count == 2)
            {
                part.SetActive(false);
                Button_flag = false;
                StartCoroutine(Typing(Step_text[0], "����, �� ������� �־�!\n������������ ���� Ŭ�����ϴ� ���, �������ͽ��� ����Ʈ��\n ���� �� �־�".Replace("\\n", "\n")));
            }else if(sub_count == 3)
            {
                StartCoroutine(Typing(Step_text[0], "�ʰ� ���ϴ� ���ݷ°� ������ ��ȭ�� �� �־�!".Replace("\\n", "\n")));
            }else if(sub_count == 4)
            {
                StartCoroutine(Typing(Step_text[0], "�� ������ ���������.\n������ �� ���ư� ���͸� ������!".Replace("\\n", "\n")));
            }else if(sub_count == 5)
            {
                StartCoroutine(Typing(Step_text[0], "����! ����� ����ǥ ��ư�� �����ٵ� �װ͵� ������!\n���� �� �ϵ��� ����!! ������ ������ ���� ����!".Replace("\\n", "\n")));
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
        guide = PlayerPrefs.GetInt("Guide_Count", 0); // playerpref�� �޾ƿ� ����
        sub_count = 0;
        Dice_flag = false;
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
        if (Dice_flag)
        {
            Dont_touch_Panel[0].SetActive(true);
        }
        //-----------------------------------------------------
        text_flag = false;

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
        if(SceneManager.GetActiveScene().name.Equals("1.StageChoice") && part.activeSelf == true 
            )
        {
            part.SetActive(false);
        }
        PlayerPrefs.SetInt("Guide_Count", guide);
        On_DontTouchPanel(false);
        Tutorial_UI.SetActive(false);
    }
}