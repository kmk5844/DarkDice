using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End_CutScene : MonoBehaviour
{
    public GameObject[] Toon;
    public TextMeshProUGUI Story_Text;
    public Button Next_Button;
    int count = 0;

    public void Start()
    {
        Next_Button.interactable = false;
        Toon[0].SetActive(true);
        StartCoroutine(Typing("������ �ο� ���, ����� ���� ��� ���� �ձ��� �ٽ� ��ȭ�� ��ã�ҽ��ϴ�.\n���� ��ư��� �������� ��ư����� ��ſ��� �޷��ֽ��ϴ�.".Replace("\n\n", "\n")));
    }

    public void Click_NextButton()
    {
        count++;
        if (count == 1)
        {
            try
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().NextLevle("1.StageChoice");
            }
            catch
            {
                SceneManager.LoadScene("1.StageChoice");
            }
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
