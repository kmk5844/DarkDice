using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public TextMeshProUGUI title;
    public DataTable Data;
    public Button PlayButton;
    
    void Awake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Test_Stage1":
                title.text = changeText(0).Replace("\\n", "\n");
                break;
            case "Test_Stage2":
                title.text = changeText(1).Replace("\\n", "\n");
                break;
            case "Test_Stage3":
                title.text = changeText(2).Replace("\\n", "\n");
                break;
            case "Test_Stage4":
                title.text = changeText(3).Replace("\\n", "\n");
                break;
            case "Test_Stage5":
                title.text = changeText(4).Replace("\\n", "\n");
                break;
        }
    }

    public string changeText(int num)
    {
        return "<size=100><color=#f05650>Stage-" + (num + 1) + "</size></color>\n" + Data.stage_Data[num].stage_fullname; 
    }

    public void PlayOnButton()
    {
        PlayButton.interactable = true;
    }

    public void Ani_TitleOff()
    {
        this.gameObject.SetActive(false);
    }
}
