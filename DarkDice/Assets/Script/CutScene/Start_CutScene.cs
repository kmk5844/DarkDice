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
        StartCoroutine(Typing("평화로운 다이스 왕국에 여느 때와 같은 평범한 아침이 밝았습니다."));
    }

    public void Click_NextButton()
    {
        count++;
        if (count == 1)
        {
            Toon[0].SetActive(false);
            Toon[1].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("그러나 이 평화에도 위기가 찾아오는데...\n오래전부터 당신이 갖고있던 주사위에서 갑자기 나타난 정령 랑이!".Replace("\n\n", "\n")));

        }
        else if (count == 2)
        {
            Toon[1].SetActive(false);
            Toon[2].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("정령은 곧 왕국에 닥칠 암울한 미래를 보여주며,\n오직 당신만이 다가올 미래를 바꿀 수 있는 용사라고 말합니다.".Replace("\n\n", "\n")));
        }
        else if (count == 3)
        {
            Toon[2].SetActive(false);
            Toon[3].SetActive(true);
            Next_Button.interactable = false;
            StartCoroutine(Typing("갑작스레 주사위 정령의 선택을 받은 당신!\n이제 본업은 잠시 접어두고 용사로서 세상을 구할 시간입니다.".Replace("\n\n", "\n")));
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
