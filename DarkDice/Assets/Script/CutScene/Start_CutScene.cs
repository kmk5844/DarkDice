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
        StartCoroutine(Typing("평화로운 왕국에 여느 때와 같은 평범한 아침이 밝았습니다"));
    }

    public void Click_NextButton()
    {
        count++;
        if (count == 1)
        {
            Toon[0].SetActive(false);
            Toon[1].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("그러나 평화는 그리 오래가지 못했으니...\n정체를 알 수 없는 몬스터 군단이 왕국을 공격해왔기 때문이죠.\n".Replace("\n\n", "\n")));

        }
        else if (count == 2)
        {
            Toon[1].SetActive(false);
            Toon[2].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("이때 평범한 점원인 당신에게도 새로운 바람이 불어오는데..\n주머니 속 주사위에서 나타난 주사위 정령 \"랑이\"!!\n정령은 당신이 바로 이 혼돈으로부터 왕국을 구할 용사라고 말합니다.\n".Replace("\n\n", "\n")));
        }
        else if (count == 3)
        {
            Toon[2].SetActive(false);
            Toon[3].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("뜬끔없이 주사위 정령의 선택을 받은 당신!\n이제 본업은 잠시 접어두고 용사로서 세상을 구할 시간입니다.".Replace("\n\n", "\n")));
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
