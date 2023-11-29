using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingDirector : MonoBehaviour
{
    public GameObject SettingUI;
    public bool SettingFlag;

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

    public void OnExitButton()
    {
        Application.Quit();
    }
}
