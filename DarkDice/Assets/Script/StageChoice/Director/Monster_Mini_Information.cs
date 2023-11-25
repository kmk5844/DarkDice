using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Mini_Information : MonoBehaviour
{
    public GameObject Information_Window;
    private bool isClick;
    public void ButtonDown()
    {
       isClick = true;
    }

    public void ButtonUp()
    {
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isClick)
        {
            Information_Window.SetActive(true);
        }
        else
        {
            Information_Window.SetActive(false);
        }
    }
}
