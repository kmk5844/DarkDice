using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingDirector : MonoBehaviour
{
    public GameObject SettingUI;
    public bool SettingFlag;
/*    public Button[] All_Buttons;
    public Toggle[] Item_Toggles;*/

    public void OnSetting()
    {
        if (!SettingFlag)
        {
            SettingUI.SetActive(true);
            SettingFlag = true;

            Time.timeScale = 0;
        }
        else
        {
            SettingUI.SetActive(false);
            SettingFlag = false;

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
        SceneManager.LoadScene("1.StageChoice");
        Time.timeScale = 1;
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
