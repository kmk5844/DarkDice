using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_CutScene : MonoBehaviour
{
    public GameObject[] Toon;
    public TextMeshProUGUI Story_Text;
    public Button Next_Button;
    int count = 0;

    public void Start()
    {
        
        Next_Button.interactable = false;
        Toon[0].SetActive(true);
        StartCoroutine(Typing("��ȭ�ο� �ձ��� ���� ���� ���� ����� ��ħ�� ��ҽ��ϴ�"));
    }

    public void Click_NextButton()
    {
        count++;
        if (count == 1)
        {
            Toon[0].SetActive(false);
            Toon[1].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("�׷��� ��ȭ�� �׸� �������� ��������...\n��ü�� �� �� ���� ���� ������ �ձ��� �����ؿԱ� ��������.\n".Replace("\n\n", "\n")));

        }
        else if (count == 2)
        {
            Toon[1].SetActive(false);
            Toon[2].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("�̶� ����� ������ ��ſ��Ե� ���ο� �ٶ��� �Ҿ���µ�..\n�ָӴ� �� �ֻ������� ��Ÿ�� �ֻ��� ���� \"����\"!!\n������ ����� �ٷ� �� ȥ�����κ��� �ձ��� ���� ����� ���մϴ�.\n".Replace("\n\n", "\n")));
        }
        else if (count == 3)
        {
            Toon[2].SetActive(false);
            Toon[3].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("������� �ֻ��� ������ ������ ���� ���!\n���� ������ ��� ����ΰ� ���μ� ������ ���� �ð��Դϴ�.".Replace("\n\n", "\n")));
        } else if (count == 4)
        {
            try
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().NextLevel("1.StageChoice");
            }
            catch
            {
                SceneManager.LoadScene("1.StageChoice");
            }
        }
    }

    public void SkipButton()
    {
        try
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().NextLevel("1.StageChoice");
        }
        catch
        {
            SceneManager.LoadScene("1.StageChoice");
        }
    }

    IEnumerator Typing(string story)
    {
        Story_Text.text = null;
        if (count == 0)
        {
            yield return new WaitForSeconds(2.5f);
        }

        for (int i = 0; i < story.Length; i++)
        {
            Story_Text.text += story[i];
            yield return new WaitForSeconds(0.03f);
        }

        Next_Button.interactable = true;
    }
}
