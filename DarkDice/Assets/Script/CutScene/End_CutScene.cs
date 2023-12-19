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

    AudioSource Sound_BGM;
    int count = 0;

    public void Start()
    {
        Sound_BGM = GameObject.Find("Bgm").GetComponent<AudioSource>();
        Next_Button.interactable = false;
        Toon[0].SetActive(true);
        StartCoroutine(Typing("열심히 싸운 당신, 당신의 용기와 노력 덕에 왕국은 다시 평화를 되찾았습니다.".Replace("\n\n", "\n")));
    }

    public void Click_NextButton()
    {
        count++;
        if (count == 1)
        {
            try
            {
                Sound_BGM.clip = Resources.Load<AudioClip>("Sound/BGM/Loby_BGM");
                Sound_BGM.Play();
                GameObject.Find("GameManager").GetComponent<GameManager>().NextLevel("1.StageChoice");
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
