using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingDirector : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject ExitWindow;
    GameManager gameManager;

    private void Start()
    {
        try
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        catch
        {
            print("게임 매니저가 없을 뿐이야~!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            ExitWindow.SetActive(true);
        }
    }

    public void OnSetting()
    {
        if (!SettingUI.activeSelf)
        {
            SettingUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            SettingUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnStoryBUtton()
    {
        try
        {
            gameManager.NextLevle("1-0.Toon");
        }
        catch
        {
            SceneManager.LoadScene("1-0.Toon");
        }
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
