using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingDirector : MonoBehaviour
{
    public GameObject SettingUI;
    public bool SettingFlag;
    public Button[] All_Buttons;
    public Toggle[] Item_Toggles;

    public void OnSetting()
    {
        if (!SettingFlag)
        {
            SettingUI.SetActive(true);
            SettingFlag = true;
            for(int i  = 0; i < All_Buttons.Length; i++)
            {
                All_Buttons[i].enabled = false;
                if(i < Item_Toggles.Length)
                {
                    Item_Toggles[i].GetComponent<Toggle>().enabled = false;
                }
            }
            Time.timeScale = 0;
        }
        else
        {
            SettingUI.SetActive(false);
            SettingFlag = false;
            for (int i = 0; i < All_Buttons.Length; i++)
            {
                All_Buttons[i].enabled = true;
                if (i < Item_Toggles.Length)
                {
                    Item_Toggles[i].GetComponent<Toggle>().enabled = true;
                }
            }
            Time.timeScale = 1;
        }
    }

    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void OnStage()
    {
        SceneManager.LoadScene("StageChoice");
        Time.timeScale = 1;
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
