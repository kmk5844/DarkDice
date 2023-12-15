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
            Debug.Log("게임 매니저가 없을 뿐이야~!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { // 게임 종료를 하고 싶을 경우에
            ExitWindow.SetActive(true);
        }
    }

    public void OnSetting()
    {
        if (!SettingUI.activeSelf) // 설정 창을 클릭했을 경우
        {
            SettingUI.SetActive(true);
        }
        else
        {
            SettingUI.SetActive(false);
        }
    }

    public void OnStoryBUtton() // 스토리 클릭 했을 경우
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

    public void OnExitButton() // 나가기 버튼 클릭했을 경우
    {
        Application.Quit();
    }
}
