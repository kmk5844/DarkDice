using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public TextMeshProUGUI title;
    public DataTable Data;
    public Button PlayButton;
    public StageData stageData;
    
    void Awake()
    {
        title.text = changeText(stageData.CurretStageNum).Replace("\\n", "\n");
    }

    public string changeText(int num)
    {
        return "<size=120><color=#FFD966>Stage " + num + "</size></color>\n" + Data.stage_Data[num - 1].stage_fullname; 
    }

    public void PlayOnButton()
    {
        PlayButton.interactable = true;
    }

    public void Ani_TitleOff()
    {
        this.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name.Equals("Story_Stage")){
            GameObject.Find("TutorialDirector").GetComponent<TutorialDirector>().Check_Guide();
        }
    }
    // 스테이지 입장 시, 스테이지 정보를 불러옴.
}
