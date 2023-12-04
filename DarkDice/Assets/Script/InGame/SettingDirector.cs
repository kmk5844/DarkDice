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
            print("���� �Ŵ����� ���� ���̾�~!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            ExitWindow.SetActive(true);
        }

        if (SettingUI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OnSetting()
    {
        if (!SettingUI.activeSelf)
        {
            SettingUI.SetActive(true);
        }
        else
        {
            SettingUI.SetActive(false);
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
