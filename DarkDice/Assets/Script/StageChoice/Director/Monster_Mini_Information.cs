using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Mini_Information : MonoBehaviour
{
    public GameObject Information_Window; //몬스터의 정보를 불러올 창
    private bool isClickFlag; 
    Button btn;

    void Start()
    {
        btn = GetComponent<Button>();    
    }

    public void ButtonDown()
    {
        isClickFlag = true;
    }

    public void ButtonUp()
    {
        isClickFlag = false;
    }

    void Update()
    {
        if (isClickFlag && btn.interactable == true)
        {
            Information_Window.SetActive(true);
        }
        else
        {
            Information_Window.SetActive(false);
        }
    }
}
