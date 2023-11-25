using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleONOFF : MonoBehaviour
{
    public Toggle to;

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
