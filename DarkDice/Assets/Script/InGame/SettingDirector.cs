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
            Debug.Log("���� �Ŵ����� ���� ���̾�~!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { // ���� ���Ḧ �ϰ� ���� ��쿡
            ExitWindow.SetActive(true);
        }
    }

    public void OnSetting()
    {
        if (!SettingUI.activeSelf) // ���� â�� Ŭ������ ���
        {
            SettingUI.SetActive(true);
        }
        else
        {
            SettingUI.SetActive(false);
        }
    }

    public void OnStoryBUtton() // ���丮 Ŭ�� ���� ���
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

    public void OnExitButton() // ������ ��ư Ŭ������ ���
    {
        Application.Quit();
    }
}
