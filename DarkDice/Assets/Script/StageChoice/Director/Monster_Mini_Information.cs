using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Mini_Information : MonoBehaviour
{
    public GameObject Information_Window;
    private bool isClick;
    Button btn;

    void Start()
    {
        btn = GetComponent<Button>();    
    }

    public void ButtonDown()
    {
       isClick = true;
    }

    public void ButtonUp()
    {
        isClick = false;
    }

    void Update()
    {
        if (isClick && btn.interactable == true)
        {
            Information_Window.SetActive(true);
        }
        else
        {
            Information_Window.SetActive(false);
        }
    }
}
