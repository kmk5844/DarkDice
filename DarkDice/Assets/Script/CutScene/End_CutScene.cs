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
        StartCoroutine(Typing("열심히 싸운 당신, 당신의 용기와 노력 덕에 왕국은 다시 평화를 되찾았습니다.\n용사로 살아갈지 점원으로 살아갈지는 당신에게 달려있습니다.".Replace("\n\n", "\n")));
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
