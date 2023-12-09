using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleONOFF : MonoBehaviour
{
    public Toggle to; //스토어 전용 토글 ( 아이템 / 무기 )

    private void Update()
    {
        if (to.isOn)
        {
            to.interactable = false;
        }
        else
        {
            to.interactable = true;
        }
    }
}
